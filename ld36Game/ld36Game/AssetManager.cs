using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ld36Game
{
    class AssetManager
    {
        Dictionary<string, Texture2D> textures;

        public AssetManager()
        {
            textures = new Dictionary<string, Texture2D>();
        }

        public void loadImageAsset(string imageId, string imagePath, Microsoft.Xna.Framework.Content.ContentManager content)
        {
            if(!textures.ContainsKey(imageId))
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
