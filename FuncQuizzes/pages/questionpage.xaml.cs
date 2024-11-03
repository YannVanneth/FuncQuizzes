using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SQLite;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection.PortableExecutable;
using System.Data;

namespace FuncQuizzes.pages
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
    public partial class questionpage : Page
    {
        private int currentQuestionIndex = 0; 
        private List<Question> questions = new List<Question>();
        public questionpage()
        {
            InitializeComponent();
            LoadQuestion();
        }

        private void buttonanswer2_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                bool isCorrect = Convert.ToInt32(clickedButton.Tag) == 1;
                if (isCorrect)
                {
                    wrongrightname.Text = "ត្រឹមត្រូវ";
                    wrongrightname.Foreground = new SolidColorBrush(Colors.Green);
                    ((App)Application.Current).totalScore += 10;
                }
                else
                {
                    wrongrightname.Text = "មិនត្រឹមត្រូវ";
                    wrongrightname.Foreground = new SolidColorBrush(Colors.Red);
                }
                currentQuestionIndex++;
            }
        }
        //private void switchpage() 
        //{
        //    MainWindow? mainWindow = Application.Current.MainWindow as MainWindow;
        //    if (mainWindow != null)
        //    {
        //        mainWindow.Main.Content = new pages.selectcategory();
        //    }
        //}
        private void LoadQuestion()
        {
            
            int categoryid = ((App)Application.Current).GlobalCategoryId;
            int levelid = ((App)Application.Current).GlobalLevelId;


            SQLiteConnection con = new SQLiteConnection("Data source=funcquiz.sqlite");
            con.Open();
            string questionQuery = "SELECT id_question, question FROM tbl_questions WHERE id_category = @CategoryId AND id_level = @LevelId ORDER BY RANDOM() LIMIT 10";
            SQLiteCommand command = new SQLiteCommand(questionQuery, con);

            command.Parameters.AddWithValue("@CategoryId", categoryid);
            command.Parameters.AddWithValue("@LevelId", levelid);

            SQLiteDataReader reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                // No questions found, show message and switch page
                MessageBox.Show("No questions found for this category and level.");
                //switchpage();
                App.SwitchPage(new pages.selectcategory());
                return;
            }
            while (reader.Read())
            {
                questions.Add(new Question
                {
                    Id = Convert.ToInt32(reader["id_question"]),
                    Text = reader["question"].ToString()
                });
                if (questions.Count > 0)
                {
                    DisplayCurrentQuestion();
                }
            }

            //if (questions.Count > 0)
            //{
            //    DisplayCurrentQuestion();
            //}
            con.Close();
        }
        private void DisplayCurrentQuestion()
        {
            if (currentQuestionIndex < questions.Count)
            {
                var currentQuestion = questions[currentQuestionIndex];
                questionnam.Text = currentQuestion.Text;
                LoadAnswers(currentQuestion.Id);
            }
            else
            {
                //MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                //if (mainWindow != null)
                //{
                //    mainWindow.Main.Content = new pages.ScoreScreen();
                //}

                App.SwitchPage(new pages.ScoreScreen());
            }
        }
        private void LoadAnswers(int questionId)
        {
            string connectionString = "Data source=funcquiz.sqlite";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                //query to select answer 
                string answerQuery = "SELECT answer, is_correct FROM tbl_answer WHERE id_question = @QuestionId LIMIT 3";
                SQLiteCommand answerCommand = new SQLiteCommand(answerQuery, connection);

                //compare questionid from table question
                answerCommand.Parameters.AddWithValue("@QuestionId", questionId);
                using (SQLiteDataReader reader = answerCommand.ExecuteReader())
                {
                    int answerIndex = 0;
                    while (reader.Read())
                    {
                        //read answer from database
                        if (answerIndex == 0)
                        {
                            buttonanswer2.Content = reader["answer"].ToString();
                        }
                        else if (answerIndex == 1)
                        {
                            buttonanswer3.Content = reader["answer"].ToString();
                        }
                        else if (answerIndex == 2)
                        {
                            buttonanswer4.Content = reader["answer"].ToString();
                        }
                        
                        //check answer correct or incorrect
                        if (answerIndex == 0)
                        {
                            buttonanswer2.Tag = reader["is_correct"];
                        }
                        else if (answerIndex == 1)
                        {
                            buttonanswer3.Tag = reader["is_correct"];
                        }
                        else if (answerIndex == 2)
                        {
                            buttonanswer4.Tag = reader["is_correct"];
                        }
                        answerIndex++;
                    }
                }
            }
        }
        private void buttonNext_MouseDown(object sender, MouseButtonEventArgs e)
        {
            wrongrightname.Text = "";
            DisplayCurrentQuestion();
        }
    }
}
