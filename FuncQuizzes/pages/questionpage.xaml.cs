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
        private DispatcherTimer quizTimer;
        private int countdownTime = 0; // Countdown time in seconds
        private int quizCountdownTime = 10 * 60;
        private int totalTime = 10 * 60;
        private int incorrectAnswerCount = 0;
        private int correctAnswerCount = 0;
        private int categoryid = 0;
        private int levelid = 0;
        private int totalQuestionCount = 10;
        public questionpage()
        {
            InitializeComponent();
            LoadQuestion();

            delayTimer = new DispatcherTimer();
            delayTimer.Interval = TimeSpan.FromSeconds(2);
            delayTimer.Tick += DelayTimer_Tick;

            quizTimer = new DispatcherTimer();
            quizTimer.Interval = TimeSpan.FromSeconds(1); // Tick every second
            quizTimer.Tick += QuizTimer_Tick;
            quizTimer.Start();

            // Display the initial time remaining
            UpdateQuizTimeDisplay();
        }
        private void buttonanswer2_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                clickedButton.IsEnabled = false;
                bool isCorrect = Convert.ToInt32(clickedButton.Tag) == 1;
                if (isCorrect)
                {
                    wrongrightname.Text = "ត្រឹមត្រូវ";
                    correctAnswerCount++;
                    wrongrightname.Foreground = new SolidColorBrush(Colors.Green);
                    ((App)Application.Current).totalScore += 10;
                }
                else
                {
                    wrongrightname.Text = "មិនត្រឹមត្រូវ";
                    incorrectAnswerCount++;
                    wrongrightname.Foreground = new SolidColorBrush(Colors.Red);
                }
                // Set the countdown time and start the delay timer
                countdownTime = 0; 
                delayTimer.Start();
            }
        }
        private void QuizTimer_Tick(object sender, EventArgs e)
        {
            quizCountdownTime--;

            // Update the displayed quiz time
            UpdateQuizTimeDisplay();

            if (quizCountdownTime <= 0)
            {
                // Stop the quiz timer when time runs out
                quizTimer.Stop();
                int totalTimespend = totalTime - quizCountdownTime;
                // Show a message or navigate to the score screen
                MessageBox.Show("Time's up! The quiz has ended.");
                App.SwitchPage(new pages.ScoreScreen());
            }
        }
        private void UpdateQuizTimeDisplay()
        {
            int minutes = quizCountdownTime / 60;
            int seconds = quizCountdownTime % 60;
            QuizTimeTextBlock.Text = $"{minutes:D2}:{seconds:D2}";
        }
        private void DelayTimer_Tick(object sender, EventArgs e)
        {
            countdownTime--;

            CountdownTextBlock.Text = $"Next question in: {countdownTime} seconds";

            if (countdownTime <= 0)
            {
                delayTimer.Stop();

                wrongrightname.Text = string.Empty;
                CountdownTextBlock.Text = string.Empty;

                currentQuestionIndex++;
                DisplayCurrentQuestion();
            }
        }
        private void LoadQuestion()
        {

            categoryid = ((App)Application.Current).GlobalCategoryId;
            levelid = ((App)Application.Current).GlobalLevelId;


            SQLiteConnection con = new SQLiteConnection("Data source=DATA\\FuncQuizzes.sqlite");
            con.Open();
            string questionQuery = "SELECT id_question, question FROM tbl_questions WHERE id_category = @CategoryId AND id_level = @LevelId ORDER BY RANDOM() LIMIT 10";
            SQLiteCommand command = new SQLiteCommand(questionQuery, con);

            command.Parameters.AddWithValue("@CategoryId", categoryid);
            command.Parameters.AddWithValue("@LevelId", levelid);
            SQLiteDataReader reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                MessageBox.Show("No questions found for this category and level.");
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
            con.Close();
        }
        private void DisplayQuestionCount()
        {
            CountdownTextBlock.Text = $"Question {currentQuestionIndex + 1} of {totalQuestionCount}";
        }
        private void DisplayCurrentQuestion()
        {
            
            if (currentQuestionIndex < questions.Count)
            {
                
                var currentQuestion = questions[currentQuestionIndex];
                questionnam.Text = currentQuestion.Text;
                LoadAnswers(currentQuestion.Id);
                buttonanswer2.IsEnabled = true;
                buttonanswer3.IsEnabled = true;
                buttonanswer4.IsEnabled = true;
                DisplayQuestionCount();
            }
            else
            {
                saveresulthistory();
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
                    string answerQuery = "SELECT answer, is_correct FROM tbl_answer WHERE id_question = @QuestionId ORDER BY RANDOM() LIMIT 3";
                    SQLiteCommand answerCommand = new SQLiteCommand(answerQuery, connection);

                    answerCommand.Parameters.AddWithValue("@QuestionId", questionId);

                    using (SQLiteDataReader reader = answerCommand.ExecuteReader())
                    {
                        int answerIndex = 0;
                        while (reader.Read())
                        {
                            string answerText = reader["answer"].ToString();

                            bool isCorrect = Convert.ToBoolean(reader["is_correct"]);

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
                MessageBox.Show("Please Select one answer only! ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void saveresulthistory()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection("Data Source=DATA\\FuncQuizzes.sqlite"))
                {
                    connection.Open();
                    string table_history = @"CREATE TABLE IF NOT EXISTS History(
                                        his_id INTEGER PRIMARY KEY AUTOINCREMENT,
                                        category TEXT,
                                        level TEXT,
                                        answer_incorect INTEGER,
                                        answer_corect INTEGER,
                                        total_score INTEGER,
                                        dateandtime TEXT,
                                        spend_time INTEGER)";
                    using (SQLiteCommand command = new SQLiteCommand(table_history, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    string categoryName = string.Empty;
                    string levelName = string.Empty;
                    string fetchCategoryQuery = "SELECT category FROM tbl_category WHERE id_category = @CategoryId";
                    using (SQLiteCommand fetchCategoryCommand = new SQLiteCommand(fetchCategoryQuery, connection))
                    {
                        fetchCategoryCommand.Parameters.AddWithValue("@CategoryId", categoryid);
                        categoryName = fetchCategoryCommand.ExecuteScalar()?.ToString() ?? "Unknown Category";
                    }
                    string fetchLevelQuery = "SELECT level FROM tbl_level WHERE id_level = @LevelId";
                    using (SQLiteCommand fetchLevelCommand = new SQLiteCommand(fetchLevelQuery, connection))
                    {
                        fetchLevelCommand.Parameters.AddWithValue("@LevelId", levelid);
                        levelName = fetchLevelCommand.ExecuteScalar()?.ToString() ?? "Unknown Level";
                    }                    int durationInSeconds = totalTime - quizCountdownTime;
                    string durationFormatted = $"{durationInSeconds / 60:D2}:{durationInSeconds % 60:D2}";
                    
                    string insertQuery = @"INSERT INTO History (category, level, answer_incorect, answer_corect, total_score, dateandtime, spend_time) 
                                   VALUES (@Category, @Level, @IncorrectAnswers, @CorrectAnswers, @Score, @DateAndTime, @Duration)";

                    using (SQLiteCommand commandInsert = new SQLiteCommand(insertQuery, connection))
                    {
                        commandInsert.Parameters.AddWithValue("@Category", categoryName);
                        commandInsert.Parameters.AddWithValue("@Level", levelName);
                        commandInsert.Parameters.AddWithValue("@IncorrectAnswers", incorrectAnswerCount);
                        commandInsert.Parameters.AddWithValue("@CorrectAnswers", correctAnswerCount);
                        commandInsert.Parameters.AddWithValue("@Score", ((App)Application.Current).totalScore);
                        commandInsert.Parameters.AddWithValue("@DateAndTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        commandInsert.Parameters.AddWithValue("@Duration", durationFormatted);

                        commandInsert.ExecuteNonQuery();
                    }
                    MessageBox.Show("អ្នកបានឆ្លើយសំណួរដោយជោគជ័យ", "អប់អរសាទរ", MessageBoxButton.OK, MessageBoxImage.Information);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}
