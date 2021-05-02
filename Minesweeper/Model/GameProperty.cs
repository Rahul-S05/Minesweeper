namespace Minesweeper
{
    public class GameProperty
    {
        public bool IsMineHit { get; set; }
        public int SelectedRow { get; set; }
        public int SelectedColumn { get; set; }
        public int TotalLeft { get; set; }
        public bool[,] Flipped { get; set; }
        public bool[,] Mines { get; set; }
        public int[,] Warnings { get; set; }

    }
}
