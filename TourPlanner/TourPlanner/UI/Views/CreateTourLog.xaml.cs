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
using System.Windows.Shapes;

namespace Tour_planner.UI.Views
{
    /// <summary>
    /// Interaktionslogik für CreateTourLog.xaml
    /// </summary>
    public partial class CreateTourLog : Window
    {
        public CreateTourLog()
        {
            InitializeComponent();
        }
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
