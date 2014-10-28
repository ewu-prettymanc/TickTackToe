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


/* Colton Prettyman 03-07-13
 * CSCD 306 Professor Thomas Capaul HW4
 * An Implemention of the Game of Tick Tack Toe
 * 
 *      ---Extra Credit Possibilites---
 *  Upgraded AI
 *  Graphics coloring on the Form
 *  A Resizable and ReScaling GameBoard 
 *  User select gameboard dimensions
 */
namespace TickTackToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {
        private GameBoard myGame;
        private int _size;
        private Line[] VLines;
        private Line[] HLines;
        private char _player;
        private bool _gameinput;
        private int _movecount;
        private bool _aiPlayer;
        private char _aiPiece;
        private bool _firstgame;

        public MainWindow()
        {
            InitializeComponent();
            Closing += new System.ComponentModel.CancelEventHandler(MainWindow_Closing);
            _firstgame = true;
         //   UpdateMainWindow();
        }

        public void UpdateMainWindow( int size = 3)
        {
            _size = size;
            _gameinput = false;
            _player = '-';
            _movecount = 0;

            VLines = new Line[_size-1];
            HLines = new Line[_size-1];
            for( int i=0; i < VLines.Length; i++ )
            {
                VLines[i] = new Line();
                HLines[i] = new Line(); 
            }

            DrawLines();

            myGame = new GameBoard(_size, TTTCanvas);
        }

        // Erases GameBoard Grid Lines from the Canvas
        public void EraseCanvas()
        {
            if( ! _firstgame )
                 for (int i = 0; i < VLines.Length; i++)
                  {
                        TTTCanvas.Children.Remove(VLines[i]);
                        TTTCanvas.Children.Remove(HLines[i]);
                  }
        }


        // Window width -20 / _size
        // Window height -50 / _size
        // Start drawing lines from left to right top to bottom 
        // giving a padding on either side of 20/2 and (50-20)/2
        private void DrawLines(bool resize = false )
        {
            double width = TTTCanvas.Width;
           // double dx = (width - 20) / _size;
          //  double edgepad_x = 10, curx;

            double dx = width / _size;
            double edgepad_x = 0, curx;

            double height = TTTCanvas.Height;
            double dy =  height  / _size;
           // double edgepad_y = 10, cury;
            // double lowerpad = 10;
            double edgepad_y = 0, cury;
            int i;

            // Allocate the Vertical Lines
            // Create an edge padding on the top and bottom
            curx = edgepad_x;
            for (i = 0; i < VLines.Length; i++)
            {
                curx += dx;
                VLines[i].X1 = curx;
                VLines[i].Y1 = edgepad_y;
                VLines[i].X2 = curx;
                VLines[i].Y2 = height - edgepad_y;
                VLines[i].StrokeThickness = 5;
                if (!resize)
                {
                    TTTCanvas.Children.Add(VLines[i]);
                    VLines[i].Stroke = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF1D0000"));
                }

            }

            // Allocate the Horizontal Lines
            // Create an edge padding on the left and right
            cury = edgepad_y;
            for (i = 0; i < HLines.Length; i++)
            {
                cury += dy;
                HLines[i].X1 = edgepad_x;
                HLines[i].Y1 = cury;
                HLines[i].X2 = width - edgepad_x;
                HLines[i].Y2 = cury;
                HLines[i].StrokeThickness = 5;
                
                if( ! resize  )
                  TTTCanvas.Children.Add(HLines[i]);
                  HLines[i].Stroke = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF1D0000")); //System.Windows.Media.Brushes.Black;

            }
        }


