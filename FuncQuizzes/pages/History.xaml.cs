using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

                    if(dataTable.Rows.Count >= 0)
                    {

                        this.Grid02.Visibility = Visibility.Visible;
                        this.NoDataPage.Visibility = Visibility.Hidden;

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            string level = dataTable.Rows[i]["level"].ToString()! == "Beginer" ? "ងាយស្រួល" : dataTable.Rows[i]["level"].ToString()! == "Advance" ? "មធ្យម" : "លំបាក";


                            string image = dataTable.Rows[i]["category"].ToString()! == "C#" ? "../assets/images/1.png" : dataTable.Rows[i]["category"].ToString()! == "C++"
                                                ? "../assets/images/3.png" : dataTable.Rows[i]["category"].ToString()! == "Flutter" ? "../assets/images/2.png" : "../assets/images/4.png";

                            double result;
                            string GradeFetch = dataTable.Rows[i]["total_score"].ToString()!;

                            double.TryParse(GradeFetch, out result);

                            switch (GradeCalculate(result))
                            {
                                case "A":
                                    GradeFetch = "../assets/images/GradeA.png";
                                    break;
                                case "B":
                                    GradeFetch = "../assets/images/GradeB.png";
                                    break;
                                case "C":
                                    GradeFetch = "../assets/images/GradeC.png";
                                    break;
                                case "D":
                                    GradeFetch = "../assets/images/GradeD.png";
                                    break;
                                case "E":
                                    GradeFetch = "../assets/images/GradeE.png";
                                    break;
                                case "F":
                                    GradeFetch = "../assets/images/GradeF.png";
                                    break;
                            }


                            DateTime date = DateTime.Parse(dataTable.Rows[i]["dateandtime"].ToString()!);

                            string duration = dataTable.Rows[i]["spend_time"].ToString()!;

                            SolidColorBrush TextColor = dataTable.Rows[i]["category"].ToString()! == "C#" ? new SolidColorBrush(Colors.White) : dataTable.Rows[i]["category"].ToString()! == "C++"
                                                ? (SolidColorBrush)new BrushConverter().ConvertFrom("#09589c")! : dataTable.Rows[i]["category"].ToString()! == "Flutter" ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.White);

                            SolidColorBrush levelColor = dataTable.Rows[i]["level"].ToString()! switch
                            {
                                "Beginer" => new SolidColorBrush(Colors.DarkGreen),
                                "Advance" => new SolidColorBrush(Colors.Orange),
                                _ => new SolidColorBrush(Colors.DarkRed)
                            };

                            SolidColorBrush cardColor = dataTable.Rows[i]["category"].ToString()! switch
                            {
                                "C#" => (SolidColorBrush)new BrushConverter().ConvertFrom("#8f76e4")!,
                                "C++" => (SolidColorBrush)new BrushConverter().ConvertFrom("#004482")!,
                                "Flutter" => (SolidColorBrush)new BrushConverter().ConvertFrom("#2e3192")!,
                                _ => (SolidColorBrush)new BrushConverter().ConvertFrom("#a6a6a6")!
                            };

                            components.history_card history_Card = new components.history_card()
                            {
                                Level = level,
                                ImageContent = new BitmapImage(new Uri($"pack://application:,,,//{image}")),
                                CorrectQuestNumber = dataTable.Rows[i]["answer_corect"].ToString()!,
                                IncorrectQuestNumber = dataTable.Rows[i]["answer_incorect"].ToString()!,
                                Grade = new BitmapImage(new Uri($"pack://application:,,,//{GradeFetch}")),
                                QuestDate = date.ToShortDateString(),
                                QuestDuration = duration != null ? duration.ToString() + " វិនាទី" : "Null",
                                TextColor = TextColor,
                                LevelColor = levelColor,
                                CardColor = cardColor,
                            };

                            int Col = i % 2;

                            switch (Col)
                            {
                                case 0:
                                    this.StackColOne.Children.Add(history_Card);
                                    break;
                                case 1:
                                    this.StackColTwo.Children.Add(history_Card);
                                    break;
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

            catch(Exception ex)
            {
                MessageBox.Show($"Error : No DATA : {ex.Message}");
            }
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
            App.SwitchPage(new pages.HomeScreen());
        }
    }


}
