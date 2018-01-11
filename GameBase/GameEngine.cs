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

        /// <summary>
        /// Create a new instance of the game engine using the specified window
        /// as the canvas and a class that extends GameUpdater as the Updater
        /// </summary>
        /// <param name="w">The Canvas Window</param>
        /// <param name="up">The GameUpdater to use</param>
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

        /// <summary>
        /// Maximize the game window
        /// </summary>
        public void setMaximized()
        {
            win.Focus();
            win.WindowState = WindowState.Maximized;
            win.WindowStyle = WindowStyle.None;
        }

        /// <summary>
        /// Called when the program closes
        /// </summary>
        /// <param name="sender">WPF argument</param>
        /// <param name="e">WPF argument</param>
        private static void onClose(object sender, EventArgs e)
        {
            runTime.getClientPlayer().saveInventory();
        }

        /// <summary>
        /// Add a visual to the heads up display
        /// </summary>
        /// <param name="gui">The gui to add to the HUD</param>
        public static void addHudElement(GuiHUD gui)
        {
            iuHUD.Add(gui);
        }

        /// <summary>
        /// Remove a visual from the heads up display
        /// </summary>
        /// <param name="gui">The gui to remove from the HUD</param>
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

        /// <summary>
        /// Gets the list of elements on the HUD
        /// </summary>
        /// <returns>A list of GuiHUD objects</returns>
        public static List<GuiHUD> getHudElements()
        {
            return iuHUD;
        }

        /// <summary>
        /// Gets the registered keybindings
        /// </summary>
        /// <returns>A list of KeyBinding objects</returns>
        public static List<GameClient.KeyBinding> getRegisteredKeys()
        {
            List<GameClient.KeyBinding> lk = new List<GameClient.KeyBinding>();
            foreach (object o in keybinds.getValues())
            {
                lk.Add((GameClient.KeyBinding)o);
            }
            return lk;
        }

        /// <summary>
        /// Gets the registered entities
        /// </summary>
        /// <returns>A list of Entity objects</returns>
        public static List<Entity> getRegisteredEntities()
        {
            return entities;
        }

        /// <summary>
        /// Register an Entity in the world
        /// </summary>
        /// <param name="e">The Entity to register</param>
        public static void registerEntity(Entity e)
        {
            entities.Add(e);
        }

        /// <summary>
        /// Register a keybinding
        /// </summary>
        /// <param name="kb">The KeyBinding to register</param>
        public static void registerKeybind(GameClient.KeyBinding kb)
        {
            keybinds.put(kb.getId(), kb);
        }

        /// <summary>
        /// Exponentiate a number
        /// </summary>
        /// <param name="number">The number to apply the power to</param>
        /// <param name="exp">The power to apply</param>
        /// <returns>The exponentiated number</returns>
        public static double exponentiate(double number, int exp)
        {

            double starter = number;

            for (int i = 1; i < exp; i++)
            {

                starter = starter * number;

            }

            return starter;

        }

        /// <summary>
        /// Update the open guis
        /// </summary>
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

        /// <summary>
        /// Preform the basic actions of the game engine
        /// </summary>
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

        /// <summary>
        /// Register the client's keyboard/mouse
        /// </summary>
        /// <param name="client">ClientInput to register</param>
        public static void registerClientInput(ClientInput client)
        {
            ci = client;
        }

        /// <summary>
        /// Called every game tick to update everything
        /// </summary>
        /// <param name="sender">WPF argument</param>
        /// <param name="e">WPF argument</param>
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

        /// <summary>
        /// Close the game
        /// </summary>
        public static void close()
        {
            win.Close();
        }

        /// <summary>
        /// See if the mouse clicks a button
        /// </summary>
        /// <param name="rect">GuiButton to click</param>
        /// <returns>true if clicked | false if not</returns>
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

        /// <summary>
        /// Called when the mouse clicks
        /// </summary>
        /// <param name="sender">WPF argument</param>
        /// <param name="e">WPF argument</param>
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

        /// <summary>
        /// Get the game window
        /// </summary>
        /// <returns>The open game Window</returns>
        public static Window getLaunch()
        {
            return win;
        }

        /// <summary>
        /// Gets the graphics engine
        /// </summary>
        /// <returns>The game's RenderEngine</returns>
        public static RenderEngine getRenderEngine()
        {
            return er;
        }

    }

    public class GameUpdater
    {

        private int tick;

        /// <summary>
        /// Create a new instance of a game updater with a certain tickspeed
        /// </summary>
        /// <param name="tick">The delay between each game tick</param>
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
