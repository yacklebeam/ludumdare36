﻿//-----------------------------------------------------------------------
// <summary>
//          Description: Helper for generating the game's main menu.
//          Author: Trent Clostio
//          Contributing Authors: Jacob Troxel
// </summary>
//-----------------------------------------------------------------------

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

using ld36Game.Managers;

namespace ld36Game.GameStates
{
    public class MainMenuState : BaseGameState
    {
        string[] menuItems = new string[2] {
            "Play Game",
            "Exit Game"
        };

        int selectedIndex;

        Color normal = Color.White;
        Color hlight = Color.Yellow;

        KeyboardState keyState;
        KeyboardState oldState;
        AssetManager aManager;

        SpriteFont hlightFont;
        SpriteFont normalFont;
        SpriteFont spriteFont;
        SoundEffect introTheme;

        public String GameMode;

        Vector2 position;
        float width = 0f;
        float height = 0f;

        public MainMenuState(StateManager p) : base(p)
        {
            aManager = parent.game.aManager;
            playSound();
        }

        public void playSound()
        {
            try
            {
                introTheme = aManager.getSound("intro-theme");
                introTheme.Play();
            }
            catch
            {
                new ArgumentNullException();
            }
        }

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                if (selectedIndex < 0)
                    selectedIndex = 0;
                if (selectedIndex >= menuItems.Length)
                    selectedIndex = menuItems.Length - 1;
            }
        }

        /// <summary>Measures the menu for indexing.</summary>
        private Vector2 MeasureMenu()
        {
            height = 0;
            width = 0;
            Vector2 size = new Vector2();

            foreach (string item in menuItems)
            {
                try
                {
                    size = spriteFont.MeasureString(item);
                    if (size.X > width)
                        width = size.X;
                    height += spriteFont.LineSpacing + 5;
                }
                catch
                {
                    new ArgumentNullException();
                }
            }

            position = new Vector2(
                    (800 - width) / 2,
                    (600 - height) / 2
                );

            return position;
        }

        /// <summary>Checks the keys.</summary>
        /// <param name="theKey">The key.</param>
        /// <returns></returns>
        private bool CheckKeys(Keys theKey)
        {
            return keyState.IsKeyUp(theKey) &&
                   oldState.IsKeyDown(theKey);
        }

        /// <summary>Updates the specified game time.</summary>
        /// <param name="gameTime">The game time.</param>
        public override void update(GameTime gameTime)
        {
            keyState = Keyboard.GetState();

            if (CheckKeys(Keys.Down))
            {
                selectedIndex++;
                if (selectedIndex == menuItems.Length)
                    selectedIndex = 0;
            }
            if (CheckKeys(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = menuItems.Length - 1;
            }

            if (CheckKeys(Keys.Enter))
            {
                if (selectedIndex == 0)
                    parent.setPlayingState();
                else if (selectedIndex == 1)
                    parent.game.Exit();
            }

            oldState = keyState;
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            Vector2 location = MeasureMenu();
            Color tint;

            spriteBatch.Draw(aManager.getTexture("menu-background"), new Rectangle(0, 0, 800, 600), Color.White);
            
            normalFont = aManager.getFont("menu-font");
            hlightFont = aManager.getFont("menu-font-hlight");

            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == selectedIndex)
                {
                    tint = hlight;
                    spriteFont = hlightFont;
                }
                else
                {
                    tint = normal;
                    spriteFont = normalFont;
                }

                spriteBatch.DrawString(spriteFont,
                                       menuItems[i],
                                       location,
                                       tint);

                location.Y += spriteFont.LineSpacing + 5;
            }
        }
    }
}