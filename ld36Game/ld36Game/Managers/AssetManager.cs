using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace ld36Game.Managers
{
    class AssetManager
    {
        Dictionary<string, Texture2D> textures;
        Dictionary<string, SpriteFont> fonts;

        public AssetManager()
        {
            textures = new Dictionary<string, Texture2D>();
            fonts = new Dictionary<string, SpriteFont>();
        }

        public void loadImageAsset(string imageId, string imagePath, ContentManager content)
        {
            if (!textures.ContainsKey(imageId))
            {
                Texture2D newTexture = content.Load<Texture2D>(imagePath);
                textures.Add(imageId, newTexture);
            }
        }
        public Texture2D getTexture(string textureId)
        {
            return textures[textureId];
        }
    }
}
