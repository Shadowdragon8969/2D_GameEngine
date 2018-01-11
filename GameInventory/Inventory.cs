using GameWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameInventory
{
    public class Inventory : Container
    {

        private Entity holder;

        public Inventory(Entity holder, int maxSize) : base(maxSize)
        {
            this.holder = holder;
        }
    }
}
