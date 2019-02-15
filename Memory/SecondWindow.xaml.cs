using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        string[] ducks =
        {
            @"C:\Users\George\source\repos\Memory\Memory\Resources\duck1.jpg",
            @"C:\Users\George\source\repos\Memory\Memory\Resources\duck1.jpg",
            @"C:\Users\George\source\repos\Memory\Memory\Resources\duck2.jpg",
            @"C:\Users\George\source\repos\Memory\Memory\Resources\duck2.jpg",
            @"C:\Users\George\source\repos\Memory\Memory\Resources\duck3.jpg",
            @"C:\Users\George\source\repos\Memory\Memory\Resources\duck3.jpg",
            @"C:\Users\George\source\repos\Memory\Memory\Resources\duck4.jpg",
            @"C:\Users\George\source\repos\Memory\Memory\Resources\duck4.jpg",
            @"C:\Users\George\source\repos\Memory\Memory\Resources\duck5.jpg",
            @"C:\Users\George\source\repos\Memory\Memory\Resources\duck5.jpg",
            @"C:\Users\George\source\repos\Memory\Memory\Resources\duck6.jpg",
            @"C:\Users\George\source\repos\Memory\Memory\Resources\duck6.jpg",
            @"C:\Users\George\source\repos\Memory\Memory\Resources\duck7.jpg",
            @"C:\Users\George\source\repos\Memory\Memory\Resources\duck7.jpg",
            @"C:\Users\George\source\repos\Memory\Memory\Resources\duck8.jpg",
            @"C:\Users\George\source\repos\Memory\Memory\Resources\duck8.jpg"
        };
        string[] randomDucks = new string[16];
        bool[] wasDuck = new bool[16];

        public SecondWindow()
        {
            InitializeComponent();
            GenerateDuckArray();
        }

        

        private MouseButtonEventHandler DoJob(ref Image image)
        {
            image.Source = new BitmapImage(new Uri(@"C:\Users\George\source\repos\Memory\Memory\Resources\duck1.jpg"));
            return null;
        }

        private void Grid1_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
                var name = ((Image)sender).Name;
                var stringNumber = Regex.Match(name, @"\d+").Value;
                var number = Int32.Parse(stringNumber);

                ((Image)sender).Source = new BitmapImage(
                                        new Uri(randomDucks[number - 1]));
            
            
        }

        private int GetNotExistingDuck()
        {
            var random = new Random();
            int value, i = 0;
            do
            {
                value = random.Next(16);
                if (!wasDuck[value])
                {
                    wasDuck[value] = true;
                    i++;
                }
                
            } while ((!wasDuck[value] || i < 1));

            return value;
        }

        private void GenerateDuckArray()
        {
            if (wasDuck.Any(w => !w))
            {
                for (int i = 0; i < randomDucks.Length; i++)
                {
                    var value = GetNotExistingDuck();
                    randomDucks[i] = ducks[value];
                }
            }
        }
    }
}
