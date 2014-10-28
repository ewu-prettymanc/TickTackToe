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
using System.Windows.Shapes;

namespace TickTackToe
{
    /// <summary>
    /// Interaction logic for UsageWindow.xaml
    /// </summary>
    public partial class UsageWindow : Window
    {
        public UsageWindow()
        {
            InitializeComponent();

            string s = "\t---Tick Tack Toe usage---";
            s += "\n\nUpon startup, select new game. Next" +
                "\nyou must flip the coin to select the " +
                "\nfirst player. Play the game clicking " +
                "\nwhere you would like to place your move." +
                "\nOnce a game is completed you have the " +
                "\noption to play again. You also have the " +
                "\noption to start the game and select the " +
                "\nboard size. Once this is chosen you must " +
                "\nenter the size of the game board. This " +
                "\nmust be between 0 and 50. Submit the input" +
                "\nand continue the game as usual";
            lblUsageInfo.Content = s;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
