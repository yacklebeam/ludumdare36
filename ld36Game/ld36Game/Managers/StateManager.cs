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
        public MainGameState game;

        private MainMenuState mainMenuState;
        private PlayingState playingState;

        public StateManager(MainGameState parent)
        {
            game = parent;

            mainMenuState = new MainMenuState(this);
            playingState = new PlayingState(this);
            currentState = mainMenuState;
        } 

        public BaseGameState getCurrentState()
        {
            return currentState;
        }

        public void setPlayingState()
        {
            currentState = playingState;
        }
    }
}