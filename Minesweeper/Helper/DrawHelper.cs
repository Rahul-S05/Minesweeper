using System;
namespace Minesweeper
{
    public interface IDrawHelper
    {
        void Draw(GameProperty gameProperties, BoardConfig boardConfig);
    }
    public class DrawHelper : IDrawHelper
    {

        /// <summary>
        /// This will draw the game grid on console
        /// </summary>
        /// <param name="gameProperties"></param>
        /// <param name="boardConfig"></param>
        public void Draw(GameProperty gameProperties, BoardConfig boardConfig)
        {
            this.PrintHeader(gameProperties.SelectedRow, gameProperties.SelectedColumn);
            Console.SetCursorPosition(boardConfig.Topmargin, boardConfig.Topmargin);
            Console.Write("  ");
            for (var column = 0; column < boardConfig.Columns; column++)
            {
                Console.Write($"{((ColumnEnum)column).ToString()} ");
            }

            for (var row = 0; row < boardConfig.Rows; row++)
            {
                Console.CursorTop++;
                DrawRowsAndData(row, boardConfig, gameProperties);
            }
            Console.WriteLine("".PadRight(10));
        }

        private void DrawRowsAndData(int row, BoardConfig boardConfig, GameProperty gameProperties)
        {
            var orgColor = Console.BackgroundColor;
            Console.CursorLeft = boardConfig.Topmargin;
            Console.Write($"{row + 1} ");
            for (var column = 0; column < boardConfig.Columns; column++)
            {
                bool checkFlippedStatus = (gameProperties.IsMineHit || gameProperties.TotalLeft == 0) ? true : gameProperties.Flipped[row, column];
                if (checkFlippedStatus)
                {
                    if (gameProperties.Mines[row, column])
                    {
                        if (row == gameProperties.SelectedRow && column == gameProperties.SelectedColumn)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.Write("m ");
                            Console.BackgroundColor = orgColor;
                        }
                        else
                            Console.Write("m ");
                    }
                    else
                    {
                        if (gameProperties.Warnings[row, column] != 0)
                            Console.Write($"{gameProperties.Warnings[row, column]} ");
                        else
                            Console.Write("0 ");
                    }
                }
                else
                    Console.Write("* ");
            }
        }

        /// <summary>
        /// This will print the selected Column and Row when game ends
        /// </summary>
        /// <param name="userCurrentRow"></param>
        /// <param name="userCurrentColumn"></param>
        private void PrintHeader(int userCurrentRow, int userCurrentColumn)
        {
            Console.SetCursorPosition(0, 0);
            if (userCurrentColumn != -1 && userCurrentRow != -1)
            {
                Console.WriteLine("You have selected");
                Console.WriteLine($"Column: {((ColumnEnum)userCurrentColumn).ToString()}");
                Console.WriteLine($"Row:    {userCurrentRow + 1}");
            }
            else
            {
                Console.WriteLine("                                 ");
                Console.WriteLine("            ");
                Console.WriteLine("            ");
            }
        }
    }
}
