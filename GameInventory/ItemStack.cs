using GameWorldGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameInventory
{
    public class ItemStack : Textureable
    {

        public int id;
        public byte data;
        public EnumItemType type;
        public String name;

        public ItemStack(int id, EnumItemType type, byte data, String name) : base(new ResourceLocation("Logo.png"))
        {
            this.name = name;
            this.id = id;
            this.type = type;
            this.data = data;
        }

    }

    public enum EnumItemType
    {
        item,
        weapon,
        potion,
        accessory,
        helmet,
        breastplate,
        leggings,
        boots
    }
}
