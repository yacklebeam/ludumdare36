using Microsoft.Xna.Framework;

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
    }
}
