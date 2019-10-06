using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for HighscoreWindow.xaml
    /// </summary>
    public partial class HighscoreWindow : Window
    {
        internal static HighscoreWindow _highscoreWindow;

        public HighscoreWindow()
        {
            InitializeComponent();
            HighscoreDatagrid.DataContext = SecondWindow._Highscores;
        }

        private void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._mainWindow = new MainWindow();
            this.Close();
            MainWindow._mainWindow.Show();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        static internal void SaveHighscores()
        {
            string serializationFile = "highscores.bin";

            //serialize
            try
            {
                Stream stream = File.Open(serializationFile, FileMode.Create);
            
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bformatter.Serialize(stream, SecondWindow._Highscores);

            }
            catch (UnauthorizedAccessException)
            {
                
            }
        }

        static internal ObservableCollection<Player> GetHighscores()
        {
            string serializationFile = "highscores.bin";

            //serialize
            try
            {
                Stream stream = File.Open(serializationFile, FileMode.Open);
                //stream.Seek(0, SeekOrigin.Begin);
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                ObservableCollection<Player> highscoreList = (ObservableCollection<Player>)bformatter.Deserialize(stream);

                stream.Close();
                return highscoreList;
            }
            catch (FileNotFoundException)
            {
                return new ObservableCollection<Player>();
            }
            catch (UnauthorizedAccessException)
            {
                return new ObservableCollection<Player>();
            }
        }
    }
}
