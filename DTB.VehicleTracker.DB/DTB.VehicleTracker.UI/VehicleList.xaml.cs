using DTB.VehicleTracker.BL;
using DTB.VehicleTracker.BL.Models;
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

namespace DTB.VehicleTracker.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class VehicleList : Window
    {
        List<Vehicle> vehicles;
        public VehicleList()
        {
            InitializeComponent();
        }

        private void BtnMakes_Click(object sender, RoutedEventArgs e)
        {
            // How to pass data
            // Mode - pass string "makes" --> Worst
            // Mode - enumeration (1,2) --> Best
            // Mode - int 1 or 2
            // Mode - Bool
            // Button text

            new MaintainAttributes(ScreenMode.Make).ShowDialog();
        }

        private void BtnColor_Click(object sender, RoutedEventArgs e)
        {
            new MaintainColors().ShowDialog();
        }

        private void BtnModels_Click(object sender, RoutedEventArgs e)
        {
            new MaintainAttributes(ScreenMode.Make).ShowDialog();
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            Reload();
        }

        private async void Reload()
        {
            vehicles = (List<Vehicle>)await VehicleManager.Load();
            grdVehicles.ItemsSource = null;
            grdVehicles.ItemsSource = vehicles;

            grdVehicles.Columns[0].Visibility = Visibility.Hidden;
            grdVehicles.Columns[1].Visibility = Visibility.Hidden;
            grdVehicles.Columns[2].Visibility = Visibility.Hidden;
            grdVehicles.Columns[3].Visibility = Visibility.Hidden;

            grdVehicles.Columns[6].Header = "Color";
            grdVehicles.Columns[7].Header = "Make";
            grdVehicles.Columns[8].Header = "Model";


        }

        private void BtnNewVehicle_Click(object sender, RoutedEventArgs e)
        {
            new MaintainVehicle().ShowDialog();
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEditVehicle_Click(object sender, RoutedEventArgs e)
        {
            new MaintainVehicle().ShowDialog();
        }
    }
}
