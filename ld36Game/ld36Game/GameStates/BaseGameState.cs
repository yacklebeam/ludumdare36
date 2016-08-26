//-----------------------------------------------------------------------
// <summary>
//          Description: The base class that the game states inherit from.
//          Author: Trent Clostio
//          Contributing Authors: Jacob Troxel
// </summary>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using ld36Game.Managers;

namespace ld36Game.GameStates
{
    public class BaseGameState : Game
    {
        GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private EntityManager eManager;
        private AssetManager aManager;

        protected ContentManager content;

        public Type Type;

        public BaseGameState()
        {
            Type = this.GetType();
            graphics = new GraphicsDeviceManager(this);
            eManager = new EntityManager();
            aManager = new AssetManager();
            Content.RootDirectory = "Content";
        }

        public virtual void LoadContent()
        {
            content = new ContentManager(StateManager.Instance.Content.ServiceProvider, "Content");
        }

        public virtual void UnloadContent()
        {
            content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
