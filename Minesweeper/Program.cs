using System;

namespace Minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Minesweeper";
            Console.ForegroundColor = ConsoleColor.Cyan;
            IGame game = new Game(8, 8, 8);
            game.Run();

        }

    }
}
