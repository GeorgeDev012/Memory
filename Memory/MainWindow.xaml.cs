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

namespace Memory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow _mainWindow;
        public static SecondWindow _win2;
        public MainWindow()
        {
            InitializeComponent();
            _mainWindow = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!(textBox.Text == String.Empty))
            {
                _win2 = new SecondWindow();
                _win2.Show();
                this.Close();
            }
        }
    }
}
