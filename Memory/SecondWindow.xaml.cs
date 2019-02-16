using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
        string CardSource { get; } = @"C:\Users\George\source\repos\Memory\Memory\Resources\card.jpg";

        readonly string[] ducks =
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
        bool[] correctlyReversedDuck = new bool[16];
        int numberOfImages = 0;
        private Image lastImage;

        public SecondWindow()
        {
            InitializeComponent();
            GenerateRandomDuckArray();
        }



        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            var number = getNumberFromName((Image)sender);
            if (!correctlyReversedDuck[number - 1])
            {
                ((Image)sender).Source = new BitmapImage(
                                            new Uri(randomDucks[number - 1]));

                numberOfImages++;
                if (numberOfImages == 1) lastImage = (Image)sender;
                if (numberOfImages == 2)
                {
                    numberOfImages = 0;
                    var comparison = AreImagesTheSame((Image)sender, lastImage);
                    if (comparison)
                    {

                    }
                    else
                    {
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Input, new ThreadStart(() =>
                        {
                            Thread.Sleep(800);
                            ChangeIncorrectImages(sender);
                        }
                        ));
                    }
                }
            }


        }

        private void ChangeIncorrectImages(object sender)
        {
            ImageSource source = new BitmapImage(new Uri(CardSource));
            ((Image)sender).Source = source;
            lastImage.Source = source;
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

        private void GenerateRandomDuckArray()
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

        private bool AreImagesTheSame(Image image1, Image image2)
        {
            var number = getNumberFromName(image1);
            var number2 = getNumberFromName(image2);

            if (randomDucks[number - 1].Equals(randomDucks[number2 - 1]))
            {
                correctlyReversedDuck[number - 1] = true;
                correctlyReversedDuck[number2 - 1] = true;
                return true;
            }
            else return false;
        }

        private int getNumberFromName(Image image)
        {
            var name = image.Name;
            var stringNumber = Regex.Match(name, @"\d+").Value;
            return Int32.Parse(stringNumber);
        }
       
    }
}
