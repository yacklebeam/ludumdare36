//-----------------------------------------------------------------------
// <summary>
//          Description: The base class that the game states inherit from.
//          Author: Trent Clostio
//          Contributing Authors: Jacob Troxel
// </summary>
//-----------------------------------------------------------------------

using ld36Game.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ld36Game.GameStates
{
    public abstract class BaseGameState
    {
        private Texture2D background;
        protected StateManager parent;

        public bool isInitialized;
        public BaseGameState(StateManager p)
        {
            isInitialized = true;
            parent = p;
        }

        public abstract void draw(SpriteBatch spriteBatch);
        public abstract void update(GameTime gameTime);
    }
}