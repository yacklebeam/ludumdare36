using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ld36Game
{
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        EntityManager eManager;
        AssetManager aManager;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            eManager = new EntityManager();
            aManager = new AssetManager();
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            //TEST, MOVE TO LEVEL LOADER
            Entity player = new Entity(new Vector2(50.0f, 50.0f), new Vector2(), new Vector2(), 75, 98, 0.0f, "player");
            Entity player2 = new Entity(new Vector2(400.0f, 50.0f), new Vector2(), new Vector2(), 75, 98, 0.0f, "enemy");
            eManager.addEntity(player);
            eManager.addEntity(player2);
            //END TEST
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            aManager.loadImageAsset("player", "images/test-texture", Content);
            aManager.loadImageAsset("enemy", "images/enemy-texture", Content);
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            for(int i = 0; i < eManager.getCount(); ++i)
            {
                Entity e = eManager.getEntity(i);
                if(!e.Equals(null))
                {
                    spriteBatch.Draw(aManager.getTexture(e.spriteId), e.position, null, Color.White, e.rotationAngle, e.center, 1.0f, SpriteEffects.None, 0.0f);
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
