using System;
using System.Threading;



namespace Ex02_MemoryGame
{
    public class Move
    {

        public void RandomRowsCols(out int io_Rows, int i_RowSize, out int io_Cols, int i_ColSize)
        {


            Random r = new Random();

            io_Rows = r.Next(0, i_RowSize + 1);
            if (io_Rows != 0)
            {
                io_Rows--;
            }
            io_Cols = r.Next(0, i_ColSize + 1);
            if (io_Cols != 0)
            {
                io_Cols--;
            }
        }


        internal void PCMakeMove(Board io_Board,int rIntRow,int rIntCol)
        {
            int milliseconds = 2800;

            Thread.Sleep(milliseconds);
            MakeMove(rIntRow, rIntCol, io_Board);
            Board.m_GameBoardFull[rIntRow, rIntCol].IsFlipped = true;
            Board.m_GameBoardFull[rIntRow, rIntCol].Iswatched = true;
            Board.m_LettersOfTurn.Add(Board.m_Letter = new Cell(rIntRow, rIntCol, Board.m_GameBoardFull[rIntRow, rIntCol].CellLetter, !true, true, true));
        }
        internal void PCMove(Board io_Board)
        {
            bool PcValidFirstGuess = !true;

            while (!PcValidFirstGuess)
            {
                RandomRowsCols(out int rIntRow, io_Board.BoardSizeRow, out int rIntCol, io_Board.BoardSizeCol);
                if (Board.m_GameBoardFull[rIntRow, rIntCol].IsFlipped == !true)
                {
                    PCMakeMove(io_Board, rIntRow, rIntCol);
                    PcValidFirstGuess = true;
                }
            }
        }

        public void PCMove2(Board io_Board) //Using AI - for level 2 and level 3
        {
            int m_CounterForLoop = 0;
            int m_MaxCounterForLoop = 0;
            bool PcValidSecondGuess = !true;
            int milliseconds = 800;

            if (io_Board.GameLevel == 3)
            {
                for (int i = 0; i < io_Board.BoardSizeRow; i++)
                {
                    for (int j = 0; j < io_Board.BoardSizeCol; j++)
                    {
                        if ((Board.m_LettersOfTurn[0].CellLetter == Board.m_GameBoardFull[i, j].CellLetter) && (Board.m_GameBoardFull[i, j].Iswatched == true) && (Board.m_GameBoardFull[i, j].IsFlipped == !true) && (PcValidSecondGuess == !true))
                        {
                            PCMakeMove(io_Board, i, j);
                            PcValidSecondGuess = true;
                        }
                    }
                }
            }
            if (io_Board.GameLevel == 2)
            {
                m_MaxCounterForLoop = 18;
            }
            else if (io_Board.GameLevel == 3)
            {
                m_MaxCounterForLoop = 36;
            }
            while (!PcValidSecondGuess)
            {
                RandomRowsCols(out int rIntRow2, io_Board.BoardSizeRow, out int rIntCol2, io_Board.BoardSizeCol);
                if (Board.m_GameBoardFull[rIntRow2, rIntCol2].IsFlipped == !true)
                {
                    Thread.Sleep(milliseconds);
                    if (Board.m_GameBoardFull[rIntRow2, rIntCol2].Iswatched == !true)
                    {
                        PCMakeMove(io_Board, rIntRow2, rIntCol2);
                        PcValidSecondGuess = true;
                    }
                    else
                    {
                        if (m_CounterForLoop < m_MaxCounterForLoop && Board.m_GameBoardFull[rIntRow2, rIntCol2].CellLetter != Board.m_LettersOfTurn[0].CellLetter) //For level 2 or 3
                        {
                            PcValidSecondGuess = !true;
                            m_CounterForLoop++;
                        }
                        else
                        {
                            MakeMove(rIntRow2, rIntCol2, io_Board);
                            Board.m_GameBoardFull[rIntRow2, rIntCol2].IsFlipped = true;
                            Board.m_LettersOfTurn.Add(Board.m_Letter = new Cell(rIntRow2, rIntCol2, Board.m_GameBoardFull[rIntRow2, rIntCol2].CellLetter, !true, true, true));
                            PcValidSecondGuess = true;
                        }
                    }
                }
            }
        }

        public void MakeMove(int i_Row, int i_Col, Board io_Board)
        {
            io_Board.GameBoard[i_Row, i_Col] = Board.m_GameBoardFull[i_Row, i_Col].CellLetter;
        }

        public void CancelMove(Board io_Board)
        {
            int x1, x2, y1, y2;
            int milliseconds = 2800;

            io_Board.GameBoard[Board.m_LettersOfTurn[0].CoordinateX, Board.m_LettersOfTurn[0].CoordinateY] = ' ';
            io_Board.GameBoard[Board.m_LettersOfTurn[1].CoordinateX, Board.m_LettersOfTurn[1].CoordinateY] = ' ';
            x1 = Board.m_LettersOfTurn[0].CoordinateX;
            y1 = Board.m_LettersOfTurn[0].CoordinateY;
            x2 = Board.m_LettersOfTurn[1].CoordinateX;
            y2 = Board.m_LettersOfTurn[1].CoordinateY;
            Board.m_GameBoardFull[x1, y1].IsFlipped = !true;
            Board.m_GameBoardFull[x2, y2].IsFlipped = !true;
            UI.NotIdenticalMsg();
            Thread.Sleep(milliseconds);
            Board.m_LettersOfTurn.RemoveRange(0, 2);
            Ex02.ConsoleUtils.Screen.Clear();
            UI.DrawTheBoard(io_Board);
        }
    }
}
