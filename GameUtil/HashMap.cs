using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameUtil
{
    public class HashMap
    {
        private static int ids = 0;
        private int id;
        private List<Set> keySet = new List<Set>();

        public HashMap()
        {
            id = ids;
            ids++;
        }

        public void put(Object key, Object value)
        {
            foreach (Set s in keySet)
            {
                if (s.getKey() == key)
                {
                    remove(key);
                    Set se = new Set(key, value);
                    keySet.Add(se);
                    return;
                }
            }
            Set set = new Set(key, value);
            keySet.Add(set);
        }

        public void remove(Object key)
        {
            if (keySet.Count <= 0) return;
            foreach (Set s in keySet)
            {
                if (s.getKey() == key)
                {
                    keySet.Remove(s);
                    return;
                }
            }
        }

        public Boolean containsKey(Object key)
        {
            foreach (Set s in keySet)
            {
                if (s.getKey() == key)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Object> getKeys()
        {
            List<Object> keys = new List<Object>();
            foreach (Set s in keySet)
            {
                keys.Add(s.getKey());
            }
            return keys;
        }

        public List<Object> getValues()
        {
            List<Object> keys = new List<Object>();
            foreach (Set s in keySet)
            {
                keys.Add(s.getValue());
            }
            return keys;
        }

        public Object get(object key)
        {
            foreach (Set s in keySet)
            {
                if (s.getKey() == key)
                {
                    return s.getValue();
                }
            }
            return null;
        }

    }
}
