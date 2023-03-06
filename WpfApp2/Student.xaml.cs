using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows;
using WpfApp2.Models;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for Student.xaml
    /// </summary>
    public partial class Student : Window
    {
        private string id;
        private int currentQuestionIndex = 0;
        private int currentAnswerIndex = 0;
        private int score = 0;
        private float scoreScore = 0;
        private int examid;
        private List<Question> filteredQuestions;
        private string wronganswers;
        HttpClient client2 = new HttpClient();

        public Student(string id)
        {
            this.id = id;
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime today = DateTime.Today;
            currentQuestionIndex = 0;
            currentAnswerIndex = 0;
            score = 0;

            HttpResponseMessage response = await client2.GetAsync("https://localhost:7157/api/Exams/name/"+txtexamname.Text);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            Exam exam = Newtonsoft.Json.JsonConvert.DeserializeObject<Exam>(responseBody);

            DateTime givendate = DateTime.Parse(exam.ExamDate);
            TimeSpan difference = givendate - today;
            if (difference.Days <= 0)
            {
                // Get the search query from the search box
                string searchQuery = txtexamname.Text;

                // Load the JSON file containing the questions and answers
                string jsonFilePath = "questions.json";
                string json;
                try
                {
                    json = File.ReadAllText(jsonFilePath);
                }
                catch (IOException ex)
                {
                    // Handle the exception by displaying an error message, logging it, or rethrowing it
                    MessageBox.Show("No json file can be found", "Try again", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var questions = JsonConvert.DeserializeObject<List<Question>>(json);

                // Filter the questions by the search query
                filteredQuestions = questions.Where(q => q.ExamName.Contains(searchQuery)).ToList();

                // Display the first question and answers in the output box
                if (filteredQuestions.Count > 0)
                {
                    Question currentQuestion = filteredQuestions[0];
                    txtquestion.Text = currentQuestion.QuestionText;
                    lbqnumber.Content = currentQuestion.QuestionNumber;
                    lbq1.Content = currentQuestion.Options[0];
                    lbq2.Content = currentQuestion.Options[1];
                    lbq3.Content = currentQuestion.Options[2];
                    lbq4.Content = currentQuestion.Options[3];
                }
                else
                {
                    MessageBox.Show("Please Enter valid Exam name", "Try again", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("The Exam is not started yet", "Try again", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            }
            
        }


        private async void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            scoreScore = ((float)score / filteredQuestions.Count) * 100;
            // Get the ExamID from the API
            string examName = txtexamname.Text;
            string apiUrl = "https://localhost:7157/api/Exams/exname/" + examName;
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var examID = await response.Content.ReadAsStringAsync();
                    examid = int.Parse(examID);
                }
                else
                {
                    MessageBox.Show("Failed to get ExamID from the server.");
                    return;
                }
            }


            // Create the result object
            var result = new ExamResult
            {
                StudentID = this.id,
                ExamID = examid, // or get it from somewhere else
                Grade = scoreScore,
                Errors = wronganswers // set it to the actual value
            };

            // Serialize the result object to JSON
            var json = JsonConvert.SerializeObject(result);

            // Create a new HTTP client
            using (var client = new HttpClient())
            {
                // Set the API endpoint URL
                client.BaseAddress = new Uri("https://localhost:7157/api/");

                // Send the POST request
                //var response = await client.PostAsync("Examresults", content);
                HttpResponseMessage response = await client.PostAsJsonAsync("Examresults", result);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Show the exam result to the user
                    MessageBox.Show("You have completed the exam. Your score is " + scoreScore + "%.", "Exam Complete", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Reset all checkboxes and textboxes
                    cka1.IsChecked = false;
                    cka2.IsChecked = false;
                    cka3.IsChecked = false;
                    cka4.IsChecked = false;
                    txtexamname.Text = "";
                    txtquestion.Text = "";
                    lbqnumber.Content = "";
                    lbq1.Content = "";
                    lbq2.Content = "";
                    lbq3.Content = "";
                    lbq4.Content = "";
                    currentQuestionIndex = 0;
                    currentAnswerIndex = 0;
                    wronganswers = "";
                    score = 0;

                }
                else
                {
                    // Show an error message to the user
                    MessageBox.Show("Failed to submit the exam result. Please try again later.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnsubmit_Click(object sender, RoutedEventArgs e)
        {
            // Check if an answer has been selected for the current question
            if (cka1.IsChecked == false && cka2.IsChecked == false && cka3.IsChecked == false && cka4.IsChecked == false)
            {
                MessageBox.Show("Please select an answer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Compare the selected answer with the correct answer of the current question
            string selectedAnswer = "";
            if (cka1.IsChecked == true)
            {
                selectedAnswer = lbq1.Content.ToString();
                currentAnswerIndex = 0;
            }
            else if (cka2.IsChecked == true)
            {
                selectedAnswer = lbq2.Content.ToString();
                currentAnswerIndex = 1;
            }
            else if (cka3.IsChecked == true)
            {
                selectedAnswer = lbq3.Content.ToString();
                currentAnswerIndex = 2;
            }
            else if (cka4.IsChecked == true)
            {
                selectedAnswer = lbq4.Content.ToString();
                currentAnswerIndex = 3;
            }

            if (currentAnswerIndex == filteredQuestions[currentQuestionIndex].CorrectOptionIndex)
            {
                // Increment the score if the answer is correct
                score++;
            }
            else
            {
                wronganswers += "(" + currentQuestionIndex+1 + ":" + selectedAnswer + "),";
            }

            // Load the next question or end the exam
            currentQuestionIndex++;
            if (currentQuestionIndex < filteredQuestions.Count)
            {
                Question currentQuestion = filteredQuestions[currentQuestionIndex];
                txtquestion.Text = currentQuestion.QuestionText;
                lbqnumber.Content = currentQuestion.QuestionNumber;
                lbq1.Content = currentQuestion.Options[0];
                lbq2.Content = currentQuestion.Options[1];
                lbq3.Content = currentQuestion.Options[2];
                lbq4.Content = currentQuestion.Options[3];
                cka1.IsChecked = false;
                cka2.IsChecked = false;
                cka3.IsChecked = false;
                cka4.IsChecked = false;
                
            }
            else
            {
                MessageBox.Show("Completed all the questions");
            }
        }

        private void txtnext_Click(object sender, RoutedEventArgs e)
        {
            currentQuestionIndex++;
            if (currentQuestionIndex < filteredQuestions.Count)
            {
                Question currentQuestion = filteredQuestions[currentQuestionIndex];
                txtquestion.Text = currentQuestion.QuestionText;
                lbqnumber.Content = currentQuestion.QuestionNumber;
                lbq1.Content = currentQuestion.Options[0];
                lbq2.Content = currentQuestion.Options[1];
                lbq3.Content = currentQuestion.Options[2];
                lbq4.Content = currentQuestion.Options[3];
                cka1.IsChecked = false;
                cka2.IsChecked = false;
                cka3.IsChecked = false;
                cka4.IsChecked = false;
            }
            else
            {
                MessageBox.Show("Completed all the questions");
            }
        }

        private void txtprevious_Click(object sender, RoutedEventArgs e)
        {
            currentQuestionIndex--;
            if (currentQuestionIndex >= 0)
            {
                Question currentQuestion = filteredQuestions[currentQuestionIndex];
                txtquestion.Text = currentQuestion.QuestionText;
                lbqnumber.Content = currentQuestion.QuestionNumber;
                lbq1.Content = currentQuestion.Options[0];
                lbq2.Content = currentQuestion.Options[1];
                lbq3.Content = currentQuestion.Options[2];
                lbq4.Content = currentQuestion.Options[3];
                cka1.IsChecked = false;
                cka2.IsChecked = false;
                cka3.IsChecked = false;
                cka4.IsChecked = false;
            }
            else
            {
                txtquestion.Text = "";
                lbqnumber.Content = "";
                lbq1.Content = "";
                lbq2.Content = "";
                lbq3.Content = "";
                lbq4.Content = "";
                cka1.IsChecked = false;
                cka2.IsChecked = false;
                cka3.IsChecked = false;
                cka4.IsChecked = false;
            }
        }

        public class Answer
        {
            public int qno { get; set; }
            public string givenanswer { get; set; }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
