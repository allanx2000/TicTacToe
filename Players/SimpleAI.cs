using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Players
{
    public class SimpleAI : AIPlayer
    {
        private Random rand = new Random(DateTime.Now.Millisecond);

        private HashSet<int> filled = new HashSet<int>();

        public SimpleAI(PlayerToken token)
            : base(token)
        {
        }

        public override Board.Location GetMove(Board board)
        {
            while (true)
            {
                int i = rand.Next(0, 9);
                if (filled.Contains(i))
                    continue;

                var location = ToLocation(i);
                bool empty = board.IsEmpty(location);

                filled.Add(i);
                
                if (!empty)
                {
                    if (filled.Count == 9)
                        throw new Exception("Board is full");
                }
                else 
                    return location;
            }
        }

        private Board.Location ToLocation(int i)
        {
            int c = i % 3;
            int r = (i - c) /3;

            return new Board.Location(r, c);
        }
    }
}
