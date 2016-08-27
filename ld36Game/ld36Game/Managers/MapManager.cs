using Microsoft.Xna.Framework;

namespace ld36Game.Managers
{
    public static class MapManager
    {

        public static Rectangle getTile(int index)
        {
            if (index > 11) index = 0;

            int xStart = (index % 6) * 16;
            int yStart = (index / 6) * 16;

            Rectangle result = new Rectangle(xStart, yStart, 16, 16);
            return result;
        }

        public static int getTileCount()
        {
            return 300;
        }
    }
}
