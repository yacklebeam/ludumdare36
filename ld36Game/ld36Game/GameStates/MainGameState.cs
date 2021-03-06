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
        public Color mouseColor;
        public TowerManager tManager;
        SpriteBatch spriteBatch;

        public struct PlayerStats
        {
            public int gold;
            public int score;
            public int level;
            public int lives;
        }

        PlayerStats playerStats = new PlayerStats();

        public MainGameState()
        {
            graphics = new GraphicsDeviceManager(this);
            eManager = new EntityManager(this);
            aManager = new AssetManager();
            levelManager = new LevelManager();
            tManager = new TowerManager(this);
            sManager = new StateManager(this);
            resetPlayerStats();

            Content.RootDirectory = "Content";

            //CHANGE THIS TO CHANGE THE STARTING STATE
            sManager.setMainMenuState();
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
            aManager.loadImageAsset("menu-background", "images/MenuBackground", Content);
            aManager.loadFontAsset("menu-font", "fonts/MenuFont", Content);
            aManager.loadFontAsset("menu-font-hlight", "fonts/MenuFontHlight", Content);
            aManager.loadImageAsset("normal-cursor", "images/cursor-normal", Content);
            aManager.loadSoundAsset("intro-theme", "sounds/IntroTheme", Content);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            sManager.getCurrentState().update(gameTime);
            base.Update(gameTime);
        }

        public void setPlayerStats(int gold, int score, int level, int lives)
        {
            playerStats.gold = gold;
            playerStats.score = score;
            playerStats.level = level;
            playerStats.lives = lives;
        }

        public void resetPlayerStats()
        {
            playerStats.gold = 0;
            playerStats.score = 0;
            playerStats.level = 1;
            playerStats.lives = 3;
        }

        public PlayerStats getPlayerStats()
        {
            return playerStats;
        }

        public void setMouseColor(Color c)
        {
            mouseColor = c;
        }

        public Color getMouseColor()
        {
            return mouseColor;
        }

        protected override void Draw(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            mouseColor = Color.White;
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null);
            sManager.getCurrentState().draw(spriteBatch);
            spriteBatch.Draw(aManager.getTexture("normal-cursor"), new Vector2(ms.X, ms.Y), mouseColor);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
