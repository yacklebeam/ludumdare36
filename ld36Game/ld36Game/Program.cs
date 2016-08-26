using System;
using ld36Game.GameStates;

namespace ld36Game
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new BaseGameState())
                game.Run();
        }
    }
#endif
}
