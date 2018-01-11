using ComputerScienceUtilities;
using GameClient;
using GameUtil;
using GameWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using static GameBase.Graphics;

namespace GameBase
{
    public class GameEngine
    {

        private static Window win;
        private static GameUpdater gu;
        private static RenderEngine er;
        private static ClientInput ci;
        public static bool paused = false;
        private static DispatcherTimer dt = new DispatcherTimer();
        private static List<Entity> entities = new List<Entity>();
        private static HashMap keybinds = new HashMap();
        public static int drag = 1;
        public static Gui currentGui = null;
        private static List<GuiHUD> iuHUD = new List<GuiHUD>();
        public static Client runTime;
        public static TexturedRect backGround = null;

        public GameEngine(Window w, GameUpdater up)
        {
            win = w;
            gu = up;
            er = new RenderEngine(w, 11293560973);
            ci = new ClientInput(w);
            win.MouseDown += new MouseButtonEventHandler(onClick);
            win.Closed += new EventHandler(onClose);
            runTime = Client.getClient();
            dt.Tick += new EventHandler(onUpdate);
            dt.Interval = new TimeSpan(0,0,0,0,gu.getTick());
            dt.IsEnabled = true;
            dt.Start();
        }

        private static void onClose(object sender, EventArgs e)
        {
            runTime.getClientPlayer().saveInventory();
        }

        public static void addHudElement(GuiHUD gui)
        {
            iuHUD.Add(gui);
        }

        public static void removeHudElement(GuiHUD gui)
        {
            if (iuHUD.ToArray().Length > 0)
            {
                foreach (GuiHUD g in iuHUD)
                {
                    if (g.id == gui.id)
                    {
                        iuHUD.Remove(g);
                        return;
                    }
                }
            }
        }

        public static List<GuiHUD> getHudElements()
        {
            return iuHUD;
        }

        public static List<GameClient.KeyBinding> getRegisteredKeys()
        {
            List<GameClient.KeyBinding> lk = new List<GameClient.KeyBinding>();
            foreach (object o in keybinds.getValues())
            {
                lk.Add((GameClient.KeyBinding)o);
            }
            return lk;
        }

        public static List<Entity> getRegisteredEntities()
        {
            return entities;
        }

        public static void registerEntity(Entity e)
        {
            entities.Add(e);
        }

        public static void registerKeybind(GameClient.KeyBinding kb)
        {
            keybinds.put(kb.getId(), kb);
        }

        public static double exponentiate(double number, int exp)
        {

            double starter = number;

            for (int i = 1; i < exp; i++)
            {

                starter = starter * number;

            }

            return starter;

        }

        private static void updateGuis()
        {
            er.clearHUD();
            if (iuHUD.ToArray().Length > 0)
            {
                foreach (GuiHUD gui in iuHUD)
                {
                    gui.sendAndDraw();
                }
            }
            if (currentGui != null)
            {
                currentGui.onUpdate();
                if (!currentGui.drawn)
                {
                    currentGui.sendAndDraw();
                    if (currentGui.doesPauseGame)
                    {
                        paused = true;
                    }
                }
            }
            else
            {
                er.clearGuis();
            }
        }

        private static void onEngineUpdate()
        {
            if (backGround != null)
            {
                er.renderTexturedRect(backGround, 0, 0, "gamebase.background");
            }
            foreach (Entity e in entities)
            {
                if ((e is Entity) && !(e is TileEntity))
                {
                    if (!e.dead)
                    {
                        e.applyMotion();
                        er.renderTexturedRect(e.getBoundingBox(e.direction), e.location.getX(), e.location.getY(), e.uuid.compress());
                    }else
                    {
                        if (er.getRenderMap().getValues().Contains(e.getBoundingBox().getTexture())) {
                            er.removeFromRender(e);
                        }
                    }
                }
                if (e is TileEntity)
                {
                    TileEntity t = (TileEntity)e;
                    er.renderTexturedRect(t.getBoundingBox(), t.location.getX(), t.location.getY(), t.sizeX, t.sizeY, t.uuid.compress());
                }
            }
        }


        public static void registerClientInput(ClientInput client)
        {
            ci = client;
        }

        private static void onUpdate(object sender, EventArgs e)
        {
            dt.Stop();
            updateGuis();
            gu.onUpdate();
            if (!paused)
            {
                onEngineUpdate();
            }
                dt.Start();
        }

        public static void close()
        {
            win.Close();
        }

        private static bool IntersectsWith(GuiButton rect)
        {
            Point pw = Mouse.GetPosition(rect.getBoundingBox().getTexture());
            Point ps = win.PointToScreen(pw);
            double x = ps.X;
            double y = ps.Y;
            return (rect.posX < x) &&
            (x < (rect.posX + rect.getBoundingBox().getTexture().Width)) &&
            (rect.posY < y) &&
            (y < rect.posY + rect.getBoundingBox().getTexture().Height);
        }

        private static void onClick(object sender, MouseButtonEventArgs e)
        {
            
            /*if (currentGui != null && currentGui.buttonList.Count > 0) {
                //win.Close();
                foreach (GuiButton b in currentGui.buttonList)
                {
                    if (IntersectsWith(b) && b.enabled && e.LeftButton == MouseButtonState.Pressed)
                    {
                        currentGui.actionPreformed(b);
                    }
                }
            }*/
        }

        public static Window getLaunch()
        {
            return win;
        }

        public static RenderEngine getRenderEngine()
        {
            return er;
        }

    }

    public class GameUpdater
    {

        private int tick;

        public GameUpdater(int tick)
        {
            this.tick = tick;
        }

        public int getTick()
        {
            return tick;
        }

        public virtual void onUpdate()
        {

        }

    }

}
