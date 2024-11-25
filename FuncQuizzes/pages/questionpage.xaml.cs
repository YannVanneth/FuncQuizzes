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
using System.Media;
using System.Windows.Media;
using System.Windows.Threading;

namespace FuncQuizzes.pages
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
    public class AnswerDetail
    {
        public int QuestionId {  get; set; }
        public string QuestionText {  get; set; }
        public string ChosenAnswerText {  get; set; }
        public bool IsCorrect {  get; set; }
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
        private List<AnswerDetail> answerDetails = new List<AnswerDetail>();
        private MediaPlayer mediaPlayer;
        public questionpage()
        {
            InitializeComponent();
            LoadQuestion();
            mediaPlayer = new MediaPlayer();
            quizTimer = new DispatcherTimer();
            quizTimer.Interval = TimeSpan.FromSeconds(1); // Tick every second
            quizTimer.Tick += QuizTimer_Tick;
            quizTimer.Start();
            displaysound();
            mediaPlayer.Play();
            // Display the initial time remaining
            UpdateQuizTimeDisplay();
        }
        private void displaysound()
        { 
            mediaPlayer.Volume = 0.3;
            mediaPlayer.Open(new Uri(Directory.GetCurrentDirectory() + "\\DATA\\sound\\quiztime.mp3", UriKind.Absolute));
        }
        private void buttonanswer2_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                //clickedButton.IsEnabled = false;
                bool isCorrect = Convert.ToInt32(clickedButton.Tag) == 1;
                var existingAnswer = answerDetails.FirstOrDefault(a => a.QuestionId == questions[currentQuestionIndex].Id);
                if (existingAnswer == null) 
                {
                    answerDetails.Add(new AnswerDetail
                    {
                        QuestionId = questions[currentQuestionIndex].Id,
                        QuestionText = questions[currentQuestionIndex].Text,
                        ChosenAnswerText = clickedButton.Content.ToString(),
                        IsCorrect = isCorrect
                    });
                }

                if (isCorrect)
                {
                    correctAnswerCount++;
                    ((App)Application.Current).totalScore += 10;
                }
                else
                {
                    incorrectAnswerCount++;
                }
                currentQuestionIndex++;
                DisplayCurrentQuestion();
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
                DisplayQuestionCount();
            }
            else
            {
                mediaPlayer.Stop();
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

                    string table_historyDetails = @"CREATE TABLE IF NOT EXISTS History_Details(
                                                detail_id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                his_id INTEGER,
                                                question_text TEXT,
                                                chosen_answer TEXT,
                                                is_correct TEXT,
                                                FOREIGN KEY(his_id) REFERENCES History(his_id))";
                    using(SQLiteCommand command = new SQLiteCommand(table_historyDetails, connection))
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
                        quizTimer.Stop();
                        commandInsert.ExecuteNonQuery();
                    }

                    long lastHistoryId = connection.LastInsertRowId;
                    string detailinsert = @"INSERT INTO History_Details (his_id,question_text,chosen_answer, is_correct)
                                        VALUES (@HistoryId, @QuestionText,@ChosenAnswer,@IsCorrect)";

                    foreach(var answerdetail in answerDetails)
                    {
                        using(SQLiteCommand commandInsert = new SQLiteCommand(detailinsert, connection))
                        {
                            commandInsert.Parameters.AddWithValue("@HistoryId",lastHistoryId);
                            commandInsert.Parameters.AddWithValue("@QuestionText", answerdetail.QuestionText);
                            commandInsert.Parameters.AddWithValue("@ChosenAnswer", answerdetail.ChosenAnswerText);
                            string IsCorrectText = answerdetail.IsCorrect ? "ត្រឹមត្រូវ" : "មិនត្រឹមត្រូវ";
                            commandInsert.Parameters.AddWithValue("@IsCorrect",IsCorrectText);

                            commandInsert.ExecuteNonQuery();
                        }
                    }
                    
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
