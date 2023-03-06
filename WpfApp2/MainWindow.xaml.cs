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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp2.Models;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpClient client = new HttpClient();
        public MainWindow()
        {
            client.BaseAddress = new Uri("https://localhost:7157/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            InitializeComponent();
        }

        private async void btnlogin_Click(object sender, RoutedEventArgs e)
        {
            string id = txtid.Text;
           

            if (cmbselection.SelectedIndex == 0) // Student selected
            {
                // Check if the entered ID exists in the student database
                var response = await client.GetAsync($"Students/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Invalid ID", "Try again", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // If ID is valid, open student window
                Student studentWindow = new Student(id);
                studentWindow.Show();
                this.Close();
            }
            else if (cmbselection.SelectedIndex == 1) // Teacher selected
            {
                // Check if the entered ID exists in the teacher database
                var response = await client.GetAsync($"Teachers/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Invalid ID", "Try again", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // If ID is valid, open teacher window
                Teacher teacherWindow = new Teacher(id);
                teacherWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please Select a User Type", "Try again", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private async void GetStudents()
        {
            var response = await client.GetStringAsync("Students");
            var students = JsonConvert.DeserializeObject<List<Student>>(response);
            MessageBox.Show(response);
        }

        private void btnsignin_Click(object sender, RoutedEventArgs e)
        {
            if(cmbselection.SelectedIndex == 0)
            {
                StudentSignIn student = new StudentSignIn();
                student.Show();
                this.Close();
            }
            else
            {
                TeacherSignIn teacher = new TeacherSignIn();
                teacher.Show();
                this.Close();
            }
        }
    }
}
