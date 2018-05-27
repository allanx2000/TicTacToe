using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TicTacToe.Players;
using TicTacToe.ViewModels;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly MainWindowViewModel vm;
        
        public MainWindow(Player p1, Player p2)
        {
            InitializeComponent();

            vm = new MainWindowViewModel(this, p1, p2);
            DataContext = vm;

            vm.NewGame();
            
        }
        
        private void TestAI()
        {
            AIPlayer p1 = new SimpleAI(PlayerToken.O);
            AIPlayer p2 = new SimpleAI(PlayerToken.X);

            Game game = new Game(p1, p2);

            //Should move player management out of Game? Then Game is not needed?

            var first = p1.GetMove(game.GetBoard());

            game.MakeMove(first);
        }

        private void TestPlayer()
        {
            Player p1 = new HumanPlayer(PlayerToken.O);
            Player p2 = new HumanPlayer(PlayerToken.X);

            Game game = new Game(p1, p2);
            
            game.NewGame();
            
            game.MakeMove(new Board.Location(0,0));
            game.MakeMove(new Board.Location(1,0));

            game.MakeMove(new Board.Location(0,1));
            game.MakeMove(new Board.Location(1,1));

            game.MakeMove(new Board.Location(0,2));
            
            //Game Over
            game.MakeMove(new Board.Location(1, 2));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TestAI();
        }
    }
}
