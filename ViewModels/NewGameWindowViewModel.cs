using Innouvous.Utils;
using Innouvous.Utils.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TicTacToe.Players;

namespace TicTacToe.ViewModels
{
    class NewGameWindowViewModel : ViewModel
    {
        public class PlayerMaker
        {
            private Func<PlayerToken, Player> creator;
            public string Name { get; private set; }

            public PlayerMaker(string name, Func<PlayerToken, Player> onCreate)
            {
                Name = name;
                this.creator = onCreate;
            }

            public Player CreatePlayer(PlayerToken token)
            {
                return creator.Invoke(token);
            }
        }

        private static List<PlayerMaker> playerTypes = new List<PlayerMaker>()
        {
            new PlayerMaker("Human", (token) => new HumanPlayer(token)),
            new PlayerMaker("Simple AI", (token) => new SimpleAI(token)),
        };

        private readonly NewGameWindow window;

        public NewGameWindowViewModel(NewGameWindow window)
        {
            this.window = window;
        }

        public List<PlayerMaker> Players
        {
            get { return playerTypes; }
        }

        public PlayerMaker Player1 { get; set; }
        public PlayerMaker Player2 { get; set; }

        public ICommand StartCommand
        {
            get
            {
                return new CommandHelper(CreateGame);
            }
        }

        private void CreateGame()
        {
            try
            {
                if (Player1 == null || Player2 == null)
                    throw new Exception("Both players must be selected.");

                Player p1 = Player1.CreatePlayer(PlayerToken.O);
                Player p2 = Player2.CreatePlayer(PlayerToken.X);

                MainWindow gameWindow = new MainWindow(p1, p2);
                gameWindow.Show();
                
                window.Close();
            }
            catch (Exception e)
            {
                MessageBoxFactory.ShowError(e);
            }
        }

    }
}
