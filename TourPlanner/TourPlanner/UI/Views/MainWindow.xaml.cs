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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tour_planner.UI.ViewModels;
using Tour_planner.UI.Views;

namespace Tour_planner.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MainWindowViewModel viewModel = new MainWindowViewModel();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }


      


        private void OpenChangeWindow(object sender, RoutedEventArgs e)
        {
            ChangeTour changeTour = new ChangeTour();
            ChangeTourViewModel changeTourViewModel = new ChangeTourViewModel(viewModel.TourModel, ref viewModel);
            changeTour.DataContext = changeTourViewModel;

            changeTour.Owner = this;
            changeTour.Show();
           

        }

        private void OpenTourCreate(object sender, RoutedEventArgs e)
        {
            CreateTour createTour = new CreateTour();
            CreateTourViewModel createTourViewModel = new CreateTourViewModel(ref viewModel);
            createTour.DataContext = createTourViewModel;

            createTour.Owner = this;
            createTour.Show();
            

        }

        private void OpenTourDelete(object sender, RoutedEventArgs e)
        {
            DeleteTour deleteTour = new DeleteTour();
            deleteTour.DataContext = this.DataContext;

            deleteTour.Owner = this;
            deleteTour.Show();
        }

        private void OpenTourLog(object sender, RoutedEventArgs e)
        {
            CreateTourLog createLog = new CreateTourLog();
            CreateTourLogViewModel createTourLogViewModel = new CreateTourLogViewModel(viewModel.TourModel.Id);
            createLog.DataContext = createTourLogViewModel;

            createLog.Owner = this;
            createLog.Show();
        }
        private void OpenTourLogDelete(object sender, RoutedEventArgs e)
        {
            DeleteTourLog deleteTourLog = new DeleteTourLog();
            deleteTourLog.DataContext = this.DataContext;
            deleteTourLog.Owner = this;
            deleteTourLog.Show();
        }
    }
}
