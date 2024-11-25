using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;

namespace FuncQuizzes.pages
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : Page
    {
        public History()
        {
            InitializeComponent();
            LoadHistoryData();
        }

        private void LoadHistoryData()
        {
            features.Database db = new features.Database($"{Directory.GetCurrentDirectory()}\\DATA\\FuncQuizzes.sqlite");

            DataTable dataTable = new DataTable();

            try
            {
                if (db != null)
                {

                    dataTable = db.SelectFromTable("History");

                    if (dataTable.Rows.Count > 0)
                    {
                        this.Grid02.Visibility = Visibility.Visible;
                        this.NoDataPage.Visibility = Visibility.Hidden;

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            string level = this.GetLevel(dataTable.Rows[i]["level"].ToString()!);

                            string image = this.GetCategoryImage(dataTable.Rows[i]["category"].ToString()!);

                            double result;
                            string GradeFetch = GradeCalculate(double.Parse(dataTable.Rows[i]["total_score"].ToString()!));
                            double.TryParse(GradeFetch, out result);

                            GradeFetch = this.GetGradeImage(GradeFetch);

                            DateTime date = DateTime.Parse(dataTable.Rows[i]["dateandtime"].ToString()!);

                            string duration = dataTable.Rows[i]["spend_time"].ToString()!;

                            components.history_card history_Card = new components.history_card()
                            {
                                Level = level,
                                ImageContent = new BitmapImage(new Uri($"pack://application:,,,//{image}")),
                                CorrectQuestNumber = dataTable.Rows[i]["answer_corect"].ToString()!,
                                IncorrectQuestNumber = dataTable.Rows[i]["answer_incorect"].ToString()!,
                                Grade = new BitmapImage(new Uri($"pack://application:,,,//{GradeFetch}")),
                                QuestDate = date.ToShortDateString(),
                                QuestDuration = duration != null ? duration.ToString() + " វិនាទី" : "Null",
                                TextColor = this.GetCategoryTextColor(dataTable.Rows[i]["category"].ToString()!),
                                LevelColor = this.GetLevelColor(dataTable.Rows[i]["level"].ToString()!),
                                CardColor = this.GetCategoryCardColor(dataTable.Rows[i]["category"].ToString()!),
                            };

                            if(i % 2 == 0)
                            {
                                this.StackColOne.Children.Add(history_Card);
                            }
                            else
                            {
                                this.StackColTwo.Children.Add(history_Card);
                            }
                        }
                    }
                    else
                    {
                        this.Grid02.Visibility = Visibility.Hidden;
                        this.NoDataPage.Visibility = Visibility.Visible;
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Error : No DATA : {ex.Message}");
            }
        }

        private string GetCategoryImage(string category)
        {
            return category switch
            {
                "C#" => "../assets/images/1.png",
                "C++" => "../assets/images/3.png",
                "Flutter" => "../assets/images/2.png",
                "HTML" => "../assets/images/5.png",
                "CSS" => "../assets/images/6.png",
                "JavaScript" => "../assets/images/7.png",
                _ => "../assets/images/4.png"
            };
        }

        private string GetGradeImage(string grade)
        {
            return grade switch
            {
                "A" => "../assets/images/GradeA.png",
                "B" => "../assets/images/GradeB.png",
                "C" => "../assets/images/GradeC.png",
                "D" => "../assets/images/GradeD.png",
                "E" => "../assets/images/GradeE.png",
                "F" => "../assets/images/GradeF.png",
                _ => "../assets/images/GradeF.png"
            };
        }

        private SolidColorBrush GetCategoryTextColor(string category)
        {
            return category switch
            {
                "C#" => new SolidColorBrush(Colors.White),
                "C++" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#09589c")),
                "Flutter" => new SolidColorBrush(Colors.Black),
                _ => new SolidColorBrush(Colors.White),
            };
        }

        private SolidColorBrush GetLevelColor(string level)
        {
            return level switch
            {
                "Beginer" => new SolidColorBrush(Colors.DarkGreen),
                "Advance" => new SolidColorBrush(Colors.Orange),
                _ => new SolidColorBrush(Colors.DarkRed),
            };
        }

        private string GetLevel(string level)
        {
            return level switch
            {
                "Beginer" => "ទាប",
                "Advance" => "មធ្យម",
                _ => "ខ្ពស់",
            };
        }

        private SolidColorBrush GetCategoryCardColor(string category)
        {
            return category switch
            {
                "C#" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8f76e4")),
                "C++" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#004482")),
                "Flutter" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2e3192")),
                "HTML" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f16529")),
                "CSS" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0096dc")),
                "JavaScript" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c6a517")),
                _ => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#a6a6a6")),
            };
        }

        private string GradeCalculate(double score)
        {
            if (score >= 90 && score <= 100)
            {
                return "A";
            }
            else if (score >= 80 && score < 90)
            {
                return "B";
            }
            else if (score >= 70 && score < 80)
            {
                return "C";
            }
            else if (score >= 60 && score < 70)
            {
                return "D";
            }
            else if (score >= 50 && score < 60)
            {
                return "E";
            }
            else
            {
                return "F";
            }
        }


        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(App.previousPage == new pages.HomeScreen())
            {
                App.SwitchPage(new pages.HomeScreen());
            }
            else
            {
                App.SwitchPage(App.previousPage!);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard RLAnimation = (Storyboard)FindResource("RLAnimation");
            Storyboard LRAnimation = (Storyboard)FindResource("LRAnimation");

            Storyboard.SetTarget(RLAnimation, StackColOne);
            Storyboard.SetTarget(LRAnimation, StackColTwo);
            RLAnimation.Begin();
            LRAnimation.Begin();
        }
    }
}
