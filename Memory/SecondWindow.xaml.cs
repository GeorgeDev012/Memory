using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace Memory
{
    /// <summary>
    /// Interaction logic for SecondWinow.xaml
    /// </summary>
    public partial class SecondWindow : Window
    {
        string CardSource { get; } = @"C:\Users\George\source\repos\Memory\Memory\Resources\card.jpg";

        readonly string[] _ducks =
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
        string[] _randomDucks = new string[16];
        bool[] _wasDuck = new bool[16];
        bool[] _correctlyReversedDuck = new bool[16];
        int _numberOfReversedImages = 0;
        private Image _previousImage;
        private int _clicks = 0;
        DispatcherTimer _timer = new DispatcherTimer();
        Stopwatch _stopwatch = new Stopwatch();
        string _currentTime = string.Empty;

        public SecondWindow()
        {
            InitializeComponent();
            GenerateRandomDuckArray();
            _timer.Tick += new EventHandler(timerTick);
            _stopwatch.Start();
            _timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {
            if(_stopwatch.IsRunning)
            {
                TimeSpan timeSpan = _stopwatch.Elapsed;
                _currentTime = string.Format("{0:00}:{1:00}:{2:00}", timeSpan.Minutes, 
                                                            timeSpan.Seconds, timeSpan.Milliseconds / 10);
                clockTextBlock.Text = _currentTime;

            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            var number = getNumberFromName((Image)sender);
            if (!_correctlyReversedDuck[number - 1] && _numberOfReversedImages == 0 || ((Image)sender).Name != _previousImage.Name)
            {
                ((Image)sender).Source = new BitmapImage(
                                            new Uri(_randomDucks[number - 1]));
                _clicks++;
                _numberOfReversedImages++;
                if (_numberOfReversedImages == 1) _previousImage = (Image)sender;
                else if (_numberOfReversedImages == 2)
                {
                    _numberOfReversedImages = 0;
                    var comparison = AreImagesTheSame((Image)sender, _previousImage);
                    if (!comparison)
                    {
                        Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
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
            _previousImage.Source = source;
        }

        private int GetNotExistingDuck()
        {
            var random = new Random();
            int value, i = 0;
            do
            {
                value = random.Next(16);
                if (!_wasDuck[value])
                {
                    _wasDuck[value] = true;
                    i++;
                }
                
            } while ((!_wasDuck[value] || i < 1));

            return value;
        }

        private void GenerateRandomDuckArray()
        {
            if (_wasDuck.Any(w => !w))
            {
                for (int i = 0; i < _randomDucks.Length; i++)
                {
                    var value = GetNotExistingDuck();
                    _randomDucks[i] = _ducks[value];
                }
            }
        }

        private bool AreImagesTheSame(Image image1, Image image2)
        {
            var number = getNumberFromName(image1);
            var number2 = getNumberFromName(image2);

            if (_randomDucks[number - 1].Equals(_randomDucks[number2 - 1]))
            {
                _correctlyReversedDuck[number - 1] = true;
                _correctlyReversedDuck[number2 - 1] = true;
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
