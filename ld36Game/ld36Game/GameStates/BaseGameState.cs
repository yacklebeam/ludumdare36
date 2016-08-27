//-----------------------------------------------------------------------
// <summary>
//          Description: The base class that the game states inherit from.
//          Author: Trent Clostio
//          Contributing Authors: Jacob Troxel
// </summary>
//-----------------------------------------------------------------------

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ld36Game.GameStates
{
    public abstract class BaseGameState
    {
        private Texture2D background;

        public bool isInitialized;
        public BaseGameState()
        {
            isInitialized = true;
        }

        public abstract void draw(SpriteBatch spriteBatch);
        public abstract void update(GameTime gameTime);
    }
}