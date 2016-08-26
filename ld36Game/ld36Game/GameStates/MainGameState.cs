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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            StateManager.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            StateManager.Instance.Draw(spriteBatch);

            base.Draw(gameTime);

            spriteBatch.End();
        }
    }
}
