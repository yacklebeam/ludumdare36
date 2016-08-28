using ld36Game.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
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
        public int[] spriteIndexes;

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
            spriteIndexes = new int[7];
        }
    }

    public class EntityManager
    {
        List<Entity> entities;
        float spawnTimer = 0.0f;
        int spawnTick = 0;
        List<string> spawnList;
        MainGameState parent;
        int spawnIndex = 0;
        bool paused = true;

        public EntityManager(MainGameState p)
        {
            entities = new List<Entity>();
            parent = p;
        }

        public void setPaused(bool val)
        {
            paused = val;
        }

        public bool isPaused()
        {
            return paused;
        }

        public void togglePaused()
        {
            paused = !paused;
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

        public void resetSpawnTimer()
        {
            spawnTimer = 0.0f;
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

        private void checkForNewSpawn()
        {
            if(spawnTimer >= spawnTick)
            {
                //new Spawn!
                spawnEnemy();
                resetSpawnTimer();
            }
        }

        private void spawnEnemy()
        {
            if(spawnIndex < spawnList.Count)
            {
                string spawnId = spawnList[spawnIndex];
                if (spawnId == "0")
                {
                    //skip
                    spawnIndex++;
                }
                else
                {
                    int spawnTileId = parent.levelManager.getSpawnPoint();
                    int startingPath = parent.levelManager.getPathIdFromStart(spawnTileId);
                    Vector2 spawnPosition = new Vector2((spawnTileId % 20) * 32 + 16, (spawnTileId / 20) * 32 + 16);
                    Entity player = new Entity(spawnPosition, new Vector2(8, 16), startingPath, 16, 16, 0.0f, 0.0f, "character-bits");
                    player.spriteIndexes = CharacterSpriteManager.getSpriteList(Convert.ToInt32(spawnId, 16));
                    addEntity(player);
                    spawnIndex++;
                }
            }
        }

        public void prepLevel()
        {
            resetSpawnTimer();
            spawnList = parent.levelManager.getSpawnList();
            spawnTick = Convert.ToInt32(spawnList[0], 16);
            spawnIndex = 1;
        }

        public void kill(int i)
        {
            entities[i].spriteIndexes[7]--;
            if(entities[i].spriteIndexes[7] <= 0)
            {
                entities[i] = null;
            }
        }

        public void update(GameTime time, LevelManager levelManager)
        {
            if (paused) return;
            double t = time.ElapsedGameTime.TotalMilliseconds;
            spawnTimer += (float)t;

            checkForNewSpawn();

            for(int i = 0; i < entities.Count; ++i)
            {

                //update entity along path
                /////ENTITY MOVEMENT/UPDATE CODE
                //float entitySpeed = 150.0f;
                //float distanceToMove = 2.0f;
                Entity e = entities[i];
                if (e == null) continue;
                float distanceToMove = (float)e.spriteIndexes[6] * (float)t / 1000.0f;
                int targetTile = levelManager.getDestTile(e.currentMapPathId);
                Vector2 targetPosition = new Vector2((targetTile % 20) * 32 + 16, (targetTile / 20) * 32 + 16);
                targetPosition -= e.position;
                float length = targetPosition.Length();

                while (length <= distanceToMove)
                {
                    if (levelManager.isEndPoint(e.currentMapPathId))
                    {
                        //entity should die!
                        parent.stats.health -= entities[i].spriteIndexes[7];
                        entities[i] = null;
                        /*int spawnTileId = levelManager.getSpawnPoint();
                        int startingPath = levelManager.getPathIdFromStart(spawnTileId);
                        Vector2 spawnPosition = new Vector2((spawnTileId % 20) * 32 + 16, (spawnTileId / 20) * 32 + 16);
                        setPosition(i, spawnPosition);
                        setCurrentPathId(i, startingPath);*/
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

                if (entities[i] == null) continue;
                Vector2 normalisedSpeedV = targetPosition * distanceToMove / length;
                Vector2 newPosition = e.position + normalisedSpeedV;
                setPosition(i, newPosition);
                ///////END ENTITY UPDATE CODE

                if (entities[i] == null)
                {
                    entities.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
