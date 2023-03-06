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
using WpfApp2.Models;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for Teacher.xaml
    /// </summary>
    public partial class Teacher : Window
    {
        private string id;
        HttpClient client = new HttpClient();
        private int examid;
        private string teacherid;

        public Teacher(string id)
        {
            this.id = id;
            InitializeComponent();

            client.BaseAddress = new Uri("https://localhost:7157/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async void btnloadexam_click(object sender, RoutedEventArgs e)
        {
            var response = await client.GetAsync($"Exams/name/{txtsearchexam.Text}");

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;

                // Deserialize the JSON string to a single Exam object instead of a List of Exam objects
                var exam = JsonConvert.DeserializeObject<Exam>(content);

                // Create a new List and add the single Exam object to it
                List<Exam> exams = new List<Exam>();
                exams.Add(exam);

                // Bind the List of Exam objects to the DataGrid
                dgExams.ItemsSource = exams;
            }
            else
            {
                MessageBox.Show($"Error: {response.ReasonPhrase}");

            }
        }



        private async void btnallexam_click(object sender, RoutedEventArgs e)
        {
            var response = await client.GetAsync($"Exams/GetExamsForTeacher?TeacherId={id}");
            // check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                // read the response content as string
                var content = await response.Content.ReadAsStringAsync();

                // check if the content is not null
                if (!string.IsNullOrEmpty(content))
                {
                    // deserialize the JSON string to a list of Exam objects
                    var exams = JsonConvert.DeserializeObject<List<Exam>>(content);

                    // bind the list of exams to the datagrid
                    dgExams.ItemsSource = exams;
                }
                else
                {
                    // handle the case where the content is null
                    MessageBox.Show("Error: response content is empty.", "Try again", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                // handle the error response
                MessageBox.Show($"Error: {response.ReasonPhrase}", "Try again", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        private async void btnallresult_click(object sender, RoutedEventArgs e)
        {

            var response = await client.GetAsync($"Examresults/result/{txtexamname.Text}");

            // Call the API and retrieve the data
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                // Handle the exception here, e.g. by logging an error message or notifying the user
                MessageBox.Show("Please Enter valid Exam name", "Try again", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            string jsonString = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON response into a list of objects
            List<ExamResult> results = JsonConvert.DeserializeObject<List<ExamResult>>(jsonString);

            // Assign the list of objects to the ItemsSource property of the DataGrid control
            dgresult.ItemsSource = results;
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create a new Exam object with the data from the form
            //string examDate = dtExamDate..ToString("yyyy-MM-dd"
            Exam exam = new Exam()
            {
                ExamName = txtNewExam.Text,
                TeacherId = id,
                QuestionCount = int.Parse(txtcount.Text),
                Duration = int.Parse(txtduration.Text),
                ExamDate = txtdate.Text,
                StartTime = txttime.Text,
                Randomized = checkrandom.IsChecked ?? false
            };

            // Send a POST request to the API endpoint to add the exam
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7157/");
                
                HttpResponseMessage response = await client.PostAsJsonAsync("api/Exams", exam);
                if (response.IsSuccessStatusCode)
                {
                    // Exam was added successfully
                    //MessageBox.Show("Exam added successfully.");
                    addquestion addquestion =new addquestion(txtcount.Text, txtNewExam.Text, id);
                    addquestion.Show();
                }
                else
                {
                    // There was an error adding the exam
                    MessageBox.Show("Error adding exam: " + response.ReasonPhrase, "Try again", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private async void ButtonSearch_Click_1(object sender, RoutedEventArgs e)
        {
            using (var httpClient = new HttpClient())
            {
                var url = "https://localhost:7157/api/Exams/name/" + txtNewExam1.Text; // replace with your API endpoint URL
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var exam = JsonConvert.DeserializeObject<Exam>(jsonString);
                    // exam object now contains the deserialized JSON data
                    // you can use the exam object to display the exam details in the UI
                    txtcount1.Text = exam.QuestionCount.ToString();
                    txtduration1.Text = exam.Duration.ToString();
                    txtdate1.Text = exam.ExamDate.ToString();
                    txttime1.Text = exam.StartTime.ToString();
                    checkrandom1.IsChecked = exam.Randomized.Equals(true);
                    examid = exam.ExamId;
                    teacherid = exam.TeacherId.ToString();
                }
                else
                {
                    // handle error response
                    MessageBox.Show("Enter valid exam name", "Try again", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Get the updated values from the UI
            int questionCount = int.Parse(txtcount1.Text);
            int duration = int.Parse(txtduration1.Text);
            string examDate = (txtdate1.Text);
            string startTime = (txttime1.Text);
            bool randomized = checkrandom1.IsChecked ?? false;

            // Create a new Exam object with the updated values
            Exam updatedExam = new Exam
            {
                ExamId = examid,
                ExamName = txtNewExam1.Text,
                TeacherId = teacherid,
                QuestionCount = questionCount,
                Duration = duration,
                ExamDate = examDate.ToString(),
                StartTime = startTime.ToString(),
                Randomized = randomized
            };

            // Send a PUT API request to update the exam in the database
            HttpClient client = new HttpClient();
            string apiUrl = $"https://localhost:7157/api/Exams/{examid}";
            HttpResponseMessage response = client.PutAsJsonAsync(apiUrl, updatedExam).Result;

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Display a message indicating that the exam was updated
                MessageBox.Show("Exam updated Succefully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                // Display an error message if the request failed
                MessageBox.Show("Failed to update exam", "Try again", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
