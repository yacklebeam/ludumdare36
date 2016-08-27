using Microsoft.Xna.Framework;
using System;

namespace ld36Game.Managers
{
    public static class Indexes
    {
        public const int WHITE_BODY = 0;
        public const int BROWN_BODY = 2;
        public const int BLACK_BODY = 4;
        public const int LIZARD_BODY = 6;
    }


    public static class CharacterSpriteManager
    {
        public static Rectangle getBody(int index)
        {
            if (index > 7) index = 0;
            int xOffset = 0;
            int yOffest = 0;

            int startX = xOffset + (index % 2) * 17;
            int startY = yOffest + (index / 2) * 17;

            Rectangle Result = new Rectangle(startX, startY, 16, 16);
            return Result;
        }

        public static Rectangle getShirt(int index)
        {
            if (index > 119) index = 0;
            int xOffset = 103;
            int yOffest = 0;

            int startX = xOffset + (index % 12) * 17;
            int startY = yOffest + (index / 12) * 17;

            Rectangle Result = new Rectangle(startX, startY, 16, 16);
            return Result;
        }

        public static Rectangle getPants(int index)
        {
            if (index > 19) index = 0;
            int xOffset = 52;
            int yOffest = 0;

            int startX = xOffset + (index % 2) * 17;
            int startY = yOffest + (index / 2) * 17;

            Rectangle Result = new Rectangle(startX, startY, 16, 16);
            return Result;
        }

        public static Rectangle getHeadpiece(int index)
        {
            if (index > 35) index = 0;
            int xOffset = 477;
            int yOffest = 0;

            int startX = xOffset + (index % 4) * 17;
            int startY = yOffest + (index / 4) * 17;

            Rectangle Result = new Rectangle(startX, startY, 16, 16);
            return Result;
        }

        public static Rectangle getWeapon(int index)
        {
            if (index > 119) index = 0;
            int xOffset = 715;
            int yOffest = 0;

            int startX = xOffset + (index % 12) * 17;
            int startY = yOffest + (index / 12) * 17;

            Rectangle Result = new Rectangle(startX, startY, 16, 16);
            return Result;
        }

        public static Rectangle getShield(int index)
        {
            if (index > 71) index = 0;
            int xOffset = 562;
            int yOffest = 0;

            int startX = xOffset + (index % 8) * 17;
            int startY = yOffest + (index / 8) * 17;

            Rectangle Result = new Rectangle(startX, startY, 16, 16);
            return Result;
        }

        public static int[] getSpriteList(int id)
        {
            Random rnd = new Random();
            int skinColor = rnd.Next(0, 3);
            int skinIndex = skinColor * 2;

            if (id == 1) return new int[] { skinIndex, 65, 2, -1, -1, -1, 100, 1};
            if (id == 2) return new int[] { skinIndex, 101, 2, -1, 74, -1, 150, 1 };
            if (id == 3) return new int[] { skinIndex, 12, -1, -1, 32, -1, 200, 1 };
            if (id == 4) return new int[] { skinIndex, 39, 12, 24, 21, 48, 150, 2 };
            if (id == 5) return new int[] { skinIndex, 55, 2, -1, 74, 1, 150, 2 };
            if (id == 6) return new int[] { skinIndex, 55, 2, -1, 7, -1, 100, 1 };
            if (id == 7) return new int[] { skinIndex, 54, 4, 0, 74, 45, 150, 4 };
            if (id == 8) return new int[] { skinIndex, 72, -1, -1, 98, 48, 200, 5 };
            if (id == 9) return new int[] { skinIndex, 118, 4, 26, 74, 8, 150, 5 };
            if (id == 10) return new int[] { skinIndex, 118, 0, -1, 76, 21, 100, 10 };
            return new int[] { skinIndex, -1, -1, -1, -1, -1 };
        }
    }
}
