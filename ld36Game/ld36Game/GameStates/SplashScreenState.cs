using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ld36Game.GameStates
{
    public class SplashScreenState : BaseGameState
    {
        Texture2D splashImage;
        string path;

        public override void LoadContent()
        {
            base.LoadContent();

            path = "images/SplashScreen";
            splashImage = content.Load<Texture2D>(path);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(splashImage, Vector2.Zero, Color.White);
        }
    }
}
