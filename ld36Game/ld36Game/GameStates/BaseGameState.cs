//-----------------------------------------------------------------------
// <summary>
//          Description: The base class that the game states inherit from.
//          Author: Trent Clostio
//          Contributing Authors: Jacob Troxel
// </summary>
//-----------------------------------------------------------------------

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using ld36Game.Managers;

namespace ld36Game.GameStates
{
    public abstract class BaseGameState : Game
    {
        private GraphicsDeviceManager graphics;
        private Texture2D background;
        private SpriteBatch spriteBatch;

        public bool isInitialized;
        public BaseGameState()
        {
            isInitialized = true;
        }

        protected virtual void Draw(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        public virtual void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}