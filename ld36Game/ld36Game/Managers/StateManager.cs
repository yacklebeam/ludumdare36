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
using Microsoft.Xna.Framework.Input;

using ld36Game.GameStates;
using System.Diagnostics;

namespace ld36Game.Managers
{
    public class StateManager : DrawableGameComponent
    {
        List<BaseGameState> states = new List<BaseGameState>();
        List<BaseGameState> tempScreensList = new List<BaseGameState>();

        KeyboardState input = new KeyboardState();

        SpriteBatch spriteBatch;
        SpriteFont font;
        Texture2D blankTexture;

        bool isInitialized;

        bool traceEnabled;

        public StateManager(Game game) : base(game) { }

        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        public SpriteFont Font
        {
            get { return font; }
        }

        // If true, gives list of screens each time the list is updated.
        // Useful for checking for add/remove timing errors.
        public bool TraceEnabled
        {
            get { return traceEnabled; }
            set { traceEnabled = value; }
        }

        public Texture2D BlankTexture
        {
            get { return blankTexture; }
        }

        public override void Initialize()
        {
            base.Initialize();

            isInitialized = true;
        }

        protected override void LoadContent()
        {
            ContentManager content = Game.Content;

            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = content.Load<SpriteFont>("fonts/MenuFont");
            blankTexture = content.Load<Texture2D>("images/SplashScreen");

            foreach(BaseGameState state in states)
            {
                state.Activate(false);
            }
        }

        protected override void UnloadContent()
        {
            foreach(BaseGameState state in states)
            {
                state.Unload();
            }
        }

        public override void Update(GameTime gameTime)
        {
            tempScreensList.Clear();

            foreach(BaseGameState state in states)
            {
                tempScreensList.Add(state);
            }

            bool otherScreenHasFocus = !Game.IsActive;
            bool coveredByOtherScreen = false;

            while (tempScreensList.Count > 0)
            {
                BaseGameState state = tempScreensList[tempScreensList.Count - 1];

                tempScreensList.RemoveAt(tempScreensList.Count - 1);

                state.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

                if (state.ScreenState == ScreenState.TransitionOn ||
                    state.ScreenState == ScreenState.Active)
                {
                    if(!otherScreenHasFocus)
                    {
                        otherScreenHasFocus = true;
                    }

                    if (!state.IsPopup)
                        coveredByOtherScreen = true;
                }
            }

            if (traceEnabled)
                TraceScreens();
        }

        void TraceScreens()
        {
            List<string> screenNames = new List<string>();

            foreach (BaseGameState state in states)
                screenNames.Add(state.GetType().Name);

            Debug.WriteLine(string.Join(", ", screenNames.ToArray()));
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (BaseGameState state in states)
            {
                if (state.ScreenState == ScreenState.Hidden)
                    continue;

                state.Draw(gameTime);
            }
        }

        public void AddState(BaseGameState state)
        {
            state.StateManager = this;
            state.IsExiting = false;

            if(isInitialized)
            {
                state.Activate(false);
            }

            states.Add(state);
        }

        public void RemoveState(BaseGameState state)
        {
            if (isInitialized)
            {
                state.Unload();
            }

            states.Remove(state);
            tempScreensList.Remove(state);
        }

        public BaseGameState[] GetStates()
        {
            return states.ToArray();
        }

        public void FadeBackBufferToBlack(float alpha)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(blankTexture, GraphicsDevice.Viewport.Bounds, Color.Black * alpha);
            spriteBatch.End();
        }

        public void Deactivate()
        {
            // Make a copy of the master screen list
            tempScreensList.Clear();
            foreach(BaseGameState state in states)
            {
                tempScreensList.Add(state);
            }
            foreach(BaseGameState state in states)
            {
                state.Deactivate();
            }
        }

        public bool Activate(bool instancePreserved)
        {
            if(instancePreserved)
            {
                tempScreensList.Clear();

                foreach(BaseGameState state in states)
                {
                    tempScreensList.Add(state);
                }
                foreach(BaseGameState state in states)
                {
                    state.Activate(true);
                }
            }

            return true;
        }
    }
}
