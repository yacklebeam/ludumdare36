using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ld36Game
{
    //spriteBatch.Draw(<SPRITE>, <POS>, <RECT>, Color.White, <ROTATE>, <ORIGIN>, 1.0f, SpriteEffects.None, 0f);

    //Struct to hold entity information -- AS SMALL AS POSSIBLE!!
    class Entity
    {
        public Vector2 position; //POS
        public Vector2 center; //ORIGIN
        public Vector2 velocity;
        public double height;
        public double width; //for hitbox
        public float rotationAngle; //ROTATE
        public string spriteId; //use to get RECT and SPRITE

        public Entity(  Vector2 p,
                        Vector2 c,
                        Vector2 v,
                        double h,
                        double w,
                        float r,
                        string s)
        {
            position = p;
            center = c;
            velocity = v;
            height = h;
            width = w;
            rotationAngle = r;
            spriteId = s;
        }
    }

    class EntityManager
    {
        List<Entity> entities;

        public EntityManager()
        {
            entities = new List<Entity>();
        }

        public void addEntity(Entity e)
        {
            entities.Add(e);
        }

        public Entity getEntity(int index)
        {
            return entities[index];
        }

        public int getCount()
        {
            return entities.Count;
        }
    }
}
