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
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using ld36Game.Managers;

namespace ld36Game.GameStates
{
    public class BaseGameState
    {
        protected ContentManager content;

        [XmlIgnore]
        public Type Type;

        public BaseGameState()
        {
            Type = this.GetType();
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
