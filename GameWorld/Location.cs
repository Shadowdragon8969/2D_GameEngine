using GameBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWorld
{

    public class Location
    {
        private int x = 0;
        private int y = 0;

        public Location(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public void setX(int amt)
        {
            this.x = amt;
        }

        public void setY(int amt)
        {
            this.y = amt;
        }

        public double distance(Location l)
        {
            double distX = this.x - l.getX();
            if (distX < 0) distX *= -1;
            double distY = this.y - l.getY();
            if (distY < 0) distY *= -1;
            double a2 = GameEngine.exponentiate(distX, 2);
            double b2 = GameEngine.exponentiate(distY, 2);
            double c2 = a2 + b2;
            double dist = Math.Sqrt(c2);
            return dist;
        }

    }
}
