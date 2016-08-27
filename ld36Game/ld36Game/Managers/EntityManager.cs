using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ld36Game.Managers
{
    //spriteBatch.Draw(<SPRITE>, <POS>, <RECT>, Color.White, <ROTATE>, <ORIGIN>, 1.0f, SpriteEffects.None, 0f);

    //Struct to hold entity information -- AS SMALL AS POSSIBLE!!
    public class Entity
    {
        public Vector2 position; //POS
        public Vector2 center; //ORIGIN
        public int currentMapPathId;
        public double height;
        public double width; //for hitbox
        public float rotationAngle; //ROTATE
        public float rotationOffset; //fuck me
        public string spriteId; //use to get RECT and SPRITE

        public Entity(Vector2 p,
                        Vector2 c,
                        int path,
                        double h,
                        double w,
                        float r,
                        float ro,
                        string s)
        {
            position = p;
            center = c;
            currentMapPathId = path;
            height = h;
            width = w;
            rotationAngle = r;
            spriteId = s;
            rotationOffset = ro;
        }

        public void draw()
        {

        }
    }

    public class EntityManager
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
            if (index <= entities.Count) return entities[index];
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

        public void setPosition(int index, Vector2 vec)
        {
            ////NAH MAN THIS HAS GOTTA STOP
            entities[index].position = vec;
        }

        public void setCurrentPathId(int index, int path)
        {
            entities[index].currentMapPathId = path;
        }

        public void update(GameTime time, LevelManager levelManager)
        {
            for(int i = 0; i < entities.Count; ++i)
            {
                double t = time.ElapsedGameTime.TotalMilliseconds;

                //update entity along path
                /////ENTITY MOVEMENT/UPDATE CODE
                float entitySpeed = 150.0f;
                float distanceToMove = entitySpeed * (float)t / 1000.0f;
                //float distanceToMove = 2.0f;
                Entity e = entities[i];
                int targetTile = levelManager.getDestTile(e.currentMapPathId);
                Vector2 targetPosition = new Vector2((targetTile % 20) * 32 + 16, (targetTile / 20) * 32 + 16);
                targetPosition -= e.position;
                float length = targetPosition.Length();

                while (length <= distanceToMove)
                {
                    if (levelManager.isEndPoint(e.currentMapPathId))
                    {
                        //entity should die!
                        int spawnTileId = levelManager.getSpawnPoint();
                        int startingPath = levelManager.getPathIdFromStart(spawnTileId);
                        Vector2 spawnPosition = new Vector2((spawnTileId % 20) * 32 + 16, (spawnTileId / 20) * 32 + 16);
                        setPosition(i, spawnPosition);
                        setCurrentPathId(i, startingPath);
                        break;
                    }

                    setPosition(i, e.position + targetPosition);
                    int newPathId = levelManager.getPathIdFromStart(targetTile);
                    setCurrentPathId(i, newPathId);
                    distanceToMove -= length;

                    e = getEntity(i);
                    targetTile = levelManager.getDestTile(e.currentMapPathId);
                    targetPosition = new Vector2((targetTile % 20) * 32 + 16, (targetTile / 20) * 32 + 16);
                    targetPosition -= e.position;
                    length = targetPosition.Length();
                }

                Vector2 normalisedSpeedV = targetPosition * distanceToMove / length;
                Vector2 newPosition = e.position + normalisedSpeedV;
                setPosition(i, newPosition);
                ///////END ENTITY UPDATE CODE
            }
        }
    }
}