        private void TTTWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_gameinput)
            {
                Point p = e.GetPosition(this.TTTCanvas);
                string result = "";

                result = this.myGame.PlacePiece(_player, p.X, p.Y);

                if (result == "notplaced")
                {
                    lblGameStatus.Content = "Invalid Move";
                    return;
                }

                if (result == "win")
                {
                    WinHappened(true);
                    return;
                }

                // MoveCount variable in case of Cats Game
                _movecount++;
                if (_movecount == _size * _size)
                {
                    WinHappened(false);
                    return;
                }

                ChangePlayer();

                if (_aiPlayer)
                {
                    System.Threading.Thread.Sleep(0);
                    MakeAIMove();
                }
            }
        }

        private void ChangePlayer()
        {
            if (_player == 'X')
            {
                _player = 'O';
                UpdatePlayerLabel();
            }
            else
            {
                _player = 'X';
                UpdatePlayerLabel();
            }  
        }

        private void MakeAIMove()
        {
           bool win = myGame.MakeAIMove(_aiPiece);
           _movecount++;

           if (win)
           {
               WinHappened(true);
               return;
           }

            if (_movecount == _size * _size)
            {
                WinHappened(false);
                return;
            }

            ChangePlayer();
        }

        private void WinHappened(bool win )
        {
            _gameinput = false ;
            btnPlayAgain.IsEnabled = true;
            btnPlayAgain.Focus();

            if (win)
            {
                if (_aiPlayer && _player == _aiPiece )
                    lblGameStatus.Content = "Computer Player " + _player.ToString() + " Won!!";
                else
                    lblGameStatus.Content = "Player " + _player.ToString() + " Won!!";
            }
            else
                lblGameStatus.Content = "Cats Game!!";
            _player = '-';
        }



        #region Event Handler Definations
        private void btnPlayAgain_Click(object sender, RoutedEventArgs e)
        {
            this.Reset();
            btnPlayAgain.IsEnabled = false;
        }

 

        private void btnCoinToss_Click(object sender, RoutedEventArgs e)
        {
            Random coin = new Random();
            int face = coin.Next() % 2 + 1;

            _gameinput = true;
            if (face == 1)
            {
                _player = 'O';
                UpdatePlayerLabel();
            }
            else
            {
                _player = 'X';
                UpdatePlayerLabel();
            }

            btnCoinToss.IsEnabled = false;
            NewGameMenuItem.IsEnabled = true;
            NewGameWithSizeMenuItem.IsEnabled = true;

            if (_aiPlayer)
            {
                face = coin.Next() % 2 + 1;
                if (face == 1)
                    _aiPiece = 'X';
                else
                    _aiPiece = 'O';

                if (_player == _aiPiece)
                {
                    System.Threading.Thread.Sleep(0);
                    MakeAIMove();
                }
            }
        }


        private void UpdatePlayerLabel()
        {
            lblGameStatus.Content = "Player " + _player + "'s Turn";
        }
        private void CloseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            TTTAboutBox about = new TTTAboutBox();
            about.ShowDialog();
        }
        #endregion

        private void UsageMenuItem_Click(object sender, RoutedEventArgs e)
        {
            UsageWindow u = new UsageWindow();
            u.ShowDialog();
        }

        //Provid a closing dialog box...
        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs  e)
        {
            MessageBoxResult result = MessageBox.Show("Exit Tick Tack Toe?", 
                    "Tick Tack Toe", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                e.Cancel = true ;
        }

        private void NewGameMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _aiPlayer = false;
            this.EraseCanvas();
            this.Reset();

            MessageBoxResult result = MessageBox.Show("Play Against the Computer?",
                "Tick Tack Toe", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
                _aiPlayer = true;

            btnPlayAgain.IsEnabled = false;
            NewGameMenuItem.IsEnabled = false;
            NewGameWithSizeMenuItem.IsEnabled = false;
            this.UpdateMainWindow();
            _firstgame = false;
        }

        private void NewGameWithSizeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _aiPlayer = false;
            if( ! _firstgame )
               myGame.Reset();
            this.EraseCanvas();
            lblGameStatus.Content = "Enter Game Board Dimensions";

            MessageBoxResult result = MessageBox.Show("Play Against the Computer?",
                "Tick Tack Toe", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
                _aiPlayer = true;

            btnPlayAgain.IsEnabled = false;
            NewGameWithSizeMenuItem.IsEnabled = false;
            NewGameMenuItem.IsEnabled = false;

            cmbxBoardSize.Visibility = Visibility.Visible;
            btnSubmit.Visibility = Visibility.Visible;
            btnSubmit.IsEnabled = true;
            cmbxBoardSize.IsEnabled = true;
            cmbxBoardSize.Focus();
            _gameinput = false;
        }

        private void Reset()
        {
            if( ! _firstgame )
                  myGame.Reset();

            btnCoinToss.IsEnabled = true;
            btnCoinToss.Focus();
            lblGameStatus.Content = "Toss Coin for First Play";
            _gameinput = false;
            _movecount = 0;
            _player = '-';
            _aiPiece = '-';
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
                string input;
                input = cmbxBoardSize.Text;

                // Get an validate input from the user
                if (int.TryParse(input, out _size))
                {
                    _size = int.Parse(input);
                    if (_size > 0 && _size <= 50)
                    {
                        cmbxBoardSize.Visibility = Visibility.Hidden;
                        btnSubmit.Visibility = Visibility.Hidden;
                        btnSubmit.IsEnabled = false;
                        cmbxBoardSize.IsEnabled = false;
                        cmbxBoardSize.Focus();

                        btnCoinToss.IsEnabled = true;
                        btnCoinToss.Focus();

                        this.UpdateMainWindow(_size);

                        btnCoinToss.IsEnabled = true;
                        btnCoinToss.Focus();
                        lblGameStatus.Content = "Toss Coin for First Play";
                        _movecount = 0;
                        _gameinput = false;
                        _firstgame = false;
                        return;
                    }
                }

                //They've Entered bad input
                lblGameStatus.Content = "Invalid Dimension";
                cmbxBoardSize.Focus();
        }

        private void TTTWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TTTCanvas.Width = e.NewSize.Width-120;
            TTTCanvas.Height = e.NewSize.Height-150;
            // Just update the canvas size and return
            if (_firstgame)
                return;
            // Updates the Lines that are drawn
            this.DrawLines( true );
            this.myGame.ScaleToSize();
        }
    }
}
