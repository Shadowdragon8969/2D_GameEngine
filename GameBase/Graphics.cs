using GameUtil;
using GameWorldGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameBase
{
    public class Graphics
    {

        public class RenderEngine {

            private long uuid = 11293560973;
            private bool valid = false;
            private static HashMap renderedObjects = new HashMap();
            private Canvas c;

            public RenderEngine(Window w, long uuid)
            {
                    valid = true;
                    this.c = new Canvas();
                    w.Content = c;
            }

            public void renderRectangle(Brush fill, Brush stroke, int width, int height, int x, int y, String id)
            {
                if (renderedObjects.containsKey(id))
                {
                    if (renderedObjects.get(id) is Rectangle)
                    {
                        c.Children.Remove((Rectangle)renderedObjects.get(id));
                        Rectangle rect = (Rectangle)renderedObjects.get(id);
                        rect.Width = width;
                        rect.Height = height;
                        rect.Stroke = stroke;
                        rect.Fill = fill;
                        Canvas.SetLeft(rect, x);
                        Canvas.SetBottom(rect, y);
                        c.Children.Add(rect);
                        renderedObjects.put(id, rect);
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    Rectangle r = new Rectangle();
                    r.Width = width;
                    r.Height = height;
                    r.Stroke = stroke;
                    r.Fill = fill;
                    Canvas.SetLeft(r, x);
                    Canvas.SetBottom(r, y);
                    c.Children.Add(r);
                    renderedObjects.put(id, r);
                    return;
                }
            }

            public HashMap getRenderMap()
            {
                return renderedObjects;
            }

            public UIElementCollection canvasObjects()
            {
                return c.Children;
            }

            public Canvas field_c19273()
            {
                return c;
            }

            public void renderText(String text, int x, int y, String id, double sizeX, double sizeY)
            {
                
                if (renderedObjects.containsKey(id))
                {
                    if (renderedObjects.get(id) is Label)
                    {
                        c.Children.Remove(((Label)renderedObjects.get(id)));
                        Label i = new Label();
                        i.Content = text;
                        i.RenderTransform = new ScaleTransform(sizeX, sizeY);
                        Canvas.SetBottom(i, y);
                        Canvas.SetLeft(i, x);
                        c.Children.Add(i);
                        renderedObjects.put(id, i);
                        return;
                    }
                    else
                    {
                        return;
                    }
                }else
                {
                    Label i = new Label();
                    i.Content = text;
                    i.RenderTransform = new ScaleTransform(sizeX, sizeY);
                    Canvas.SetLeft(i, x);
                    Canvas.SetBottom(i, y);
                    c.Children.Add(i);
                    renderedObjects.put(id, i);
                }
            }

            public void removeFromRender(String id)
            {
                if (renderedObjects.containsKey(id))
                {
                    if (renderedObjects.get(id) is Textureable)
                    {
                        Textureable txb = (Textureable)renderedObjects.get(id);
                        c.Children.Remove(txb.getBoundingBox(GameWorld.Entity.Direction.left).getTexture());
                    }
                }
            }

            public void removeFromRender(Textureable id)
            {
                c.Children.Remove(id.getBoundingBox().getTexture());
            }

            public void clearHUD()
            {
                foreach (Object o in renderedObjects.getKeys())
                {
                    if (renderedObjects.get(o) is GuiHUD)
                    {
                        removeFromRender((String)o);
                    }
                }
            }

            public void clearGuis()
            {
                foreach (Object o in renderedObjects.getKeys())
                {
                    if (renderedObjects.get(o) is Gui && !(renderedObjects.get(o) is GuiHUD))
                    {
                        foreach (GuiButton b in ((Gui)renderedObjects.get(o)).buttonList)
                        {
                            removeFromRender(b);
                        }
                        ((Gui)renderedObjects.get(o)).closeGui();
                        removeFromRender((String)o);
                    }
                }
            }

            public void renderGui(Gui gui, String id)
            {
                if (renderedObjects.containsKey(id))
                {
                    if (renderedObjects.get(id) is Gui || renderedObjects.get(id) is GuiScreen)
                    {
                        Gui obj = (Gui) (renderedObjects.get(id));
                        obj.drawGui();
                        c.Children.Remove(obj.getBoundingBox(GameWorld.Entity.Direction.left).getTexture());
                        Image i = obj.getBoundingBox(GameWorld.Entity.Direction.left).getTexture();
                        Canvas.SetLeft(i, obj.posX);
                        Canvas.SetBottom(i, obj.posY);
                        i.RenderTransform = new ScaleTransform(obj.sizeX, obj.sizeY);
                        c.Children.Add(i);
                        if (obj.buttonList.Count > 0)
                        {
                            foreach (GuiButton b in obj.buttonList)
                            {
                                if (c.Children.Contains(b.getBoundingBox().getTexture())) {
                                    c.Children.Remove(b.getBoundingBox().getTexture());
                                }
                                Image i2 = b.getBoundingBox().getTexture();
                                Canvas.SetLeft(i2, b.posX);
                                Canvas.SetBottom(i2, b.posY);
                                i2.RenderTransform = new ScaleTransform(b.sizeX, b.sizeY);
                                c.Children.Add(i2);
                            }
                        }
                        
                        renderedObjects.put(id, obj);
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    Image i = gui.getBoundingBox(GameWorld.Entity.Direction.left).getTexture();
                    gui.drawGui();
                    Canvas.SetLeft(i, gui.posX);
                    Canvas.SetBottom(i, gui.posY);
                    i.RenderTransform = new ScaleTransform(gui.sizeX, gui.sizeY);
                    c.Children.Add(i);
                    if (gui.buttonList.Count > 0)
                    {
                        foreach (GuiButton b in gui.buttonList)
                        {
                            if (c.Children.Contains(b.getBoundingBox().getTexture()))
                            {
                                c.Children.Remove(b.getBoundingBox().getTexture());
                                Image i2 = b.getBoundingBox().getTexture();
                                Canvas.SetLeft(i2, b.posX);
                                Canvas.SetBottom(i2, b.posY);
                                i2.RenderTransform = new ScaleTransform(b.sizeX, b.sizeY);
                                c.Children.Add(i2);
                            }
                        }
                    }
                    
                    renderedObjects.put(id, gui);
                }
            }

            public void renderTexturedRect(TexturedRect rect, int x, int y, String id)
            {
                if (renderedObjects.containsKey(id))
                {
                    if (renderedObjects.get(id) is TexturedRect)
                    {
                        c.Children.Remove(((TexturedRect)renderedObjects.get(id)).getTexture());
                        Image i = rect.getTexture();
                        Canvas.SetBottom(i, y);
                        Canvas.SetLeft(i, x);
                        c.Children.Add(i);
                        renderedObjects.put(id, rect);
                        return;
                    }
                    else
                    {
                        return;
                    }
                }else
                {
                    Image i = rect.getTexture();
                    Canvas.SetBottom(i, y);
                    Canvas.SetLeft(i, x);
                    c.Children.Add(i);
                    renderedObjects.put(id, rect);
                    return;
                }
            }

            public void renderTexturedRect(TexturedRect rect, int x, int y, double sizeX, double sizeY, String id)
            {
                if (renderedObjects.containsKey(id))
                {
                    if (renderedObjects.get(id) is TexturedRect)
                    {
                        c.Children.Remove(((TexturedRect)renderedObjects.get(id)).getTexture());
                        Image i = rect.getTexture();
                        i.RenderTransform = new ScaleTransform(sizeX, sizeY);
                        Canvas.SetBottom(i, y);
                        Canvas.SetLeft(i, x);
                        c.Children.Add(i);
                        renderedObjects.put(id, rect);
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    Image i = rect.getTexture();
                    i.RenderTransform = new ScaleTransform(sizeX, sizeY);
                    Canvas.SetBottom(i, y);
                    Canvas.SetLeft(i, x);
                    if (c.Children.Contains(i)) c.Children.Remove(i);
                    c.Children.Add(i);
                    renderedObjects.put(id, rect);
                    return;
                }
            }
        
    }

        public class TexturedRect
        {

            private Image i = new Image();

            public TexturedRect(ResourceLocation res)
            {
                String docsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                ImageSource bm = new BitmapImage(new Uri(docsPath + "\\assets\\" + res.getFilePath()));
                i.Source = bm;
            }

            public Image getTexture()
            {
                return i;
            }

            public void rotateImage(int rotation)
            {
                i.RenderTransform = new RotateTransform(rotation);
            }

            public void glChangleOpacity(double amt)
            {
                i.Opacity = amt;
            }

            public void flipImage(int amt)
            {
                i.RenderTransformOrigin = new Point(0.5, 0.5);
                ScaleTransform st = new ScaleTransform();
                st.ScaleX = amt;
                i.RenderTransform = st; 
            }

        }

        public class Gui : Textureable {

            public static HashMap guis = new HashMap();
            public int posX;
            public int posY;
            public double sizeX;
            public double sizeY;
            public String id = "";
            public bool drawn = false;
            public bool doesPauseGame = true;
            public List<GuiButton> buttonList = new List<GuiButton>();

            public Gui(int x, int y, double sizeX, double sizeY, String id) : base(new ResourceLocation("Logo.png"))
            {
                this.posX = x;
                this.posY = y;
                this.sizeX = sizeX;
                this.sizeY = sizeY;
                this.id = id;
                guis.put(id, this);
            }

            public virtual void drawGui()
            {

            }

            public virtual void onUpdate()
            {

            }

            public virtual void actionPreformed(GuiButton b)
            {

            }

            public virtual void onClose()
            {

            }

            public virtual void sendAndDraw()
            {
                GameEngine.getRenderEngine().renderGui(this, id);
                drawGui();
                drawn = true;
                GameEngine.currentGui = this;
            }

            public void closeGui()
            {
                onClose();
                if (doesPauseGame)
                {
                    GameEngine.paused = false;
                }
            }

        }

        public class GuiButton : Textureable
        {

            public int posX;
            public int posY;
            public double sizeX;
            public double sizeY;
            private Gui parent;
            public bool enabled = true;

            public GuiButton(int x, int y, double sizeX, double sizeY, Gui parent) : base(new ResourceLocation("Logo.png"))
            {
                this.parent = parent;
                posX = x;
                posY = y;
                this.sizeX = sizeX;
                this.sizeY = sizeY;
                getBoundingBox().getTexture().MouseDown += new MouseButtonEventHandler(onPress);
            } 

            public virtual void drawButton()
            {

            }

            public virtual void onUpdate()
            {

            }

            private void onPress(object sender, MouseButtonEventArgs e)
            {
                parent.actionPreformed(this);
            }

            public void draw()
            {

            }

            public virtual void press()
            {
                GameEngine.close();
            }

        }

        public class GuiScreen : Gui
        {
            public GuiScreen(int x, int y, double sizeX, double sizeY, String id) : base(x,y,sizeX,sizeY,id)
            {

            }
        }

        public class GuiHUD : Gui
        {
            public GuiHUD(int x, int y, double sizeX, double sizeY, string id) : base(x, y, sizeX, sizeY, id)
            {

            }

            public override void sendAndDraw()
            {
                this.doesPauseGame = false;
                if (GameEngine.getHudElements().Contains(this))
                {
                    GameEngine.getRenderEngine().renderGui(this, this.id);
                    drawGui();
                }
                else
                {
                    if (GameEngine.getRenderEngine().getRenderMap().getValues().Contains(this))
                    {
                        GameEngine.getRenderEngine().removeFromRender(this.id);
                    }
                }
            }

        }

    }
}
