using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Players;

namespace TicTacToe.Controls
{
    public struct ButtonInfo
    {
        public PlayerToken? Value { get; set; }
        public int Row { get; private set; }
        public int Column { get; private set; }

        public ButtonInfo(int r, int c) : this()
        {
            Row = r;
            Column = c;
        }
    }
}
