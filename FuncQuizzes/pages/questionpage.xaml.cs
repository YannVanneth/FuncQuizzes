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
using System.Windows.Threading;

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
        private DispatcherTimer delayTimer;
        private int countdownTime = 2; // Countdown time in seconds
        public questionpage()
        {
            InitializeComponent();
            LoadQuestion();

            // Initialize the timer for the delay
            delayTimer = new DispatcherTimer();
            delayTimer.Interval = TimeSpan.FromSeconds(2); // Set the delay duration (e.g., 2 seconds)
            delayTimer.Tick += DelayTimer_Tick;
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
                // Set the countdown time and start the delay timer
                countdownTime = 2; // Set your delay duration in seconds
                CountdownTextBlock.Text = $"Next question in: {countdownTime} seconds";
                delayTimer.Start();
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
        // This event triggers after the delay to show the next question
        private void DelayTimer_Tick(object sender, EventArgs e)
        {
            countdownTime--;

            // Update the countdown display on the form
            CountdownTextBlock.Text = $"Next question in: {countdownTime} seconds";

            if (countdownTime <= 0)
            {
                // Stop the timer when countdown finishes
                delayTimer.Stop();

                // Clear the feedback and countdown text
                wrongrightname.Text = string.Empty;
                CountdownTextBlock.Text = string.Empty;

                // Move to the next question
                currentQuestionIndex++;
                DisplayCurrentQuestion();
            }
        }
        private void LoadQuestion()
        {

            int categoryid = ((App)Application.Current).GlobalCategoryId;
            int levelid = ((App)Application.Current).GlobalLevelId;


            SQLiteConnection con = new SQLiteConnection("Data source=DATA\\FuncQuizzes.sqlite");
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
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection("Data source=DATA\\FuncQuizzes.sqlite"))
                {
                    connection.Open();

                    // SQL query to select answers for the specified question
                    string answerQuery = "SELECT answer, is_correct FROM tbl_answer WHERE id_question = @QuestionId LIMIT 3";
                    SQLiteCommand answerCommand = new SQLiteCommand(answerQuery, connection);

                    // Set the parameter
                    answerCommand.Parameters.AddWithValue("@QuestionId", questionId);

                    // Attempt to execute the reader
                    using (SQLiteDataReader reader = answerCommand.ExecuteReader())
                    {
                        int answerIndex = 0;
                        while (reader.Read())
                        {
                            // Process the answers here
                            string answerText = reader["answer"].ToString();
                            bool isCorrect = Convert.ToBoolean(reader["is_correct"]);

                            // Assign answer and correctness to UI buttons
                            switch (answerIndex)
                            {
                                case 0:
                                    buttonanswer2.Content = answerText;
                                    buttonanswer2.Tag = isCorrect;
                                    break;
                                case 1:
                                    buttonanswer3.Content = answerText;
                                    buttonanswer3.Tag = isCorrect;
                                    break;
                                case 2:
                                    buttonanswer4.Content = answerText;
                                    buttonanswer4.Tag = isCorrect;
                                    break;
                            }

                            answerIndex++;
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("SQLite error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

    }
}