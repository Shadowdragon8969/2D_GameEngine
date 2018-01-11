using GameWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event
{
    public class EventListener
    {

        public virtual void onEntityDamage(Entity e)
        {

        }

        public virtual void onPlayerMove(EntityPlayer p)
        {

        }

        public virtual void onEntityDamageByEntity(Entity victim, Entity target)
        {

        }

        public virtual void onPlayerUseItem(EntityPlayer p)
        {

        } 

    }
}
