using System;
using System.Collections.Generic;
//using System.Linq;
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
        string CardSource { get; set; } = @"C:\Users\George\source\repos\Memory\Memory\Resources\card.jpg";

        public SecondWindow()
        {
            InitializeComponent();
            populateGridWithImages();
        }

        private void populateGridWithImages()
        {

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    SetImage(i, j);
                }
            }
        }

        private void SetImage(int column, int row)
        {
            var image = new Image();
            image.Width = grid1.Width / 4;
            image.Height = grid1.Height / 4;
            image.Source = new BitmapImage(new Uri(CardSource));
            image.SetValue(Grid.RowProperty, row);
            image.SetValue(Grid.ColumnProperty, column);
            //image.MouseUp += DoJob(ref image);
            grid1.Children.Add(image);
            
        }

        private MouseButtonEventHandler DoJob(ref Image image)
        {
            image.Source = new BitmapImage(new Uri(@"C:\Users\George\source\repos\Memory\Memory\Resources\duck1.jpg"));
            return null;
        }

        private void Grid1_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }
    }
}
