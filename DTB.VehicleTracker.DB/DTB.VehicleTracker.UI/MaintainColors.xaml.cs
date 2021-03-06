using DTB.VehicleTracker.BL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.DeleteAsync("Color/" + color.Id).Result;
                /*Task.Run(async () =>
                {
                    int results = await ColorManager.Delete(color.Id);
                });*/

                colors.Remove(color);
                Rebind(0);
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



                HttpClient client = InitializeClient();
                string serializedObject = JsonConvert.SerializeObject(color);
                var content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PostAsync("Color", content).Result;

                /*Task.Run(async () =>
                {
                    int results = await ColorManager.Insert(color);
                });
                */

                colors.Add(color);
                Rebind(colors.Count - 1);
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


                HttpClient client = InitializeClient();
                string serializedObject = JsonConvert.SerializeObject(color);
                var content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PutAsync("Color/" + color.Id, content).Result;

                /*Task.Run(async () =>
                {
                    int results = await ColorManager.Update(color);
                });*/

                colors[cboAttribute.SelectedIndex] = color;
                Rebind(cboAttribute.SelectedIndex);
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
        private async void Rebind(int index)
        {
            cboAttribute.ItemsSource = null;
            cboAttribute.ItemsSource = colors;
            cboAttribute.DisplayMemberPath = "Description";
            cboAttribute.SelectedValuePath = "Id";
            cboAttribute.SelectedIndex = index;
        }

        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("https://localhost:44343/");
            client.BaseAddress = new Uri("https://vehicletrackerapi.azurewebsites.net/api/");
            return client;
        }

        private async void Reload()
        {
            //colors = (List<BL.Models.Color>)await ColorManager.Load();

            HttpClient client = InitializeClient();
            HttpResponseMessage response;
            string result;
            dynamic items;

            // Call API controller/get
            response = client.GetAsync("Color").Result;
            // Get json
            result = response.Content.ReadAsStringAsync().Result;
            // split json into array
            items = (JArray)JsonConvert.DeserializeObject(result);
            // convert jarray to list<color>
            colors = items.ToObject<List<BL.Models.Color>>();



            Rebind(0);

        }
    }
}
