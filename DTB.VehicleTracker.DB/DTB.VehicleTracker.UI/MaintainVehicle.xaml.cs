using DTB.VehicleTracker.BL;
using DTB.VehicleTracker.BL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        bool newVehicle;


        // New Vehicle
        public MaintainVehicle(Vehicle vehicle)
        {
            InitializeComponent();
            this.vehicle = vehicle;
            if (Guid.Empty == vehicle.Id)
            {
                this.Title = "New Vehicle";
                newVehicle = true;
            }
            else
            {
                this.Title = "Edit Vehicle";
                newVehicle = false;
            }
            btnUpdate.IsEnabled = !newVehicle;
            btnDelete.IsEnabled = !newVehicle;
            btnInsert.IsEnabled = newVehicle;

            DrawScreen();
        }

        private void DrawScreen()
        {
            ucMaintainVehicle ucColors = new ucMaintainVehicle(ControlMode.Color, vehicle.ColorId);
            ucMaintainVehicle ucMakes = new ucMaintainVehicle(ControlMode.Make, vehicle.MakeId);
            ucMaintainVehicle ucModels = new ucMaintainVehicle(ControlMode.Model, vehicle.ModelId);
            ucMaintainVehicle ucYears = new ucMaintainVehicle(ControlMode.Year, vehicle.Id);
            txtVIN.Text = vehicle.VIN;

            ucColors.imgDelete.MouseLeftButtonUp += ImgDelete_MouseLeftButtonUp;
            ucMakes.imgDelete.MouseLeftButtonUp += ImgDelete_MouseLeftButtonUp;
            ucModels.imgDelete.MouseLeftButtonUp += ImgDelete_MouseLeftButtonUp;
            ucYears.imgDelete.MouseLeftButtonUp += ImgDelete_MouseLeftButtonUp;

            //ucColors.cboAttribute.SelectionChanged += CboAttribute_SelectionChanged;
            //ucMakes.cboAttribute.SelectionChanged += CboAttribute_SelectionChanged;
            //ucModels.cboAttribute.SelectionChanged += CboAttribute_SelectionChanged;


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

        private void CboAttribute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = ((ComboBox)sender);
            ControlMode controlMode = (ControlMode)comboBox.Tag;

            switch (controlMode)
            {
                case ControlMode.Color:
                    vehicle.ColorName = comboBox.Text;
                    Debug.Print("Changing to " + comboBox.Text);
                    break;
                case ControlMode.Make:
                    vehicle.MakeName = comboBox.Text;
                    break;
                case ControlMode.Model:
                    vehicle.ModelName = comboBox.Text;
                    break;
            }
        }

        private void ImgDelete_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Title = "Picked the Image" + DateTime.Now;
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;

            vehicle.ColorId = attributes[(int)ControlMode.Color].AttributeId;
            index = attributes[(int)ControlMode.Color].cboAttribute.SelectedIndex;
            vehicle.ColorName = attributes[(int)ControlMode.Color].Colors[index].Description;

            vehicle.MakeId = attributes[(int)ControlMode.Make].AttributeId;
            index = attributes[(int)ControlMode.Make].cboAttribute.SelectedIndex;
            vehicle.MakeName = attributes[(int)ControlMode.Make].Makes[index].Description;

            vehicle.ModelId = attributes[(int)ControlMode.Model].AttributeId;
            index = attributes[(int)ControlMode.Model].cboAttribute.SelectedIndex;
            vehicle.ModelName = attributes[(int)ControlMode.Model].Models[index].Description;

            vehicle.Year = Convert.ToInt32(attributes[(int)ControlMode.Year].AttributeText);
            vehicle.VIN = txtVIN.Text;

            int results = 0;

            Task.Run(async () =>
            {
                results = await VehicleManager.Insert(vehicle);
                this.Title = vehicle.Id.ToString();
            });
            Debug.Print("Insert: " + results);
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;

            vehicle.ColorId = attributes[(int)ControlMode.Color].AttributeId;
            index = attributes[(int)ControlMode.Color].cboAttribute.SelectedIndex;
            vehicle.ColorName = attributes[(int)ControlMode.Color].Colors[index].Description;

            vehicle.MakeId = attributes[(int)ControlMode.Make].AttributeId;
            index = attributes[(int)ControlMode.Make].cboAttribute.SelectedIndex;
            vehicle.MakeName = attributes[(int)ControlMode.Make].Makes[index].Description;

            vehicle.ModelId = attributes[(int)ControlMode.Model].AttributeId;
            index = attributes[(int)ControlMode.Model].cboAttribute.SelectedIndex;
            vehicle.ModelName = attributes[(int)ControlMode.Model].Models[index].Description;

            vehicle.Year = Convert.ToInt32(attributes[(int)ControlMode.Year].AttributeText);
            vehicle.VIN = txtVIN.Text;

            int results = 0;

            Task.Run(async () =>
            {
                results = await VehicleManager.Update(vehicle);
            });
            Debug.Print("Update: " + results);
        }

        private void Button_Click(object sender, RoutedEventArgs e) //Delete
        {
            int results = 0;

            Task.Run(async () =>
            {
                results = await VehicleManager.Delete(vehicle.Id);
            });
            Debug.Print("Delete: " + results);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
