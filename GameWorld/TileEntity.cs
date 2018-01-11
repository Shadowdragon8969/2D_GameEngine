using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWorld
{
    public class TileEntity : Entity
    {

        public double sizeX;
        public double sizeY;

        public TileEntity(int x, int y, double sizeX, double sizeY, String name) : base(name)
        {

            setMaxMotion(0);
            moveable = false;
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            spawn(new Location(x, y));

        }

    }
}
