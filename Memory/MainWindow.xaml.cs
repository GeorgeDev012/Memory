﻿using System;
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
        public static SecondWindow _secondWindow;
        public MainWindow()
        {
            InitializeComponent();
            _mainWindow = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!(nameTextBox.Text == String.Empty))
            {
                _secondWindow = new SecondWindow();
                _secondWindow.Show();
                this.Close();
            }
        }
    }
}
