namespace Minesweeper
{

    public interface IGameHelper
    {
        void Flip();
        void CountAndPlaceWarnings(int row, int col);
    }
    public class GameHelper : IGameHelper
    {
        private BoardConfig boardConfig;
        private GameProperty gameProperties;


        public GameHelper(BoardConfig boardConfig, GameProperty gameProperties)
        {
            this.boardConfig = boardConfig;
            this.gameProperties = gameProperties;
        }

        /// <summary>
        /// This will check the mine and then flip the neighbour
        /// </summary>
        /// <returns></returns>
        public void Flip()
        {
            if (gameProperties.Mines[gameProperties.SelectedRow, gameProperties.SelectedColumn])
            {
                gameProperties.IsMineHit = true;
            }
            this.FlipNeighbour(gameProperties.SelectedRow, gameProperties.SelectedColumn);
        }

        /// <summary>
        /// This will count the warnings as per mine position and place them
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void CountAndPlaceWarnings(int row, int col)
        {
            int colOffset = col - 1;
            if (colOffset < 0)
                colOffset = 0;

            int rowOffset = row - 1;
            if (rowOffset < 0)
                rowOffset = 0;

            int rowMax = row + 1;
            if (rowMax >= boardConfig.Rows)
                rowMax = boardConfig.Rows - 1;

            int colMax = col + 1;
            if (colMax >= boardConfig.Columns)
                colMax = boardConfig.Columns - 1;
            var startCol = colOffset;
            for (; rowOffset <= rowMax; rowOffset++)
            {
                colOffset = startCol;
                for (; colOffset <= colMax; colOffset++)
                    gameProperties.Warnings[rowOffset, colOffset]++;
            }
        }


        #region privateMethods

        /// <summary>
        /// This will check the provide position is fliped or not
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private bool CheckIsFlipped(int row, int column)
        {
            if (gameProperties.IsMineHit || gameProperties.TotalLeft == 0)
                return true;
            return gameProperties.Flipped[row, column];
        }


        /// <summary>
        /// This will Update the Flippped postion to ture and 
        /// then flip the neighbours
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void FlipNeighbour(int row, int col)
        {
            if (col < 0 || col >= boardConfig.Columns)
                return;
            if (row < 0 || row >= boardConfig.Rows)
                return;
            if (CheckIsFlipped(row, col))
                return;
            gameProperties.Flipped[row, col] = true;
            gameProperties.TotalLeft--;

            if (gameProperties.Warnings[row, col] != 0)
                return;

            FlipNeighbour(row - 1, col - 1);
            FlipNeighbour(row - 1, col);
            FlipNeighbour(row - 1, col + 1);

            FlipNeighbour(row, col - 1);
            FlipNeighbour(row, col + 1);

            FlipNeighbour(row + 1, col - 1);
            FlipNeighbour(row + 1, col);
            FlipNeighbour(row + 1, col + 1);
        }

        #endregion
    }
}
