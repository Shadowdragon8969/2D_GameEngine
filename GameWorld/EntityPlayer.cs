using GameBase;
using GameInventory;
using GameWorldGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameBase.Graphics;

namespace GameWorld
{
    public class EntityPlayer : Entity
    {

        public EntityPlayer() : base("Player1")
        {

        }

        public void saveInventory()
        {
            String builder = "";
            foreach (ItemStack it in inventory.items)
            {
                if (inventory.items.Count < 1)
                {
                    builder += it.id + "," + it.type.ToString() + "," + it.data + "," + it.name;
                }
                if (inventory.items.Count >= 1)
                {
                    builder += "|" + it.id + "," + it.type.ToString() + "," + it.data + "," + it.name;
                }
            }
            GameEngine.runTime.writeSection("inventory", builder);
        }

        public void loadInventory()
        {
            String s = GameEngine.runTime.getSection("inventory");
            String[] items = s.Split('|');
            foreach (String i in items)
            {
                String[] nbt = i.Split(',');
                if (nbt.Length > 1)
                {
                    EnumItemType et = EnumItemType.item;
                    String str = nbt[1];
                    if (str == EnumItemType.item.ToString())
                    {
                        et = EnumItemType.item;
                    }
                    if (str == EnumItemType.weapon.ToString())
                    {
                        et = EnumItemType.weapon;
                    }
                    if (str == EnumItemType.potion.ToString())
                    {
                        et = EnumItemType.potion;
                    }
                    if (str == EnumItemType.accessory.ToString())
                    {
                        et = EnumItemType.accessory;
                    }
                    if (str == EnumItemType.helmet.ToString())
                    {
                        et = EnumItemType.helmet;
                    }
                    if (str == EnumItemType.breastplate.ToString())
                    {
                        et = EnumItemType.breastplate;
                    }
                    if (str == EnumItemType.leggings.ToString())
                    {
                        et = EnumItemType.leggings;
                    }
                    if (str == EnumItemType.boots.ToString())
                    {
                        et = EnumItemType.boots;
                    }
                    inventory.items.Add(new ItemStack(int.Parse(nbt[0]), et, byte.Parse(nbt[2]), nbt[3]));
                }
            }
        }

    }
}
