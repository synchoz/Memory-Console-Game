using System.Threading;

namespace Ex02_MemoryGame
{
    public class Game
    {
        public Board m_Board;
        public bool m_IsToQuit = !true;
        public Move Moves { get; set; } = new Move();
        public eTurn EnumPlayerTurn { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public bool IfGameOver()
        {
            bool ifGameOver = !true;
            int MaxScore = m_Board.BoardSizeRow * m_Board.BoardSizeCol / 2;

            if (MaxScore == Player1.Score + Player2.Score)
            {
                ifGameOver = true;
            }

            return ifGameOver;
        }

        public void UpdateScore()
        {
            if (EnumPlayerTurn == eTurn.P1)
            {
                Player1.Score += 1;
            }
            else
            {
                Player2.Score += 1;
            }
        }

        public bool TurnManager()
        {
            int milliseconds;
            bool flag = true;

            if (Board.m_LettersOfTurn.Count != 0)
            {
                if (Board.m_LettersOfTurn[0].CellLetter == Board.m_LettersOfTurn[1].CellLetter)
                {
                    UpdateScore();
                    if (EnumPlayerTurn == eTurn.P1)
                    {
                        UI.IdenticalCardsMsg(Player1.Name);
                        if (!IfGameOver())
                        {
                            UI.AnotherTurnMsg(Player1.Name);
                        }
                    }
                    else
                    {
                        UI.IdenticalCardsMsg(Player2.Name);
                        if (!IfGameOver())
                        {
                            UI.AnotherTurnMsg(Player2.Name);
                        }
                    }
                    milliseconds = 2800;
                    Thread.Sleep(milliseconds);
                    Board.m_LettersOfTurn.RemoveRange(0, 2);
                }
                else
                {
                    Moves.CancelMove(m_Board);
                    ChangeTurn();
                }
            }
            else
            {
                ChangeTurn();
            }

            return flag;
        }

        public void ChangeTurn()
        {
            if (EnumPlayerTurn == eTurn.P1)
            {
                EnumPlayerTurn = eTurn.P2;
            }
            else if (EnumPlayerTurn == eTurn.P2)
            {
                EnumPlayerTurn = eTurn.P1;
            }
        }
        public void NewGame()
        {
            m_Board.RestartBoard();
            Player1.Score = 0;
            Player2.Score = 0;
        }

        public Game EndGame()
        {
            Game startNewGame = new Game
            {
                Player1 = Player1,
                Player2 = Player2,
                m_Board = m_Board
            };
            startNewGame.m_Board.RestartBoard();
            startNewGame.Player1.Score = 0;
            startNewGame.Player2.Score = 0;
            startNewGame.m_IsToQuit = !true;

            return startNewGame;
        }
    }
}
