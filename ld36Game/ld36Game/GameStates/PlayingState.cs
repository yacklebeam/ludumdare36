/////////////////////////////////////////
//                                     // 
//  "lol why do u have so many unused  //
//    references that's dum."          //
//          -JT                        //
/////////////////////////////////////////
using System;                          //
using System.Collections.Generic;      //
using System.Linq;                     // <------------ @_@ [ofug]
using System.Text;                     //
using System.Threading.Tasks;          //
//-------------------------------------//
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
            MouseState ms = Mouse.GetState();
            Color tileColor = Color.White;
            //if (ms.LeftButton == ButtonState.Pressed) tileColor = Color.LightPink;

            for (int i = 0; i < MapManager.getTileCount(); ++i)//this should switch to the level loader???
            {
                int xPos = (i % 20) * 32;
                int yPos = (i / 20) * 32;

                int[] testMap = levelManager.getMap();

                if (ms.X > xPos && ms.X < xPos + 32 && ms.Y > yPos && ms.Y < yPos + 32 && ms.LeftButton == ButtonState.Pressed)
                {
                    if (testMap[i] >= 3 && testMap[i] <= 11) tileColor = Color.LightPink;
                    else tileColor = Color.LightBlue;

                    parent.game.setMouseColor(tileColor);
                }
                else tileColor = Color.White;

                //draw all the grass
                int j = i / 20;
                if ((i + j) % 2 == 0) spriteBatch.Draw(aManager.getTexture("map-tiles"), new Vector2(xPos, yPos), MapManager.getTile(0), tileColor, 0.0f, new Vector2(), 2.0f, SpriteEffects.None, 0.0f);
                else spriteBatch.Draw(aManager.getTexture("map-tiles"), new Vector2(xPos, yPos), MapManager.getTile(1), tileColor, 0.0f, new Vector2(), 2.0f, SpriteEffects.None, 0.0f);

                //draw roads from the map above
                if (testMap[i] > 1) //not a grass tile
                {
                    spriteBatch.Draw(aManager.getTexture("map-tiles"), new Vector2(xPos, yPos), MapManager.getTile(testMap[i]), tileColor, 0.0f, new Vector2(), 2.0f, SpriteEffects.None, 0.0f);
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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) parent.game.Exit();

            parent.game.eManager.update(gameTime, parent.game.levelManager);
        }
    }
}
