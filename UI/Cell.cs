namespace Ex02_MemoryGame
{
    public class Cell
    {
        public Cell(int i_X, int i_Y, char i_Letter,
                    bool i_Occupied, bool i_Flip,
                    bool i_Watched)
        {
            CoordinateX = i_X;
            CoordinateY = i_Y;
            CellLetter = i_Letter;
            IsOccupied = i_Occupied;// for filling random letters from the bank
            IsFlipped = i_Flip; //i_Flip 2 cards on each turn, at the end of the turn-> Flip back
            Iswatched = i_Watched; //If flipped once - no marked as watch for the rest of the game
        }

        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public char CellLetter { get; set; }
        public bool IsOccupied { get; set; }
        public bool IsFlipped { get; set; }
        public bool Iswatched { get; set; }
    }
}
