using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GameClient
{
    public class KeyBinding
    {
        private Key key;
        private String id;
        private bool pressed;

        public KeyBinding(Key key, String id)
        {
            this.key = key;
            this.id = id;
            pressed = false;
        }

        public void press(bool arg0)
        {
            pressed = arg0;
        }

        public bool isPressed()
        {
            return pressed;
        }

        public Key getKey()
        {
            return key;
        }

        public String getId()
        {
            return id;
        }

    }
}
