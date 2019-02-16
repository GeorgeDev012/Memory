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

namespace Memory
{
    /// <summary>
    /// Interaction logic for PlayersScoreWindow.xaml
    /// </summary>
    public partial class PlayersScoreWindow : Window
    {
        public PlayersScoreWindow()
        {
            InitializeComponent();
            SetTextBlocks();
        }

        private void SetTextBlocks()
        {
            nameTextBlock.Text = MainWindow._mainWindow.textBox.Text;
            timeTextBlock.Text = MainWindow._win2.clockTextBlock.Text;
            clicksTextBlock.Text = MainWindow._win2.numberOfClicksTextBlock.Text;
        }
    }
}
