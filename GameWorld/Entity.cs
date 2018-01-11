using GameWorldGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBase;
using GameUtil;
using GameInventory;

namespace GameWorld
{
    public class Entity : Textureable
    {

        public enum Direction
        {
            left,
            right,
            up,
            down
        }

        public Location location;
        public String name;
        public Direction direction = Direction.right;
        public UUID uuid = new UUID(-1,int.MaxValue);
        private int maxMotion = 25;
        private int motionX = 0;
        private int motionY = 0;
        private int lastDirAppliedTicks = 0;
        public int uncontrollableTicks = 0;
        public Inventory inventory;
        public bool dead = false;
        public bool moveable = true;
        public bool collides = true;
        public List<Attribute> attributes = new List<Attribute>();
        private HashMap statistics = new HashMap();

        public Entity(String name) : base(new ResourceLocation("Logo.png"))
        {
            this.name = name;
            inventory = new Inventory(this, 7);
            uuid.build();
        }

        public void setStat(String id, Object value)
        {
            statistics.put(id, value);
        }

        public Object getStat(String id)
        {
            return statistics.get(id);
        }

        public bool IntersectsWith(Entity rect)
        {
            if (rect == this)
            {
                return false;
            }
            return (rect.location.getX() < this.location.getX() + this.getBoundingBox().getTexture().ActualWidth) &&
            (this.location.getX() < (rect.location.getX() + rect.getBoundingBox().getTexture().ActualWidth)) &&
            (rect.location.getY() < this.location.getY() + this.getBoundingBox().getTexture().ActualHeight) &&
            (this.location.getY() < rect.location.getY() + rect.getBoundingBox().getTexture().ActualHeight);
        }

        public void setMaxMotion(int velocity)
        {
            this.maxMotion = velocity;
        }

        public void applyDirectionalMotion(Direction dir, int velocity)
        {
            lastDirAppliedTicks = 0;
            if (uncontrollableTicks > 0) return;
            direction = dir;
            if (dir == Direction.left)
            {
                if (motionX - velocity > (maxMotion * -1))
                {
                    motionX -= velocity;
                }else
                {
                    motionX = -maxMotion;
                }
            }
            if (dir == Direction.right)
            {
                if (motionX + velocity < maxMotion)
                {
                    motionX += velocity;
                }else
                {
                    motionX = maxMotion;
                }
            }
            if (dir == Direction.up)
            {
                if (motionY + velocity < maxMotion)
                {
                    motionY += velocity;
                }else
                {
                    motionY = maxMotion;
                }
            }
                if (dir == Direction.down)
                {
                    if (motionY - velocity > -maxMotion)
                    {
                        motionY -= velocity;
                    }
                    else
                    {
                        motionY = -maxMotion;
                    }
                }
            
        }

        public void spawn(Location loc)
        {
            location = loc;
            GameEngine.registerEntity(this);
        }


        public void invertMotion(double offset) {
            double newMX = motionX;
            double newMY = motionY;
            newMX *= offset;
            newMY *= offset;
            motionX = (int) newMX;
            motionY = (int) newMY;
        }

        public void applyMotion()
        {
            if (!moveable) return;
            if (uncontrollableTicks > 0) uncontrollableTicks--;
            foreach (Entity e in GameEngine.getRegisteredEntities())
            {
                if (this.IntersectsWith(e))
                {
                    invertMotion(-1.3);
                    uncontrollableTicks += 4;
                }
            }
            lastDirAppliedTicks++;       
            if (motionX > 0) {
                if (motionX - GameEngine.drag > 0)
                {
                    motionX -= GameEngine.drag;
                }
                else
                {
                    motionX = 0;
                }
            }
            if (motionX < 0)
            {
                if (motionX + GameEngine.drag < 0)
                {
                    motionX += GameEngine.drag;
                }
                else
                {
                    motionX = 0;
                }
            }
            if (motionY > 0)
            {
                if (motionY - GameEngine.drag > 0)
                {
                    motionY -= GameEngine.drag;
                }
                else
                {
                    motionY = 0;
                }
            }
            if (motionY < 0)
            {
                if (motionY + GameEngine.drag < 0)
                {
                    motionY += GameEngine.drag;
                }
                else
                {
                    motionY = 0;
                }
            }
            location.setX(location.getX() + (motionX / 11));
            location.setY(location.getY() + (motionY / 11));
        }

    }
}
