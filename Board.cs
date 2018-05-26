using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.Players;

namespace TicTacToe
{
    public class Board
    {
        public struct Location
        {
            public int Row { get; private set; }
            public int Column { get; private set; }

            public Location(int r, int c) : this()
            {
                Row = r;
                Column = c;
            }
        }

        public PlayerToken? Winner { get; set; }


        private PlayerToken?[,] board = new PlayerToken?[3,3];

        private Dictionary<PlayerToken, List<Location>> moves = new Dictionary<PlayerToken, List<Location>>();

        public Board()
        {
        }

        /// <summary>
        /// Places a token and evaluates the board
        /// </summary>
        /// <param name="location"></param>
        /// <param name="token"></param>
        /// <returns>Whether a token was placed</returns>
        public bool PlaceToken(Location location, PlayerToken token)
        {
            if (GameOver || IsBoardFull()) //TODO: Draw?
                throw new GameOverException(Winner);

            if (!IsEmpty(location))
                return false;
            else
            {
                board[location.Row, location.Column] = token;

                if (!moves.ContainsKey(token))
                    moves.Add(token, new List<Location>());

                moves[token].Add(location);

                EvaluateBoard();

                return true;
            }
        }



        private void EvaluateBoard()
        {
            PlayerToken? winner = null;
            
            EvaluateRow(0, ref winner);
            EvaluateRow(1, ref winner);
            EvaluateRow(2, ref winner);
            
            EvaluateCol(0, ref winner);
            EvaluateCol(1, ref winner);
            EvaluateCol(2, ref winner);

            EvaluateDiagonals(ref winner);

            if (winner != null)
            {
                Winner = winner;
                GameOver = true;
            }
            else if (IsBoardFull())
            {
                GameOver = true;
            }

        }

        private void EvaluateDiagonals(ref PlayerToken? winner)
        {
            PlayerToken? center = board[1,1];

            if (center == null)
                return;

            if (center == board[0, 0] && center == board[2, 2])
                winner = center;
            else if (center == board[2, 0] && center == board[0, 2])
                winner = center;
        }

        private void EvaluateCol(int col, ref PlayerToken? winner)
        {
            PlayerToken? lastToken = null;

            for (int r = 0; r < 3; r++)
            {
                var token = board[r, col];
                if (token == null)
                    return;
                else if (lastToken != null && lastToken != token)
                    return;
                else
                    lastToken = token;
            }

            winner = lastToken;    
        }

        private void EvaluateRow(int row, ref PlayerToken? winner)
        {
            PlayerToken? lastToken = null;

            for (int c = 0; c < 3; c++)
            {
                var token = board[row, c];
                if (token == null)
                    return;
                else if (lastToken != null && lastToken != token)
                    return;
                else
                    lastToken = token;
            }

            winner = lastToken;
        }

        private bool isFull = false;
        public bool GameOver { get; private set; }
        private bool IsBoardFull()
        {
            if (isFull)
                return true;
            else
            {
                int total = 0;

                foreach (var list in moves.Values)
                {
                    total += list.Count;
                }

                isFull = total == 9;

                if (isFull)
                    GameOver = true;

                return isFull;
            }
        }

        public bool IsEmpty(Location location)
        {
            return board[location.Row, location.Column] == null;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int r = 0; r < 3; r++)
            {
                sb.AppendLine(string.Join("|", 
                    Str(board[r,0]),
                    Str(board[r,1]),
                    Str(board[r,2])
                    ));
            }

            sb.AppendLine("Winner: " + Winner);

            return sb.ToString();
        }

        private string Str(PlayerToken? str)
        {
            if (str == null)
                return "-";
            else
                return str.ToString();
        }
    }
}
