using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ld36Game.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ld36Game.GameStates
{
    class EndGameState : BaseGameState
    {
        private bool gameWin;
        private string endGameTitle;
        private Color endGameTitleColor;

        public EndGameState(StateManager p) : base(p)
        {
            gameWin = false;
            endGameTitle = "YOU LOSE";
            endGameTitleColor = Color.LightPink;
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            if(gameWin)
            {
                endGameTitle = "YOU WIN";
                endGameTitleColor = Color.White;
            }
            else
            {
                endGameTitle = "YOU LOSE";
                endGameTitleColor = Color.LightPink;
            }
            spriteBatch.DrawString(parent.game.aManager.getFont("menu-font"), endGameTitle, new Vector2(200.0f, 200.0f), endGameTitleColor);
        }

        public override void update(GameTime gameTime)
        {
            if(Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                parent.setMainMenuState();
            }
        }

        public void setEndGameType(bool win)
        {
            gameWin = win;
        }
    }
}
