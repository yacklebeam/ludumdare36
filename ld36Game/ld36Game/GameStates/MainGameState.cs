﻿//-----------------------------------------------------------------------
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
using Microsoft.Xna.Framework.Content;

namespace ld36Game.GameStates
{
    public class MainGameState : Game
    {
        GraphicsDeviceManager graphics;
        public EntityManager eManager;
        public AssetManager aManager;
        public StateManager sManager;
        public LevelManager levelManager;
        SpriteBatch spriteBatch;

        public MainGameState()
        {
            graphics = new GraphicsDeviceManager(this);
            eManager = new EntityManager();
            aManager = new AssetManager();
            levelManager = new LevelManager();
            sManager = new StateManager(this);

            Content.RootDirectory = "Content";

            //CHANGE THIS TO CHANGE THE STARTING STATE
            sManager.setPlayingState();
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;

            graphics.ApplyChanges();

            base.Initialize();
        }

        public ContentManager getContent()
        {
            return Content;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            aManager.loadImageAsset("character-bits", "images/characters", Content);
            aManager.loadImageAsset("map-tiles", "images/maptiles", Content);
            aManager.loadImageAsset("normal-cursor", "images/cursor-normal", Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            sManager.getCurrentState().update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            Color mouseColor;
            if (ms.LeftButton == ButtonState.Pressed) mouseColor = Color.LightPink;
            else mouseColor = Color.White;
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null);
            sManager.getCurrentState().draw(spriteBatch);
            spriteBatch.Draw(aManager.getTexture("normal-cursor"), new Vector2(ms.X, ms.Y), mouseColor);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
