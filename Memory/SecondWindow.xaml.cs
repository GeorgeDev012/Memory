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
    /// Interaction logic for SecondWinow.xaml
    /// </summary>
    public partial class SecondWindow : Window
    {
        public SecondWindow()
        {
            InitializeComponent();
            populateGridWithImages();
        }

        private void populateGridWithImages()
        {
            Image image = new Image();
            var uri = new Uri(@"C:\Users\George\source\repos\Memory\Memory\duck.jpg");
            ImageSource source = new BitmapImage(uri);
            image.Source = source;
            grid1.RowDefinitions.Add(new RowDefinition());
            grid1.Children.Add(image);
        }
    }
}
