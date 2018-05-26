using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Players
{
    public abstract class AIPlayer : Player
    {
        public AIPlayer(PlayerToken type)
            : base(type)
        {
        }

        public abstract Board.Location GetMove(Board board);
    }
}
