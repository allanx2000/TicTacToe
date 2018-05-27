using Innouvous.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Players;
using TicTacToe.ViewModels;

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

        public GameState MakeMove(Board.Location location)
        {
            if (board.GameOver)
            {
                return GetState();
            }

            Player p = GetCurrentPlayer();
            bool placed = board.PlaceToken(location, p.Token);

            if (placed) //Change player
            {
                currentTurn = currentTurn == 0 ? 1 : 0;
            }

            return GetState();
        }

        public GameState GetState()
        {
            GameState state = new GameState();

            state.Finished = board.GameOver;
            if (state.Finished)
            {
                state.Finished = true;
                state.Winner = board.Winner;
            }
            else
            {
                state.TurnPlayer = GetCurrentPlayer();
            }

            return state;
        }

        private Player GetCurrentPlayer()
        {
            return currentTurn == 0 ? player1 : player2;
        }

        internal Board GetBoard()
        {
            return board;
        }

        public PlayerToken? CurrentPlayerToken
        {
            get
            {
                if (board.GameOver)
                    return null;
                else
                {
                    return GetCurrentPlayer().Token;
                }
            }
        }
    }
}
