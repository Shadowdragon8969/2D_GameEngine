using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameUtil
{
    public class Set
    {
        private Object key;
        private Object value;

        public Set(Object key, Object value)
        {
            this.key = key;
            this.value = value;
        }

        public Object getKey()
        {
            return this.key;
        }

        public Object getValue()
        {
            return this.value;
        }

    }
}
