using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWorld
{
    public class Attribute
    {

        private String gid;

        public Attribute(String genericID)
        {
            this.gid = genericID;
        }

        public void apply(Entity e) {
            e.attributes.Add(this);
            onApply(e);
        }

        public virtual void onApply(Entity e)
        {

        }

    }
}
