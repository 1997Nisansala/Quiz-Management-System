using System;
using System.Collections.Generic;
using System.IO;
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
using Newtonsoft.Json;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for addquestion.xaml
    /// </summary>
    public partial class addquestion : Window
    {
        private string examName;
        private int count;
        private string teacherid;
        private List<Question> questions = new List<Question>();
        private int currentQuestionNumber = 1;

        public addquestion(string count,string examName, string teacherid)
        {
            this.examName = examName;
            this.count = int.Parse(count);
            this.teacherid = teacherid;
            InitializeComponent();
            txtexamname.Text = examName;
            txtQuestionNumber.Text = currentQuestionNumber.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveQuestion();

            // increment question number
            currentQuestionNumber++;

            // clear input fields
            ClearFields();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SaveQuestion();
            currentQuestionNumber++;
            if(currentQuestionNumber == count + 1)
            {
                MessageBox.Show("All questions are added succefully");
                this.Close();
            }
            ClearFields();
        }

        private void SaveQuestion()
        {
            // create a new question object with input values
            Question question = new Question()
            {
                ExamName = examName,
                QuestionNumber = currentQuestionNumber,
                QuestionText = txtQuestion.Text,
                Options = new List<string>() { txtOption1.Text, txtOption2.Text, txtOption3.Text, txtOption4.Text },
                CorrectOptionIndex = comboCorrectOption.SelectedIndex
            };

            // read existing questions from the JSON file
            List<Question> existingQuestions = new List<Question>();
            if (File.Exists("questions.json"))
            {
                string json = File.ReadAllText("questions.json");
                if (!string.IsNullOrEmpty(json))
                {
                    existingQuestions = JsonConvert.DeserializeObject<List<Question>>(json);
                }
            }

            // add the new question to the list of existing questions
            existingQuestions.Add(question);

            // serialize the list of questions to JSON and save to the file
            string updatedJson = JsonConvert.SerializeObject(existingQuestions);
            File.WriteAllText("questions.json", updatedJson);
        }

        private void ClearFields()
        {
            txtQuestionNumber.Text = currentQuestionNumber.ToString();
            txtQuestion.Text = "";
            txtOption1.Text = "";
            txtOption2.Text = "";
            txtOption3.Text = "";
            txtOption4.Text = "";
            comboCorrectOption.SelectedIndex = 0;
        }
    }

    public class Question
    {
        public string ExamName { get; set; }
        public int QuestionNumber { get; set; }
        public string QuestionText { get; set; }
        public List<string> Options { get; set; }
        public int CorrectOptionIndex { get; set; }
    }

}
