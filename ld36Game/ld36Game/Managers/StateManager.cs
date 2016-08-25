//-----------------------------------------------------------------------
// <summary>
//          Description: Manages the game's states.
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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ld36Game.Managers
{
    public class StateManager
    {
        private static StateManager instance;
        public Vector2 Dimensions { private set; get; }

        public static StateManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new StateManager();

                return instance;
            }
        }

        public StateManager()
        {
            Dimensions = new Vector2(640, 480);
        }


        public void LoadContent(ContentManager Content)
        {

        }

        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
