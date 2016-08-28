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
        TowerManager tManager;
        KeyboardState oldState;

        public PlayingState(StateManager p) : base(p)
        {
            eManager = parent.game.eManager;
            levelManager = parent.game.levelManager;
            aManager = parent.game.aManager;
            tManager = parent.game.tManager;

            levelManager.loadLevel("LudumDareLevel.dat");
            eManager.prepLevel();
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            MouseState ms = Mouse.GetState();
            Color tileColor = Color.White;
            //if (ms.LeftButton == ButtonState.Pressed) tileColor = Color.LightPink;

            for (int j = 0; j < tManager.getTowerTypeCount(); ++j)
            {
                Vector2 pos = new Vector2(700.0f, 100.0f + j * 100.0f);
                if(eManager.isPaused()) drawTexture(spriteBatch, "character-bits", pos, 0.0f, Vector2.Zero, CharacterSpriteManager.getBody(0), 4.0f, Color.White);
                else drawTexture(spriteBatch, "character-bits", pos, 0.0f, Vector2.Zero, CharacterSpriteManager.getBody(4), 4.0f, Color.White);
            }

            for (int i = 0; i < MapManager.getTileCount(); ++i)//this should switch to the level loader???
            {
                int xPos = (i % 20) * 32;
                int yPos = (i / 20) * 32;

                int[] testMap = levelManager.getMap();

                if (ms.X >= xPos && ms.X < xPos + 32 && ms.Y >= yPos && ms.Y < yPos + 32 && ms.LeftButton == ButtonState.Pressed)
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

            if (tManager.getSelectedTowerType() >= 0)
            {
                drawTexture(spriteBatch, "character-bits", new Vector2(ms.X-16, ms.Y-16), 0.0f, Vector2.Zero, CharacterSpriteManager.getBody(0), 2.0f, parent.game.getMouseColor());
            }

            for (int i = 0; i < tManager.getTowerCount(); ++i)
            {
                Tower t = tManager.getTower(i);
                drawTexture(spriteBatch, "character-bits", t.position, 0.0f, new Vector2(0.0f, 8.0f), CharacterSpriteManager.getBody(0), 2.0f, Color.White);
            }

            for (int i = 0; i < tManager.getBulletCount(); ++i)
            {
                Bullet t = tManager.getBullet(i);
                drawTexture(spriteBatch, "character-bits", t.position, 0.0f, new Vector2(8.0f, 8.0f), CharacterSpriteManager.getBody(0), 2.0f, Color.White);
            }

            for (int i = eManager.getCount()-1; i >= 0;  --i)
            {
                Entity e = eManager.getEntity(i);
                if (e != null)
                {
                    float adjustedAngle = e.rotationAngle + e.rotationOffset;

                    if (e.spriteIndexes[0] >= 0) drawTexture(spriteBatch, e.spriteId, e.position, adjustedAngle, e.center, CharacterSpriteManager.getBody(e.spriteIndexes[0]), 2.0f, Color.White);
                    if (e.spriteIndexes[2] >= 0) drawTexture(spriteBatch, e.spriteId, e.position, adjustedAngle, e.center, CharacterSpriteManager.getPants(e.spriteIndexes[2]), 2.0f, Color.White);
                    if (e.spriteIndexes[1] >= 0) drawTexture(spriteBatch, e.spriteId, e.position, adjustedAngle, e.center, CharacterSpriteManager.getShirt(e.spriteIndexes[1]), 2.0f, Color.White);
                    if (e.spriteIndexes[3] >= 0) drawTexture(spriteBatch, e.spriteId, e.position, adjustedAngle, e.center, CharacterSpriteManager.getHeadpiece(e.spriteIndexes[3]), 2.0f, Color.White);
                    if (e.spriteIndexes[4] >= 0) drawTexture(spriteBatch, e.spriteId, e.position, adjustedAngle, e.center, CharacterSpriteManager.getWeapon(e.spriteIndexes[4]), 2.0f, Color.White);
                    if (e.spriteIndexes[5] >= 0) drawTexture(spriteBatch, e.spriteId, e.position, adjustedAngle, e.center, CharacterSpriteManager.getShield(e.spriteIndexes[5]), 2.0f, Color.White);
                }
            }
        }

        private void drawTexture(SpriteBatch spriteBatch, string id, Vector2 position, float adjustedAngle, Vector2 center, Rectangle rect, float scale, Color color)
        {
            spriteBatch.Draw(aManager.getTexture(id), position, rect, color, adjustedAngle, center, scale, SpriteEffects.None, 0.0f);
        }

        public override void update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            if (kState.IsKeyDown(Keys.Escape)) parent.game.Exit();
            if (kState.IsKeyDown(Keys.P) && oldState.IsKeyUp(Keys.P)) eManager.togglePaused();
            eManager.update(gameTime, parent.game.levelManager);
            tManager.update(gameTime);
            
            if(parent.game.stats.health <= 0)
            {
                //game over.
                parent.game.Exit();
            }
            oldState = kState;
        }
    }
}
