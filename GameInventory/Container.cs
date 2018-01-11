using GameWorldGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameInventory
{
    public class Container : Textureable
    {

        public int slotsPerRow = 7;
        private int maxSize;
        public List<ItemStack> items = new List<ItemStack>();

        public Container(int maxSize) : base(new ResourceLocation("Logo.png"))
        {
            this.maxSize = maxSize;
        }

    }
}
