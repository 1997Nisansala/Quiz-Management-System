using Newtonsoft.Json;
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
using System.Xml.Linq;
using WpfApp2.Models;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for TeacherSignIn.xaml
    /// </summary>
    public partial class TeacherSignIn : Window
    {
        public TeacherSignIn()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private async void Button_Click_sign_in(object sender, RoutedEventArgs e)
        {
            // Get the ID and name from the text boxes
            string id = txtID.Text;
            string name = txtName.Text;

            // Create a new HttpClient instance
            using (HttpClient client = new HttpClient())
            {
                // Set the base URL of the API
                client.BaseAddress = new Uri("https://localhost:7157/");

                Teacher_temp teacher = new Teacher_temp()
                {
                    teacherId = id,
                    teacherName = name,
                };

                // Serialize the Student object to JSON
                string json = JsonConvert.SerializeObject(teacher);

                // Create a new StringContent object with the JSON data
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send a POST request to the API endpoint with the JSON data
                HttpResponseMessage response = await client.PostAsync("api/Teachers", content);

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Teacher Added Successfully", "Try Again", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Can't add Teacher", "Try Again", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                }
            }
        }
    }
}
