//-----------------------------------------------------------------------
// <summary>
//          Description: The game's main state.
//          Author: Jacob Troxel
//          Contributing Authors: Trent Clostio
// </summary>
//-----------------------------------------------------------------------

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

using ld36Game.Managers;

namespace ld36Game.GameStates
{
    public class MainGameState : Game
    {
        GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private EntityManager eManager;
        private AssetManager aManager;

        public MainGameState()
        {
            graphics = new GraphicsDeviceManager(this);
            eManager = new EntityManager();
            aManager = new AssetManager();
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            //TEST, MOVE TO LEVEL LOADER
            Entity player = new Entity(new Vector2(50.0f, 50.0f), new Vector2(49.0f, 37.5f), new Vector2(), 75, 98, 0.0f, (float)Math.PI / 2.0f, "player");
            Entity player2 = new Entity(new Vector2(400.0f, 50.0f), new Vector2(49.0f, 37.5f), new Vector2(), 75, 98, 0.0f, -(float)Math.PI / 2.0f, "enemy");
            eManager.addEntity(player);
            eManager.addEntity(player2);
            //END TEST

            graphics.PreferredBackBufferWidth = (int)StateManager.Instance.Dimensions.X;
            graphics.PreferredBackBufferHeight = (int)StateManager.Instance.Dimensions.Y;
            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>Loads the content.</summary>
        protected override void LoadContent()
        { 
            spriteBatch = new SpriteBatch(GraphicsDevice);

            StateManager.Instance.LoadContent(Content);

            aManager.loadImageAsset("player", "images/test-texture", Content);
            aManager.loadImageAsset("enemy", "images/enemy-texture", Content);
        }

        protected override void UnloadContent()
        {
            StateManager.Instance.UnloadContent();
            Content.Unload();
        }

        /// <summary>Updates the specified game time.</summary>
        /// <param name="gameTime">The game time.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //test code for mouse, set rotation to mouse cursor
            MouseState ms = Mouse.GetState();
            double curX = ms.X;
            double curY = ms.Y;

            Entity e = eManager.getEntity(0);

            double oldX = e.position.X;
            double oldY = e.position.Y;

            double theta = Math.Atan2(curY - oldY, curX - oldX);

            eManager.setRotation(0, (float)theta);
            //end test code

            //test code for mouse click, ship go when clicked
            if(ms.LeftButton == ButtonState.Pressed)
            {
                Vector2 newVel = new Vector2();
                double xChange = 5.0f * Math.Cos(theta);
                double yChange = 5.0f * Math.Sin(theta);

                newVel.X = (float)xChange;
                newVel.Y = (float)yChange;
                eManager.setVelocity(0, newVel);

                Vector2 newPos = e.position + newVel;
                eManager.setPosition(0, newPos);
            }

            //end test code

            StateManager.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            for(int i = 0; i < eManager.getCount(); ++i)
            {
                Entity e = eManager.getEntity(i);
                if(!e.Equals(null))
                {
                    float adjustedAngle = e.rotationAngle + e.rotationOffset;
                    spriteBatch.Draw(aManager.getTexture(e.spriteId), e.position, null, Color.White, adjustedAngle, e.center, 1.0f, SpriteEffects.None, 0.0f);
                }
            }

            StateManager.Instance.Draw(spriteBatch);

            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
