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
        enum GameStates
        {
            State1,
            State2,
            State3,
            State4
        }

        GameStates gameState;

        public Vector2 Dimensions { private set; get; }
        public ContentManager Content { private set; get; }
        public String GameStateTitle { private set; get; }
        public Color Background { private set; get; }

        public static BaseGameState currentState, newState;
        public GraphicsDevice graphicsDevice;
        public SpriteBatch spriteBatch;
        public SpriteFont spriteFont;

        private static StateManager instance;

        public static StateManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new StateManager(newState);

                return instance;
            }
        }

        public StateManager(object newState)
        {
            Dimensions = new Vector2(640, 480);
            GameStateTitle = String.Empty;
            Background = Color.Red;
            gameState = (GameStates)newState;
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
