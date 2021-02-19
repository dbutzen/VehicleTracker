using DTB.VehicleTracker.BL;
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
    /// Interaction logic for MaintainColors.xaml
    /// </summary>
    public partial class MaintainColors : Window
    {

        List<BL.Models.Color> colors;
        BL.Models.Color color;
        public MaintainColors()
        {
            InitializeComponent();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                color = colors[cboAttribute.SelectedIndex];
                
                Task.Run(async () =>
                {
                    int results = await ColorManager.Delete(color.Id);
                });

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void CboAttribute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cboAttribute.SelectedIndex > -1)
            {
                color = colors[cboAttribute.SelectedIndex];
                txtDescription.Text = color.Description;

                byte[] colorCode = BitConverter.GetBytes(color.Code);
                cpCode.SelectedColor = System.Windows.Media.Color.FromRgb(colorCode[2], 
                                                                          colorCode[1],
                                                                          colorCode[0]);
            }
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                color = new BL.Models.Color();
                color.Description = txtDescription.Text;

                color.Code = BitConverter.ToInt32(new byte[] { cpCode.SelectedColor.Value.B, cpCode.SelectedColor.Value.G, cpCode.SelectedColor.Value.R, 0x00 }, 0);

                Task.Run(async () =>
                {
                    int results = await ColorManager.Insert(color);
                });

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                color = colors[cboAttribute.SelectedIndex];
                color.Description = txtDescription.Text;

                color.Code = BitConverter.ToInt32(new byte[] { cpCode.SelectedColor.Value.B, cpCode.SelectedColor.Value.G, cpCode.SelectedColor.Value.R, 0x00 }, 0);

                Task.Run(async () =>
                {
                    int results = await ColorManager.Update(color);
                });

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            var colorProperty = typeof(Colors).GetProperties()
                                .FirstOrDefault(p => (System.Windows.Media.Color)(p.GetValue(p, null)) == e.NewValue);
            color = new BL.Models.Color();
            txtDescription.Text = colorProperty != null ? colorProperty.Name : color.Description;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Reload();
        }

        private async void Reload()
        {
            colors = (List<BL.Models.Color>)await ColorManager.Load();
            cboAttribute.ItemsSource = null;
            cboAttribute.ItemsSource = colors;
            cboAttribute.DisplayMemberPath = "Description"; //what's shown
            cboAttribute.SelectedValuePath = "Id"; //Primary key

        }
    }
}
