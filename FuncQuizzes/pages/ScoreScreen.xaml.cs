using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FuncQuizzes.pages
{
    /// <summary>
    /// Interaction logic for ScoreScreen.xaml
    /// </summary>
    public partial class ScoreScreen : Page
    {
        private int finalScore;
        private MediaPlayer mediaPlayer;
        public ScoreScreen()
        {
            InitializeComponent();
            mediaPlayer = new MediaPlayer();
            finalScore = ((App)Application.Current).totalScore;
            textblockScore.Text = $"{finalScore.ToString()}";
            ((App)Application.Current).totalScore = 0;
            displaysound();
            loadHistory();
        }

        private void displaysound()
        {
            if (finalScore < 50 && finalScore >= 0)
            {
                mediaPlayer.Volume = 0.3;
                mediaPlayer.Open(new Uri(Directory.GetCurrentDirectory() + "\\DATA\\sound\\sad.mp3", UriKind.Absolute));
                mediaPlayer.Play();
            }
            else if (finalScore >= 50 && finalScore <= 100)
            {
                mediaPlayer.Volume = 0.3;
                mediaPlayer.Open(new Uri(Directory.GetCurrentDirectory() + "\\DATA\\sound\\congratulation.mp3", UriKind.Absolute));
                mediaPlayer.Play();
            }
        }
        private void buttonagain_Click(object sender, RoutedEventArgs e)
        {
            App.SwitchPage(new pages.selectcategory());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.SwitchPage(new pages.HomeScreen());
        }
        public void loadHistory()
        {
            // Define a List to hold each row of data from the History_Details table
            List<string[]> tblHistoryDetails = new List<string[]>();

            // Step 1: Open a database connection and query data
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=DATA\\FuncQuizzes.sqlite"))
            {
                connection.Open();

                // Query the latest history records using the most recent `his_id` from `History`
                string lastHistoryIdQuery = "SELECT MAX(his_id) FROM History";
                long lastHistoryId = (long)new SQLiteCommand(lastHistoryIdQuery, connection).ExecuteScalar();

                // Fetch question_text, chosen_answer, and is_correct from `History_Details`
                string query = @"SELECT question_text, chosen_answer, is_correct FROM History_Details WHERE his_id = @HistoryId";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@HistoryId", lastHistoryId);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Add each row of data to the list as an array of strings
                            tblHistoryDetails.Add(new string[]
                            {
                                reader["question_text"].ToString(),
                                reader["chosen_answer"].ToString(),
                                reader["is_correct"].ToString()
                            });
                        }
                    }
                }
            }

            // Step 2: Dynamically create and configure HistoryDetails components using data from tblHistoryDetails
            for (int i = 0; i < tblHistoryDetails.Count; i++)
            {
                FuncQuizzes.components.HistoryDetailsxaml temp = new FuncQuizzes.components.HistoryDetailsxaml
                {
                    // Populate each HistoryDetails control with data
                    QuestionText = tblHistoryDetails[i][0], // Question text
                    AnswerText = tblHistoryDetails[i][1],    // Chosen answer text
                    IsCorrectText = tblHistoryDetails[i][2]  // Correctness text
                };
                stack_answer.Children.Add(temp);
            }

        }
    }
}
