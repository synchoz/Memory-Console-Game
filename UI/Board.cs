using System;
using System.Collections.Generic;

namespace Ex02_MemoryGame
{
    public class Board
    {
        public static Cell[,] m_GameBoardFull;
        private static List<Cell> m_LettersBank; //Fill list with double of each letter - by the size that givven.   [example: A A B B C C D D]
        public static List<Cell> m_LettersOnBoard;  //Fill list with cells from 'm_lettersBank' -> but now random places. [example: D A B A C D B C]
        public static List<Cell> m_LettersOfTurn; // Each turn fill this list with 2 cells letters the choosen at the turn. [example: D A]
        public static Cell m_Letter;

        public Board(int i_BoardSizeRow, int i_BoardSizeCol, int i_level)
        {
            BoardSizeRow = i_BoardSizeRow;
            BoardSizeCol = i_BoardSizeCol;
            GameLevel = i_level;
            GameBoard = new char[BoardSizeRow, BoardSizeCol];
            RestartBoard();
        }
        public int BoardSizeRow { get; } = 0;
        public int BoardSizeCol { get; } = 0;
        public int GameLevel { get; } = 0;
        public char[,] GameBoard { get; set; }
        public Cell[,] GamgeBoardFull
        {
            get => m_GameBoardFull;
            set => m_GameBoardFull = value;
        }
        public bool IsEmptyCell(int i_Row, int i_Col)
        {
            bool isEmptyCell = true;

            if (GameBoard[i_Row, i_Col] != (char)eCellLetter.EMPTY)
            {
                isEmptyCell = !true;
            }

            return isEmptyCell;
        }

        public bool IsInBorders(int i_Row, int i_Col)
        {
            bool inBorders = true;

            if (i_Row < 0 || i_Row >= BoardSizeRow || i_Col < 0 || i_Col >= BoardSizeCol)
            {
                inBorders = !true;
            }

            return inBorders;
        }

        public void RestartBoard()
        {
            GamgeBoardFull = new Cell[BoardSizeRow, BoardSizeCol];
            m_LettersBank = new List<Cell>();
            m_LettersOnBoard = new List<Cell>();
            m_LettersOfTurn = new List<Cell>();
            for (int i = 0; i < BoardSizeRow; i++)
            {
                for (int j = 0; j < BoardSizeCol; j++)
                {
                    GameBoard[i, j] = (char)eCellLetter.EMPTY;
                    m_Letter = new Cell(100, 100, ' ', !true, !true, !true);
                    GamgeBoardFull[i, j] = m_Letter;
                }
            }

            fillingLetters();
        }


        private void fillingLetters()
        {
            char ascendingLetters = 'A';
            int rowFinal = 0;
            int colFinal = 0;
            int place;

            for (int i = 0; i < BoardSizeRow * BoardSizeCol; i++)
            {
                m_Letter = new Cell(100, 100, ascendingLetters, !true, !true, !true);
                m_LettersBank.Add(m_Letter);
                m_LettersBank.Add(m_Letter);
                ascendingLetters++;
                i++;
            }
            while (m_LettersOnBoard.Count != BoardSizeRow* BoardSizeCol)
            {

               
                Random r = new Random();
                int rInt = r.Next(0, (BoardSizeRow * BoardSizeCol)+1);

                if (rInt!= 0)
                {
                    rInt--;
                }

                if (m_LettersBank[rInt].IsOccupied == !true)
                {
                    m_Letter = new Cell(m_LettersBank[rInt].CoordinateX, m_LettersBank[rInt].CoordinateY, m_LettersBank[rInt].CellLetter, true, !true, !true);
                    m_LettersBank[rInt] = m_Letter;
                    m_Letter = new Cell(rowFinal, colFinal, m_LettersBank[rInt].CellLetter, true, !true, !true);
                    m_LettersOnBoard.Add(m_Letter);
                    if (colFinal == BoardSizeCol)
                    {
                        colFinal = 0;
                        rowFinal++;
                    }
                    else
                    {
                        colFinal++;
                    }
                }
            }

            for (int i = 0; i < BoardSizeRow; i++)
            {
                for (int j = 0; j < BoardSizeCol; j++)
                {
                    if (i == 0)
                    {
                        place = j;
                    }
                    else
                    {
                        place = j == 0 ? i * BoardSizeCol : (i * BoardSizeCol) + j;
                    }
                    m_GameBoardFull[i,j] = m_LettersOnBoard[place]; //insert letters to 2D matrix - From the mixed list
                }
            }
        }
    }
}
