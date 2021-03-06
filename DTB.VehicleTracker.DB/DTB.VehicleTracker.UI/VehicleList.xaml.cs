using DTB.VehicleTracker.BL;
using DTB.VehicleTracker.BL.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        List<Vehicle> filteredVehicles;
        List<BL.Models.Color> colors;
        Vehicle vehicle;
        private readonly ILogger<VehicleList> logger;
        

        public VehicleList(ILogger<VehicleList> _logger)
        {
            logger = _logger;
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
            MaintainAttributes maintainAttributes = new MaintainAttributes(ScreenMode.Make);
            maintainAttributes.Owner = this;
            maintainAttributes.ShowDialog();
        }

        private void BtnColor_Click(object sender, RoutedEventArgs e)
        {
            MaintainColors maintainColors = new MaintainColors();
            maintainColors.Owner = this;
            maintainColors.ShowDialog();
        }

        private void BtnModels_Click(object sender, RoutedEventArgs e)
        {
            MaintainAttributes maintainAttributes = new MaintainAttributes(ScreenMode.Model);
            maintainAttributes.Owner = this;
            maintainAttributes.ShowDialog();
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            Reload();
        }

        private async void Reload()
        {
            vehicles = (List<Vehicle>)await VehicleManager.Load();

            filteredVehicles = vehicles;
            logger.LogInformation("Loaded " + vehicles.Count + " vehicles.");

            colors = (List<BL.Models.Color>)await ColorManager.Load();

            cboFilter.ItemsSource = null;
            cboFilter.ItemsSource = colors;
            cboFilter.DisplayMemberPath = "Description";
            cboFilter.SelectedValuePath = "Id";

            Rebind();


        }

        private void Rebind()
        {
            grdVehicles.ItemsSource = null;
            grdVehicles.ItemsSource = vehicles;

            

            grdVehicles.Columns[0].Visibility = Visibility.Hidden;
            grdVehicles.Columns[1].Visibility = Visibility.Hidden;
            grdVehicles.Columns[2].Visibility = Visibility.Hidden;
            grdVehicles.Columns[3].Visibility = Visibility.Hidden;

            grdVehicles.Columns[6].Header = "Color";
            grdVehicles.Columns[7].Header = "Make";
            grdVehicles.Columns[8].Header = "Model";

            Style headerStyle = new Style();
            DataGridColumnHeader header = new DataGridColumnHeader();
            headerStyle.TargetType = header.GetType();

            Setter setter = new Setter();
            setter.Property = FontSizeProperty;
            setter.Value = 10.0;
            headerStyle.Setters.Add(setter);

            headerStyle.Setters.Add(new Setter { Property = Control.BackgroundProperty, Value = Brushes.LightYellow });
            headerStyle.Setters.Add(new Setter { Property = Control.FontFamilyProperty, Value = new FontFamily("Verdana") });
            headerStyle.Setters.Add(new Setter { Property = Control.FontWeightProperty, Value = FontWeights.Bold });
            headerStyle.Setters.Add(new Setter { Property = Control.FontStyleProperty, Value = FontStyles.Italic });
            headerStyle.Setters.Add(new Setter { Property = Control.BorderThicknessProperty, Value = new Thickness(1) });
            headerStyle.Setters.Add(new Setter { Property = Control.BorderBrushProperty, Value = Brushes.Black });
            headerStyle.Setters.Add(new Setter { Property = Control.HorizontalContentAlignmentProperty, Value = HorizontalAlignment.Center });

            grdVehicles.Columns[4].HeaderStyle = headerStyle;
            grdVehicles.Columns[5].HeaderStyle = headerStyle;
            grdVehicles.Columns[6].HeaderStyle = headerStyle;
            grdVehicles.Columns[7].HeaderStyle = headerStyle;
            grdVehicles.Columns[8].HeaderStyle = headerStyle;

            Setter setterRow = new Setter();
            setterRow.Property = FontSizeProperty;
            setterRow.Value = 18.0;
            setterRow.Property = Control.ForegroundProperty;
            setterRow.Value = Brushes.Blue;
            setterRow.Property = Control.BackgroundProperty;
            setterRow.Value = Brushes.Pink;
            grdVehicles.RowStyle = new Style();
            grdVehicles.RowStyle.Setters.Add(setterRow);

        }

        private void BtnNewVehicle_Click(object sender, RoutedEventArgs e)
        {
            Vehicle vehicle = new Vehicle();
            MaintainVehicle maintainVehicle = new MaintainVehicle(vehicle);
            maintainVehicle.Owner = this;
            maintainVehicle.ShowDialog();

            vehicles.Add(vehicle);
            Rebind();
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEditVehicle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Vehicle vehicle = vehicles[grdVehicles.SelectedIndex];
                MaintainVehicle maintainVehicle = new MaintainVehicle(vehicle);
                maintainVehicle.Owner = this;
                maintainVehicle.ShowDialog();
                vehicles[grdVehicles.SelectedIndex] = vehicle;
                
                Rebind();
                throw new Exception("Trying to load vehicles");
            }
            catch (Exception ex)
            {
                logger.LogError("Error Editing Vehicle: " + ex.Message);
            }
        }

        private void cboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboFilter.SelectedIndex > -1)
            {
                filteredVehicles = vehicles.Where(v => v.ColorId == colors[cboFilter.SelectedIndex].Id).ToList();
                Rebind();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Reload();
        }
    }
}
