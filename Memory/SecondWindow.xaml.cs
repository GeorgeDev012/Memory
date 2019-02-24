using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        string CardSource { get; } = @".\Resources\card.jpg";

        readonly string[] _ducks =
        {
            @".\Resources\duck1.jpg",
            @".\Resources\duck1.jpg",
            @".\Resources\duck2.jpg",
            @".\Resources\duck2.jpg",
            @".\Resources\duck3.jpg",
            @".\Resources\duck3.jpg",
            @".\Resources\duck4.jpg",
            @".\Resources\duck4.jpg",
            @".\Resources\duck5.jpg",
            @".\Resources\duck5.jpg",
            @".\Resources\duck6.jpg",
            @".\Resources\duck6.jpg",
            @".\Resources\duck7.jpg",
            @".\Resources\duck7.jpg",
            @".\Resources\duck8.jpg",
            @".\Resources\duck8.jpg"
        };
        string[] _randomDucks = new string[16];
        bool[] _alreadyChosenDuck = new bool[16];
        bool[] _correctlyReversedDucks = new bool[16];
        int _numberOfReversedImages = 0;
        Image _previousImage;
        int _clicks = 0;
        DispatcherTimer _timer = new DispatcherTimer();
        Stopwatch _stopwatch = new Stopwatch();
        string _currentTime = string.Empty;
        internal static PlayersScoreWindow _PlayersScoreWindow;
        internal static ObservableCollection<Player> _Highscores = new ObservableCollection<Player>();


        public SecondWindow()
        {
            InitializeComponent();
            GenerateRandomDuckArray();
            _timer.Tick += Timer_Tick;
            _stopwatch.Start();
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(_stopwatch.IsRunning)
            {
                TimeSpan timeSpan = _stopwatch.Elapsed;
                _currentTime = string.Format("{0:00}:{1:00}:{2:00}", timeSpan.Minutes, 
                                                            timeSpan.Seconds, timeSpan.Milliseconds / 10);
                clockTextBlock.Text = _currentTime;
                numberOfClicksTextBlock.Text = _clicks.ToString();
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            var number = GetNumberFromName((Image)sender);
            if (!_correctlyReversedDucks[number - 1] && (_numberOfReversedImages == 0 || ((Image)sender).Name != _previousImage.Name))
            {
                ((Image)sender).Source = new BitmapImage(
                                            new Uri(_randomDucks[number - 1], UriKind.Relative));
                _clicks++;
                _numberOfReversedImages++;
                if (_numberOfReversedImages == 1) _previousImage = (Image)sender;
                else if (_numberOfReversedImages == 2)
                {
                    _numberOfReversedImages = 0;
                    var comparison = AreImagesTheSame((Image)sender, _previousImage);
                    if (_correctlyReversedDucks.All(b => b == true))
                    {
                        _stopwatch.Stop();
                        AddScoresToHighscoreList();
                        //SortHighscoreList();
                        _PlayersScoreWindow = new PlayersScoreWindow();
                        _PlayersScoreWindow.Show();
                        this.Close();
                    }
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

        private void AddScoresToHighscoreList()
        {
            var playerName = MainWindow._mainWindow.nameTextBox.Text;
            var numberOfClicks = Convert.ToInt32(MainWindow._secondWindow.numberOfClicksTextBlock.Text);
            var timePassed = MainWindow._secondWindow.clockTextBlock.Text;

            Player player = new Player{ Name = playerName, ClickCount = numberOfClicks, TimePassed = timePassed };
            _Highscores.Add(player);
        }

        //private void SortHighscoreList()
        //{
            
        //    _Highscores.Sort((x, y) => {
        //        int result = y.Item2.CompareTo(x.Item2);
        //        return result == 0 ? y.Item3.CompareTo(x.Item3) : result;
        //    });
        //}

        private void ChangeIncorrectImages(object sender)
        {
            ImageSource source = new BitmapImage(new Uri(CardSource, UriKind.Relative));
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
                if (!_alreadyChosenDuck[value])
                {
                    _alreadyChosenDuck[value] = true;
                    i++;
                }
                
            } while ((!_alreadyChosenDuck[value] || i < 1));

            return value;
        }

        private void GenerateRandomDuckArray()
        {
            if (_alreadyChosenDuck.Any(w => w == false))
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
            var number = GetNumberFromName(image1);
            var number2 = GetNumberFromName(image2);

            if (_randomDucks[number - 1].Equals(_randomDucks[number2 - 1]))
            {
                _correctlyReversedDucks[number - 1] = true;
                _correctlyReversedDucks[number2 - 1] = true;
                return true;
            }
            else return false;
        }

        private int GetNumberFromName(Image image)
        {
            var name = image.Name;
            var stringNumber = Regex.Match(name, @"\d+").Value;
            return Int32.Parse(stringNumber);
        }
    }
}
