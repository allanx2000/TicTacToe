using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.Players;

namespace TicTacToe.ViewModels
{
    struct GameState
    {
        public bool Finished { get; set; }

        public PlayerToken? Winner { get; set; }

        public Player TurnPlayer { get; set; }
    }
}
