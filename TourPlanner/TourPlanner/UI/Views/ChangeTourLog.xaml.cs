﻿using System;
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

namespace TourPlanner.UI.Views
{
    /// <summary>
    /// Interaktionslogik für ChangeTourLog.xaml
    /// </summary>
    public partial class ChangeTourLog : Window
    {
        public ChangeTourLog()
        {
            InitializeComponent();
        }
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
