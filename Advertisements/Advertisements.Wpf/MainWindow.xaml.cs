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
using Advertisements.Infrastructures.Services;
using Advertisements.Infrastructures.Services.Contracts;

namespace Advertisements.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IHomeServices homeServices;
        
        public MainWindow()
        {
            InitializeComponent();

            this.homeServices = new HomeServices();
        }

        public MainWindow(IHomeServices homeServices)
        {
            this.homeServices = homeServices;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var dataContext = this.homeServices.GetAllAds().ToList();
            //this.GridAds.DataContext = dataContext;
            this.DataGrid.ItemsSource = dataContext;
        }
    }
}
