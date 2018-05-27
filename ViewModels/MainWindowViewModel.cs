using Innouvous.Utils.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using TicTacToe.Controls;

namespace TicTacToe.ViewModels
{
    class MainWindowViewModel : Innouvous.Utils.MVVM.ViewModel
    {
        private readonly MainWindow mainWindow;
        private readonly Grid grid;

        private readonly Players.Player p1;
        private readonly Players.Player p2;
        
        private Game game;
        private GridButton[,] buttonGrid;

        public MainWindowViewModel(MainWindow mainWindow, Players.Player p1, Players.Player p2)
        {
            this.mainWindow = mainWindow;
            this.grid = mainWindow.GameGrid;
            this.p1 = p1;
            this.p2 = p2;
        }

        public ICommand NewGameCommand
        {
            get { return new CommandHelper(NewGame); }
        }

        public void NewGame()
        {
            p1.Reset();
            p2.Reset();

            grid.ColumnDefinitions.Clear();
            grid.Children.Clear();
            grid.RowDefinitions.Clear();

            //Simple Game
            this.game = new Game(p1,p2);
            this.buttonGrid = new GridButton[3, 3];

            //Create rows, columns
            for (int i = 0; i < 3; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < 3; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }

            //Create buttons
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    var info = new ButtonInfo(r,c);

                    GridButton button = new GridButton();
                    button.SetValue(Grid.RowProperty, r);
                    button.SetValue(Grid.ColumnProperty, c);
                    button.InitializeButton(info);
                    grid.Children.Add(button);

                    buttonGrid[r, c] = button;
                }
            }

            //Set Handlers
            GridButton.SetHandlers(
                OnClick,
                () => game.CurrentPlayerToken);

            UpdateGameState(game.GetState());
        }

        private bool OnClick(int r, int c, Players.PlayerToken token)
        {
            if (!InGame)
                return false;

            GameState state = game.MakeMove(new Board.Location(r, c));
            UpdateGameState(state);

            return true;
        }

        /*
        private GridButton GetButton(int r, int c)
        {
            if (r < 0 || r >= board.Height)
                return null;
            else if (c < 0 || c >= board.Width)
                return null;
            else
                return buttonGrid[r, c];
        }

        public int CellsLeft { get; private set; }

        private int minesLeft;
        */
        
        private bool inGame;
        private string statusText;
        
        private void UpdateGameState(GameState state)
        {
            InGame = !state.Finished;

            if (state.Finished) //Finished
            {
                if (state.Winner == null)
                {
                    StatusText = "It's a Draw";
                }
                else
                {
                    StatusText = state.Winner.Value + " won!";
                }
            }
            else
            {
                StatusText = "It's now " + state.TurnPlayer.Token + "'s turn.";
            }
        }

        public bool InGame { 
            get { return inGame; }
            set
            {
                inGame = value;
                RaisePropertyChanged("InGame");
            }
        }

        public string StatusText {
            get { return statusText; }
            set
            {
                statusText = value;
                RaisePropertyChanged("StatusText");
            }
        }
    }
}
