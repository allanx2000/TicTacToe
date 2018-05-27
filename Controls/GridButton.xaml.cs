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

namespace TicTacToe.Controls
{
    /// <summary>
    /// Interaction logic for GridButton.xaml
    /// </summary>
    public partial class GridButton : UserControl
    {
        private enum ButtonStates
        {
            Empty,
            X,
            O
        }

        private static SolidColorBrush GRAY = new SolidColorBrush(Colors.Gray);
        private static SolidColorBrush BLUE = new SolidColorBrush(Colors.Blue);
        private static SolidColorBrush RED = new SolidColorBrush(Colors.Red);
        
        private ButtonInfo info;

        public void InitializeButton(ButtonInfo info)
        {
            this.info = info;
        }

        #region Game Actions Handlers

        private static Action<int, int, PlayerToken> onClick;
        private static Func<PlayerToken?> getCurrentToken;

        //onFlag: r,c, isFlagged
        public static void SetHandlers(Action<int, int, PlayerToken> onClick, Func<PlayerToken?> getCurrentToken)
        {
            GridButton.onClick = onClick;
            GridButton.getCurrentToken = getCurrentToken;
        }

        #endregion

        #region DPs

        public static readonly DependencyProperty TextColorProperty =
        DependencyProperty.Register("TextColor", typeof(SolidColorBrush),
        typeof(GridButton), null);

        public SolidColorBrush TextColor
        {
            get
            {
                return (SolidColorBrush)GetValue(TextColorProperty);
            }
            set
            {
                SetValue(TextColorProperty, value);
            }
        }

        public static readonly DependencyProperty ButtonTextProperty =
        DependencyProperty.Register("ButtonText", typeof(string),
            typeof(GridButton), null);

        public string ButtonText
        {
            get
            {
                return (string)GetValue(ButtonTextProperty);
            }
            set
            {
                SetValue(ButtonTextProperty, value);
            }
        }


        #endregion

        public bool Clicked { get; private set; }

        public GridButton()
        {
            InitializeComponent();

            SetColor(null);
            Clicked = false;
        }

        private void SetColor(PlayerToken? token)
        {
            SolidColorBrush color;

            switch (token)
            {
                case PlayerToken.X:
                    color = RED;
                    break;
                case PlayerToken.O:
                    color = BLUE;
                    break;
                default:
                    color = GRAY;
                    break;
            }

            SetValue(ButtonTextProperty, ToString(token));
            SetValue(TextColorProperty, color);
        }

        private string ToString(PlayerToken? token)
        {
            return token == null ? null : token.ToString();
        }

        //Click
        //TODO: Shouldn't need both
        public void OnLeftClick(object sender, MouseButtonEventArgs e)
        {
            var token = getCurrentToken.Invoke();

            if (Clicked || token == null)
                e.Handled = true;
            else
                DoClick(token);
        }

        public void Click()
        {
            if (Clicked)
            {
                return;
            }

            DoClick();
        }

        private void DoClick(PlayerToken? token = null)
        {
            if (token == null)
                token = getCurrentToken.Invoke();
            else
            {
                SetColor(token);
            
                Clicked = true;

                if (onClick != null)
                    onClick.Invoke(info.Row, info.Column, token.Value);
            }
        }
    }
}
