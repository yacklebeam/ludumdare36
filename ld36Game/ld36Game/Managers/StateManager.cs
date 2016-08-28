//-----------------------------------------------------------------------
// <summary>
//          Description: Manages the game's states.
//          Author: Trent Clostio
//          Contributing Authors: Jacob Troxel
// </summary>
//-----------------------------------------------------------------------

using ld36Game.GameStates;

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

        public void setMainMenuState()
        {
            currentState = mainMenuState;
        }
    }
}