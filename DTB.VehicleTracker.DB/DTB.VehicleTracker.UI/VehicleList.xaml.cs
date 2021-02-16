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

        }

        private void BtnModels_Click(object sender, RoutedEventArgs e)
        {
            new MaintainAttributes(ScreenMode.Make).ShowDialog();
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnNewVehicle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEditVehicle_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
