//-----------------------------------------------------------------------
// <summary>
//          Description: Manages the game's entities.
//          Author: Jacob Troxel
//          Contributing Authors: Trent Clostio
// </summary>
//-----------------------------------------------------------------------

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ld36Game.Managers
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
        public float rotationOffset; //fuck me
        public string spriteId; //use to get RECT and SPRITE

        public Entity(  Vector2 p,
                        Vector2 c,
                        Vector2 v,
                        double h,
                        double w,
                        float r,
                        float ro,
                        string s)
        {
            position = p;
            center = c;
            velocity = v;
            height = h;
            width = w;
            rotationAngle = r;
            spriteId = s;
            rotationOffset = ro;
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
            if(index <= entities.Count) return entities[index];
            return null;
        }

        public int getCount()
        {
            return entities.Count;
        }


        ///////////////////////////////////////////////////
        //                                               //
        //  THE FUNCTIONS THAT FOLLOW ARE SO BAD THANKS  //
        //                                               //
        ///////////////////////////////////////////////////

        public void setRotation(int index, float value)
        {
            ////HOLY FUCK NO CLEAN THIS UP
            entities[index].rotationAngle = value;
        }

        public void setVelocity(int index, Vector2 vec)
        {
            ////LMAO WTF DUDE WHY
            entities[index].velocity = vec;
        }

        public void setPosition(int index, Vector2 vec)
        {
            ////NAH MAN THIS HAS GOTTA STOP
            entities[index].position = vec;
        }
    }
}
