using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.Players;

namespace TicTacToe
{
    class GameOverException : Exception
    {
        private PlayerToken? winner;

        public GameOverException(PlayerToken? winner)
        {
            this.winner = winner;
        }

        public override string Message
        {
            get
            {
                if (winner == null)
                    return "It's a Draw!";
                else
                    return winner + " has already won!";
            }
        }
    }
}
