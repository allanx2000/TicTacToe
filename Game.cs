using Innouvous.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Players;

namespace TicTacToe
{
    class Game
    {
        private Board board = new Board();

        private Player player1;
        private Player player2;

        int currentTurn = 0; //0,1

        public Game(Player p1, Player p2)
        {
            player1 = p1;
            player2 = p2;
        }

        public void NewGame()
        {
            board = new Board();
        }

        public void MakeMove(Board.Location location)
        {
            if (board.GameOver)
                return;

            Player p = GetCurrentPlayer();

            bool placed = board.PlaceToken(location, p.Token);

            if (placed)
            {
                if (board.GameOver)
                {
                    //MessageBoxFactory.ShowInfo("Winner: " + board.Winner, "Game Over");
                    //TODO: Notify Winner
                }
                else
                {
                    Next();
                }
            }

            
        }

        private Player GetCurrentPlayer()
        {
            return currentTurn == 0 ? player1 : player2;
        }


        //TODO: Board shouldnt care about AI, Handled by Game/GUI
        private void Next()
        {
            currentTurn = currentTurn == 0 ? 1 : 0;

            Player p = GetCurrentPlayer();
            if (p is AIPlayer)
            {
                if (board.GameOver)
                    return;

                var move = ((AIPlayer)p).GetMove(board);
                MakeMove(move);
            }
        }

        internal Board GetBoard()
        {
            return board;
        }
    }
}
