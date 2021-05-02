using System;
namespace Minesweeper
{
    public static class Utility
    {
        /// <summary>
        /// This method will clear the message on the console at cursol position
        /// </summary>
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        /// <summary>
        /// This method will write a message on console
        /// </summary>
        /// <param name="message">message to print</param>
        /// <param name="position">Postion where message needs to show</param>
        public static void WriteMessage(string message, int position)
        {
            Console.SetCursorPosition(0, position);
            Console.WriteLine(message.PadRight(100));
        }

    }
}
