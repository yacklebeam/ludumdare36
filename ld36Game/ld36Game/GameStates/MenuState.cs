//-----------------------------------------------------------------------
// <summary>
//          Description: Helper for generating the game's main menu
//          Author: Trent Clostio
//          Contributing Authors: Jacob Troxel
// </summary>
//-----------------------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;

namespace ld36Game.GameStates
{
    public class MenuState : Microsoft.Xna.Framework.DrawableGameComponent
    {
        string[] menuItems;
        int selectedIndex;

        Color normal = Color.White;
        Color hlight = Color.Yellow;

        KeyboardState keyState;
        KeyboardState oldState;

        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        Vector2 position;
        float width = 0f;
        float height = 0f;

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

        /// <summary>Initializes a new instance of the <see cref="MenuState"/> class.</summary>
        /// <param name="game">The game.</param>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="spriteFont">The sprite font.</param>
        /// <param name="menuItems">The menu items.</param>
        public MenuState(Game game,
                        SpriteBatch spriteBatch,
                        SpriteFont spriteFont,
                        string[] menuItems) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.spriteFont = spriteFont;
            this.menuItems = menuItems;
            MeasureMenu();
        }

        /// <summary>Measures the menu for indexing.</summary>
        private void MeasureMenu()
        {
            height = 0;
            width = 0;
            foreach (string item in menuItems)
            {
                Vector2 size = spriteFont.MeasureString(item);
                if (size.X > width)
                    width = size.X;
                height += spriteFont.LineSpacing + 5;
            }

            position = new Vector2(
                (Game.Window.ClientBounds.Width - width) / 2,
                (Game.Window.ClientBounds.Height - height) / 2
                );
        }

        public override void Initialize()
        {
            base.Initialize();
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
        public override void Update(GameTime gameTime)
        {
            keyState = Keyboard.GetState();

            if(CheckKeys(Keys.Down))
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
            base.Update(gameTime);

            oldState = keyState;
        }

        /// <summary>Draws the specified game time.</summary>
        /// <param name="gameTime">The game time.</param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Vector2 location = position;
            Color tint;

            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == selectedIndex)
                    tint = hlight;
                else
                    tint = normal;

                spriteBatch.DrawString(spriteFont,
                                       menuItems[i],
                                       location,
                                       tint);

                location.Y += spriteFont.LineSpacing + 5;
            }
        }
    }
}
