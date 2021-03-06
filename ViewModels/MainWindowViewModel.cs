﻿using Innouvous.Utils.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;
using TicTacToe.Controls;
using TicTacToe.Players;

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

                if (state.TurnPlayer is AIPlayer)
                {
                    MakeCallAI((AIPlayer)state.TurnPlayer);
                }
            }
        }

        private void MakeCallAI(AIPlayer ai)
        {
            WaitingAI = true;

            Thread th = new Thread(() =>
            {
                var b = game.GetBoard();
                var move = ai.GetMove(b);
            
                Thread.Sleep(1000);

                App.Current.Dispatcher.Invoke(() =>
                {
                    buttonGrid[move.Row, move.Column].Click();
                    WaitingAI = false;
                });
            });

            th.Start();
    
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

        private bool waitingAI = false;

        public bool UIEnabled
        {
            get { return !waitingAI; }
        }

        public bool WaitingAI { 
            get { return waitingAI; }
            set
            {
                waitingAI = value;
                RaisePropertyChanged("WaitingAI");
                RaisePropertyChanged("UIEnabled");
            }
        }
    }
}
