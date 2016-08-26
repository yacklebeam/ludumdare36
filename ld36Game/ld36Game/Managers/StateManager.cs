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

using ld36Game.GameStates;

namespace ld36Game.Managers
{
    public class StateManager
    {
        private static StateManager instance;
        public Vector2 Dimensions { private set; get; }
        public ContentManager Content { private set; get; }

        BaseGameState currentState, newState;
        public GraphicsDevice graphicsDevice;
        public SpriteBatch spriteBatch;

        public static StateManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new StateManager();

                return instance;
            }
        }

        public void ChangeStates(string stateName)
        {
            newState = (BaseGameState)Activator.CreateInstance(Type.GetType("ld36Game.GameStates." + stateName));
        }

        void Transition()
        {

        }

        public StateManager()
        {
            Dimensions = new Vector2(640, 480);
            currentState = new SplashScreenState();
        }


        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            currentState.LoadContent();
        }

        public void UnloadContent()
        {
            currentState.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            currentState.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentState.Draw(spriteBatch);
        }
    }
}
