﻿using System;
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
using System.Windows.Shapes;
using TicTacToe.ViewModels;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for NewGameWindow.xaml
    /// </summary>
    public partial class NewGameWindow : Window
    {
        private readonly NewGameWindowViewModel vm;

        public NewGameWindow()
        {
            InitializeComponent();

            vm = new NewGameWindowViewModel(this);
            DataContext = vm;
        }
    }
}
