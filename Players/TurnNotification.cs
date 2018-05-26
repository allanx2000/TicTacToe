using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe.Players
{
    public class TurnNotificationArgs : EventArgs
    {
        private Player player;

        public TurnNotificationArgs(Player player)
        {
            this.player = player;
            
        }
    }
}
