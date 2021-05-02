namespace Minesweeper
{
    public class BoardConfig
    {
        public int Columns { get; set; }
        public int Rows { get; set; }
        public int Topmargin { get; set; }
        public int Bottommargin { get; set; }
        public int NumberOfMines { get; set; }

        public BoardConfig(int columns, int rows, int numberOfMines)
        {
            this.Columns = columns;
            this.Rows = rows;
            this.Topmargin = 3;
            this.Bottommargin = 2;
            this.NumberOfMines = numberOfMines;

        }
    }
}
