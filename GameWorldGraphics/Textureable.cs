using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using static GameBase.Graphics;
using static GameWorld.Entity;

namespace GameWorldGraphics
{
    public class Textureable
    {

        private ResourceLocation rl;
        private TexturedRect tr = null;
        public double alpha = 1;
        public double opacity = 1;

        public Textureable(ResourceLocation defualtResource)
        {
            rl = defualtResource;
            tr = new TexturedRect(rl);
        }

        public void bindTexture(ResourceLocation res) {
            rl = res;
            tr = new TexturedRect(rl);
        }

        public virtual TexturedRect getBoundingBox(Direction d)
        {
            if (d == Direction.left)
            {
                tr.flipImage(1);
            }
            if (d == Direction.right)
            {
                tr.flipImage(-1);
            }
            return tr;
        }

        public virtual TexturedRect getBoundingBox()
        {
            return getBoundingBox(Direction.left);
        }

    }

    public class ResourceLocation
    {

        private String path;

        public ResourceLocation(String path)
        {
            this.path = path;
        }

        public String getFilePath()
        {
            return path;
        }

    }

}
