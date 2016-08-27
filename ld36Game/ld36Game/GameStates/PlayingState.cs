using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ld36Game.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ld36Game.GameStates
{
    class PlayingState : BaseGameState
    {
        EntityManager eManager;  
        LevelManager levelManager;
        AssetManager aManager;

        public PlayingState(StateManager p) : base(p)
        {
            eManager = parent.game.eManager;
            levelManager = parent.game.levelManager;
            aManager = parent.game.aManager;

            levelManager.loadLevel("LudumDareLevel.dat");

            //move to level loader??????
            int spawnTileId = levelManager.getSpawnPoint();
            int startingPath = levelManager.getPathIdFromStart(spawnTileId);
            Vector2 spawnPosition = new Vector2((spawnTileId % 20) * 32 + 16, (spawnTileId / 20) * 32 + 16);
            Entity player = new Entity(spawnPosition, new Vector2(8, 16), startingPath, 16, 16, 0.0f, 0.0f, "character-bits");
            eManager.addEntity(player);
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < MapManager.getTileCount(); ++i)//this should switch to the level loader???
            {
                int xPos = (i % 20) * 32;
                int yPos = (i / 20) * 32;

                int[] testMap = levelManager.getMap();

                //draw all the grass
                int j = i / 20;
                if ((i + j) % 2 == 0) spriteBatch.Draw(aManager.getTexture("map-tiles"), new Vector2(xPos, yPos), MapManager.getTile(0), Color.White, 0.0f, new Vector2(), 2.0f, SpriteEffects.None, 0.0f);
                else spriteBatch.Draw(aManager.getTexture("map-tiles"), new Vector2(xPos, yPos), MapManager.getTile(1), Color.White, 0.0f, new Vector2(), 2.0f, SpriteEffects.None, 0.0f);

                //draw roads from the map above
                if (testMap[i] > 1) //not a grass tile
                {
                    spriteBatch.Draw(aManager.getTexture("map-tiles"), new Vector2(xPos, yPos), MapManager.getTile(testMap[i]), Color.White, 0.0f, new Vector2(), 2.0f, SpriteEffects.None, 0.0f);
                }
            }

            for (int i = 0; i < eManager.getCount(); ++i)
            {
                Entity e = eManager.getEntity(i);
                if (!e.Equals(null))
                {
                    float adjustedAngle = e.rotationAngle + e.rotationOffset;

                    spriteBatch.Draw(aManager.getTexture(e.spriteId), e.position, CharacterSpriteManager.getBody(0), Color.White, adjustedAngle, e.center, 2.0f, SpriteEffects.None, 0.0f);
                    spriteBatch.Draw(aManager.getTexture(e.spriteId), e.position, CharacterSpriteManager.getShield(0), Color.White, adjustedAngle, e.center, 2.0f, SpriteEffects.None, 0.0f);
                    spriteBatch.Draw(aManager.getTexture(e.spriteId), e.position, CharacterSpriteManager.getShirt(0), Color.White, adjustedAngle, e.center, 2.0f, SpriteEffects.None, 0.0f);
                    spriteBatch.Draw(aManager.getTexture(e.spriteId), e.position, CharacterSpriteManager.getPants(0), Color.White, adjustedAngle, e.center, 2.0f, SpriteEffects.None, 0.0f);
                    spriteBatch.Draw(aManager.getTexture(e.spriteId), e.position, CharacterSpriteManager.getHeadpiece(0), Color.White, adjustedAngle, e.center, 2.0f, SpriteEffects.None, 0.0f);
                    spriteBatch.Draw(aManager.getTexture(e.spriteId), e.position, CharacterSpriteManager.getWeapon(0), Color.White, adjustedAngle, e.center, 2.0f, SpriteEffects.None, 0.0f);
                }
            }
        }

        public override void update(GameTime gameTime)
        {
            double t = gameTime.ElapsedGameTime.TotalMilliseconds;

            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) parent.game.Exit();

            //update entity along path
            /////ENTITY MOVEMENT/UPDATE CODE
            float entitySpeed = 150.0f;
            float distanceToMove =  entitySpeed * (float)t / 1000.0f;
            //float distanceToMove = 2.0f;
            Entity e = eManager.getEntity(0);
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
                    eManager.setPosition(0, spawnPosition);
                    eManager.setCurrentPathId(0, startingPath);
                    break;
                }

                eManager.setPosition(0, e.position + targetPosition);
                int newPathId = levelManager.getPathIdFromStart(targetTile);
                eManager.setCurrentPathId(0, newPathId);
                distanceToMove -= length;

                e = eManager.getEntity(0);
                targetTile = levelManager.getDestTile(e.currentMapPathId);
                targetPosition = new Vector2((targetTile % 20) * 32 + 16, (targetTile / 20) * 32 + 16);
                targetPosition -= e.position;
                length = targetPosition.Length();
            }

            Vector2 normalisedSpeedV = targetPosition * distanceToMove / length;
            Vector2 newPosition = e.position + normalisedSpeedV;
            eManager.setPosition(0, newPosition);
            ///////END ENTITY UPDATE CODE
        }
    }
}
