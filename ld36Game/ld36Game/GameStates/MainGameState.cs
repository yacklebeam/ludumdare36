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
        private EntityManager eManager;
        private AssetManager aManager;
        private StateManager sManager;

        public MainGameState()
        {
            graphics = new GraphicsDeviceManager(this);
            eManager = new EntityManager();
            aManager = new AssetManager();
            sManager = new StateManager(this);
            Content.RootDirectory = "Content";
        }

        private void AddInitialStates()
        {
            // Activate the first states
            sManager.AddState(new SplashScreenState());
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 480;

            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            sManager.LoadContent(new SplashScreenState());
        }

        protected override void UnloadContent()
        {
            sManager.UnloadContent(new SplashScreenState());
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
