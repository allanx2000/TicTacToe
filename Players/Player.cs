using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Players
{
    public abstract class Player
    {
        private PlayerToken type;

        public Player(PlayerToken type)
        {
            this.type = type;
        }

        public PlayerToken Token { get { return type; } }

        public event EventHandler<TurnNotificationArgs> OnTurn;

        public void NotifyTurn()
        {
            if (OnTurn != null)
            {
                OnTurn.Invoke(this, new TurnNotificationArgs(this));
            }
        }

        public virtual void Reset()
        {
        }
    }
}
