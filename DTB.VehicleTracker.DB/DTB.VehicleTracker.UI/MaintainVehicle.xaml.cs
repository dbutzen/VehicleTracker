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
using System.Windows.Shapes;

namespace DTB.VehicleTracker.UI
{
    /// <summary>
    /// Interaction logic for MaintainVehicle.xaml
    /// </summary>
    public partial class MaintainVehicle : Window
    {
        ucMaintainVehicle[] attributes = new ucMaintainVehicle[4];
        Vehicle vehicle;


        // New Vehicle
        public MaintainVehicle()
        {
            InitializeComponent();
            vehicle = new Vehicle();
            this.Title = "New Vehicle";
            DrawScreen();
        }

        // Edit Vehicle
        public MaintainVehicle(Vehicle vehicle)
        {
            InitializeComponent();
            this.vehicle = vehicle;
            this.Title = "Edit Vehicle";
            DrawScreen();
            txtVIN.Text = vehicle.VIN;
        }

        private void DrawScreen()
        {
            ucMaintainVehicle ucColors = new ucMaintainVehicle(ControlMode.Color, vehicle.ColorId);
            ucMaintainVehicle ucMakes = new ucMaintainVehicle(ControlMode.Make, vehicle.MakeId);
            ucMaintainVehicle ucModels = new ucMaintainVehicle(ControlMode.Model, vehicle.ModelId);
            ucMaintainVehicle ucYears = new ucMaintainVehicle(ControlMode.Year, vehicle.Id);

            ucColors.Margin = new Thickness(40, 25, 0, 0);
            ucMakes.Margin = new Thickness(40, 60, 0, 0);
            ucModels.Margin = new Thickness(40, 95, 0, 0);
            ucYears.Margin = new Thickness(40, 130, 0, 0);

            grdVehicle.Children.Add(ucColors);
            grdVehicle.Children.Add(ucMakes);
            grdVehicle.Children.Add(ucModels);
            grdVehicle.Children.Add(ucYears);

            attributes[0] = ucColors;
            attributes[1] = ucMakes;
            attributes[2] = ucModels;
            attributes[3] = ucYears;

            lblVIN.Margin = new Thickness(40, 170, 0, 0);
            txtVIN.Margin = new Thickness(85, 170, 0, 0);
            btnInsert.Margin = new Thickness(36, 200, 0, 0);
            btnUpdate.Margin = new Thickness(116, 200, 0, 0);
            btnDelete.Margin = new Thickness(196, 200, 0, 0);

        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
