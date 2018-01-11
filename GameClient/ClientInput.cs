using GameBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GameClient
{
    public class ClientInput
    {

        private static Window win;

        public ClientInput(Window scrn)
        {
            win = scrn;
            win.KeyDown += new KeyEventHandler(inputKeyDown);
            win.KeyUp += new KeyEventHandler(inputKeyUp);
        }

        public void inputKeyDown(object sender, KeyEventArgs e)
        {
            foreach (KeyBinding k in GameEngine.getRegisteredKeys())
            {
                if (k.getKey() == e.Key)
                {
                    k.press(true);
                }
            }
        }

        public void inputKeyUp(object sender, KeyEventArgs e)
        {
            foreach (KeyBinding k in GameEngine.getRegisteredKeys())
            {
                if (k.getKey() == e.Key)
                {
                    k.press(false);
                }
            }
        }

    }
}
