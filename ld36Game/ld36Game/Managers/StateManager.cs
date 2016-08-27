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
    public class StateManager
    {
        private BaseGameState currentState;
        private MainGameState game;

        private SplashScreenState splashScreenState;

        public StateManager(MainGameState parent)
        {
            splashScreenState = new SplashScreenState();

            game = parent;
            currentState = splashScreenState;
        } 

        public BaseGameState getCurrentState()
        {
            return currentState;
        }

        public void setSplashState()
        {
            currentState = splashScreenState;
        }
    }
}