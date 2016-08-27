//-----------------------------------------------------------------------
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
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;

using ld36Game.Managers;

namespace ld36Game.GameStates
{
    public class MainMenuState : BaseGameState
    {
        string[] menuItems = new string[2] {
            "Play Game",
            "Exit Game"
        };

        MainGameState mainGameState = new MainGameState();

        int selectedIndex;

        Color normal = Color.White;
        Color hlight = Color.Yellow;
        Color background = Color.Bisque;

        KeyboardState keyState;
        KeyboardState oldState;

        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        private const string TITLE = "SPEAR CHUCKERS";
        private SpriteFont titleFont;

        Vector2 position;
        float width = 0f;
        float height = 0f;

        public MainMenuState(StateManager p) : base(p)
        {
            
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
                    (mainGameState.Window.ClientBounds.Width - width) / 2,
                    (mainGameState.Window.ClientBounds.Height - height) / 2
                );
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
            oldState = keyState;
        }

        public override void draw(SpriteBatch spriteBatch)
        {
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