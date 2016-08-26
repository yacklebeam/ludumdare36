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
    public enum ScreenState
    {
        TransitionOn,
        Active,
        TransitionOff,
        Hidden,
    }

    public abstract class BaseGameState
    {
        public bool IsPopup
        {
            get { return isPopup; }
            protected set { isPopup = value; }
        }

        bool isPopup = false;

        public TimeSpan TransitionOnTime
        {
            get { return transitionOnTime; }
            protected set { transitionOnTime = value; }
        }

        TimeSpan transitionOnTime = TimeSpan.Zero;

        public float TransitionPosition
        {
            get { return transitionPosition; }
            protected set { transitionPosition = value; }
        }

        public TimeSpan TransitionOffTime
        {
            get { return transitionOffTime; }
            protected set { transitionOffTime = value; }
        }

        TimeSpan transitionOffTime = TimeSpan.Zero;

        float transitionPosition = 1;

        public float TransitionAlpha
        {
            get { return 1f - TransitionPosition; }
        }

        public ScreenState ScreenState
        {
            get { return screenState; }
            protected set { screenState = value; }
        }

        ScreenState screenState = ScreenState.TransitionOn;

        public bool IsExiting
        {
            get { return isExiting; }
            protected internal set { isExiting = value; }
        }

        bool isExiting = false;

        public bool IsActive
        {
            get
            {
                return !otherScreenHasFocus &&
                       (screenState == ScreenState.TransitionOn ||
                        ScreenState == ScreenState.Active);
            }
        }

        public virtual void Activate(bool instancePreserved) { }

        public virtual void Deactivate() { }

        public virtual void Unload() { }

        bool otherScreenHasFocus;

        public StateManager StateManager
        {
            get { return stateManager; }
            internal set { stateManager = value; }
        }

        StateManager stateManager;

        public virtual void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            this.otherScreenHasFocus = otherScreenHasFocus;

            if(isExiting)
            {
                screenState = ScreenState.TransitionOff;

                if(UpdateTransition(gameTime, transitionOffTime, 1))
                {
                    StateManager.RemoveState(this);
                }
            }
            else if (coveredByOtherScreen)
            {
                if (UpdateTransition(gameTime, transitionOffTime, 1))
                {
                    screenState = ScreenState.TransitionOff;
                }
                else
                {
                    screenState = ScreenState.Hidden;
                }
            }
            else
            {
                if (UpdateTransition(gameTime, transitionOnTime, -1))
                {
                    screenState = ScreenState.TransitionOn;
                }
                else
                {
                    screenState = ScreenState.Active;
                }
            }
        }

        bool UpdateTransition(GameTime gameTime, TimeSpan time, int direction)
        {
            float transitionDelta;

            if (time == TimeSpan.Zero)
                transitionDelta = 1;
            else
                transitionDelta = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / time.TotalMilliseconds);

            // update the transition position
            transitionPosition += transitionDelta * direction;

            // end of transition?
            if (((direction < 0) && (transitionPosition <= 0)) ||
                ((direction > 0) && (transitionPosition >= 1)))
            {
                transitionPosition = MathHelper.Clamp(transitionPosition, 0, 1);

                return false;
            }

            // otherwise it's still going....
            return true;
        }

        public virtual void HandleInput(GameTime gameTime, KeyboardState input) { }

        public virtual void Draw(GameTime gameTime) { }

        public void ExitScreen()
        {
            if (TransitionOffTime == TimeSpan.Zero)
            {
                StateManager.RemoveScreen(this);
            }
            else
            {
                isExiting = true;
            }
        }
    }
}
