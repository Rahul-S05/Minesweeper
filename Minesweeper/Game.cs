using System;
using System.Globalization;

namespace Minesweeper
{
    public interface IGame
    {
        void Run();
    }
    public class Game : IGame
    {
        private BoardConfig boardConfig;
        private IDrawHelper drawHelper;
        private IGameHelper gameHelper;
        private GameProperty gameProperties { get; set; }
        private bool isInputValid = true;
        public Game(int numberOfColumn, int numberOfRows, int numberOfMines)
        {
            gameProperties = new GameProperty();
            boardConfig = new BoardConfig(numberOfColumn, numberOfRows, numberOfMines);
            drawHelper = new DrawHelper();
            gameHelper = new GameHelper(this.boardConfig, this.gameProperties);
            InitialSetup(boardConfig);
            SetupMinesAndWarnings(boardConfig);
        }

        /// <summary>
        /// Main method to start the game
        /// </summary>
        public void Run()
        {
            try
            {
                while (!IsGameFinished)
                {
                    gameProperties.SelectedRow = -1;
                    gameProperties.SelectedColumn = -1;
                    drawHelper.Draw(gameProperties, boardConfig);
                    this.GetUserInput();
                    if (isInputValid)
                    {
                        gameHelper.Flip();
                        drawHelper.Draw(gameProperties, boardConfig);
                    }
                    else
                    {
                        Utility.WriteMessage("Please enter valid values", MessagePosition - 1);
                    }

                }
            }
            catch (ApplicationException)
            {
                Utility.WriteMessage("User quit game!", MessagePosition);
                return;

            }
            if (gameProperties.TotalLeft == 0)
            {
                Utility.WriteMessage("Congratulations you won...., Press any key to play again", MessagePosition);
                Console.ReadKey();
                this.Reset();
                this.Run();
            }
            else
            {
                Utility.WriteMessage("**Sorry you hit a mine, Press any key to play again", MessagePosition);
                Console.ReadKey();
                this.Reset();
                this.Run();
            }
        }


        #region privatehelpers
        private void InitialSetup(BoardConfig boardConfig)
        {
            gameProperties.Flipped = new bool[boardConfig.Rows, boardConfig.Columns];
            gameProperties.Mines = new bool[boardConfig.Rows, boardConfig.Columns];
            gameProperties.Warnings = new int[boardConfig.Rows, boardConfig.Columns];
            gameProperties.TotalLeft = boardConfig.Rows * boardConfig.Columns;
        }
        private void SetupMinesAndWarnings(BoardConfig boardConfig)
        {
            var newMines = 0;
            var random = new Random();
            while (newMines < boardConfig.NumberOfMines)
            {
                int randomColumn = random.Next(0, boardConfig.Columns);
                int randomRow = random.Next(0, boardConfig.Rows);
                if (gameProperties.Mines[randomRow, randomColumn] == true)
                    continue;
                gameProperties.Mines[randomRow, randomColumn] = true;
                gameHelper.CountAndPlaceWarnings(randomRow, randomColumn);
                newMines++;
            }

        }
        private void Reset()
        {
            gameProperties = new GameProperty();
            boardConfig = new BoardConfig(8, 8, 8);
            drawHelper = new DrawHelper();
            gameHelper = new GameHelper(this.boardConfig, this.gameProperties);
            InitialSetup(boardConfig);
            SetupMinesAndWarnings(boardConfig);
        }
        private void GetUserInput()
        {
            Utility.WriteMessage("Please enter a column and row (e.g. A7):", MessagePosition);
            var userSelection = Console.ReadLine().ToUpper();
            this.ValidateInput(userSelection);
            if (isInputValid)
                Console.SetCursorPosition(0, Console.CursorTop + 2);
            else
                Console.SetCursorPosition(0, Console.CursorTop - 1);

            Utility.ClearCurrentConsoleLine();
        }
        private void ValidateInput(string userSelection)
        {
            try
            {
                int currentRow = Convert.ToInt32(StringInfo.GetNextTextElement(userSelection, 1)) - 1;
                int currentColumn = (int)((ColumnEnum)Enum.Parse(typeof(ColumnEnum), StringInfo.GetNextTextElement(userSelection, 0)));

                if (currentRow >= 0 && currentRow < 9 && currentColumn < 8)
                {
                    gameProperties.SelectedRow = currentRow;
                    gameProperties.SelectedColumn = currentColumn;
                    isInputValid = true;
                    Console.SetCursorPosition(0, MessagePosition - 1);
                    Utility.ClearCurrentConsoleLine();
                }
                else
                {
                    isInputValid = false;
                }
            }
            catch (Exception)
            {
                isInputValid = false;
            }
        }
        private bool IsGameFinished { get { return gameProperties.IsMineHit || gameProperties.TotalLeft == 0; } }
        private int MessagePosition { get { return (boardConfig.Topmargin + boardConfig.Rows + boardConfig.Bottommargin); } }
        #endregion


    }
}