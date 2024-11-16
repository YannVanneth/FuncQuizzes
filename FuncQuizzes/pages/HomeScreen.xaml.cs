using FuncQuizzes.components;
using Microsoft.Win32;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FuncQuizzes.pages
{
    /// <summary>
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : Page
    {
        private ScrollViewer _scrollViewer;
        private StackPanel _stackPanel;
        public HomeScreen()
        {
            InitializeComponent();
            this.ActiveBorder(this.HomePageBorder.BorderBrush);
            CheckInfomation();
        }

        private void HistoryPage_Click(object sender, RoutedEventArgs e)
        {
            this.ActiveBorder(this.HistoryPageBorder.BorderBrush);
            App.SwitchPage(new pages.History());
        }

        private void AboutUsPage_Click(object sender, RoutedEventArgs e)
        {
            this.ActiveBorder(this.AboutUsPageBorder.BorderBrush);
            App.SwitchPage(new pages.AboutUs());
        }

        private void HomePage_Click(object sender, RoutedEventArgs e)
        {
            this.ActiveBorder(this.HomePageBorder.BorderBrush);
        }

        private void ActiveBorder(Brush border)
        {
            if (border == this.AboutUsPageBorder.BorderBrush)
            {
                this.AboutUsPageBorder.BorderBrush = new SolidColorBrush(Colors.Orange);
                this.HomePageBorder.BorderBrush = new SolidColorBrush(Colors.White);
                this.HistoryPageBorder.BorderBrush = new SolidColorBrush(Colors.White);

                this.AboutIcon.Color = new SolidColorBrush(Colors.Orange);
                this.HistoryIcon.Color = new SolidColorBrush(Colors.White);
                this.HomeIcon.Color = new SolidColorBrush(Colors.White);
            }

            else if (border == this.HomePageBorder.BorderBrush)
            {
                this.AboutUsPageBorder.BorderBrush = new SolidColorBrush(Colors.White);
                this.HomePageBorder.BorderBrush = new SolidColorBrush(Colors.Orange);
                this.HistoryPageBorder.BorderBrush = new SolidColorBrush(Colors.White);

                this.AboutIcon.Color = new SolidColorBrush(Colors.White);
                this.HistoryIcon.Color = new SolidColorBrush(Colors.White);
                this.HomeIcon.Color = new SolidColorBrush(Colors.Orange);
            }

            else if (border == this.HistoryPageBorder.BorderBrush)
            {
                this.HistoryPageBorder.BorderBrush = new SolidColorBrush(Colors.Orange);
                this.HomePageBorder.BorderBrush = new SolidColorBrush(Colors.White);
                this.AboutUsPageBorder.BorderBrush = new SolidColorBrush(Colors.White);

                this.AboutIcon.Color = new SolidColorBrush(Colors.White);
                this.HistoryIcon.Color = new SolidColorBrush(Colors.Orange);
                this.HomeIcon.Color = new SolidColorBrush(Colors.White);
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var mySender = (TextBox)sender;

            if (mySender.Text == "ស្វែងរក")
            {
                this.SearchBox.Text = "";
                this.SearchBox.Foreground = Brushes.Black;
            }

            else if (mySender.Text == "ឈ្មោះ")
            {
                this.NameBox.Text = "";
            }
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var mySender = (TextBox)sender;

            if (mySender.Text == "ស្វែងរក" || mySender.Text == string.Empty)
            {
                this.SearchBox.Text = "ស្វែងរក";
                this.SearchBox.Foreground = Brushes.Black;
            }

            else if (mySender.Text == "ឈ្មោះ" || mySender.Text == string.Empty)
            {
                this.NameBox.Text = "ឈ្មោះ";
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            features.Database database = new features.Database($"{Directory.GetCurrentDirectory()}\\DATA\\FuncQuizzes.sqlite");
            DataTable data = database.SelectFromTable("tbl_category");

            foreach (DataRow row in data.Rows)
            {
                if (row[1].ToString().ToLower().Trim().Contains(this.SearchBox.Text.ToLower().Trim()) && this.SearchBox.Text != string.Empty)
                {
                    _scrollViewer = null;

                    _scrollViewer = new ScrollViewer
                    {
                        Margin = new Thickness(20, 0, 0, 0),
                        Content = _stackPanel,
                        HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden,
                        VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                    };

                    _stackPanel = new StackPanel
                    {
                        Orientation = Orientation.Horizontal,
                    };
                }
            }

            if (this.SearchStack.Children.Count > 1)
            {
                this.SearchStack.Children.RemoveAt(1);
            }

            if (_stackPanel != null)
            {
                _stackPanel.Children.Clear();
            }

            foreach (DataRow row in data.Rows)
            {
                if (row[1].ToString().ToLower().Trim().Contains(this.SearchBox.Text.ToLower()) && this.SearchBox.Text != string.Empty)
                {
                    try
                    {
                        TopicCard topicCard = new TopicCard
                        {
                            Image = new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}\\DATA\\Assets\\{row[2]}.png")),
                            BackgroundColor = new SolidColorBrush(Colors.Transparent),
                            Cursor = Cursors.Hand,
                            Size = 40,
                            Raduis = 8,
                        };

                        topicCard.MouseDown += new components.ContentList().TopicCardEvent;

                        this._stackPanel.Children.Add(topicCard);
                        this.SearchStack.Children.Add(_scrollViewer);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show($"{ex.Message}");
                    }
                }
            }
        }

        private void SelectPicture_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "File name : *.jpg,*.png|*.jpg;*.png"};

            try
            {
                if (openFileDialog.ShowDialog() == true)
                {
                   string picture  = openFileDialog.FileName;

                   if (picture != string.Empty)
                   {
                      File.Copy(openFileDialog.FileName, $"{Directory.GetCurrentDirectory()}\\DATA\\Assets\\UserProfilePicture{Path.GetExtension(openFileDialog.FileName)}", true);
                   }
                }

            }

            catch (Exception ex) 
            {
                MessageBox.Show("Error System !!");
            }

            finally
            {
                try
                {
                    this.UserPicture.Background = new ImageBrush(new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}\\DATA\\Assets\\UserProfilePicture{Path.GetExtension(openFileDialog.FileName)}")));
                    this.UserProfile = $"UserProfilePicture{Path.GetExtension(openFileDialog.FileName)}";
                    this.UserImageProfile.ImageSource = new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}\\DATA\\Assets\\UserProfilePicture{Path.GetExtension(openFileDialog.FileName)}"));
                }
                catch(Exception ex){
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Registerbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.UserName = this.NameBox.Text;
                this.UserNameProfile.Text = this.UserName;

                StreamWriter file = new StreamWriter($"{Directory.GetCurrentDirectory()}\\DATA\\UserNameProfile.FuncQuizzes");

                if (UserProfile == string.Empty || this.UserImageProfile.ImageSource == null)
                {
                    MessageBox.Show("Sorry, we cannot find your profile picture !");
                    return;
                }

                if (UserName == string.Empty || this.NameBox.Text == string.Empty)
                {
                    MessageBox.Show("Sorry, we cannot find your name !");
                    return;
                }

                file.Write($"{this.UserName};{Path.GetFileName(this.UserProfile)}");
                file.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                CheckInfomation();
            }
        }

        private bool CheckInfomation()
        {
            string[]? lines = null;

            try
            {
                StreamReader read = new StreamReader($"{Directory.GetCurrentDirectory()}\\DATA\\UserNameProfile.FuncQuizzes");

                while(!read.EndOfStream)
                {
                    string line = read.ReadLine()!;
                    lines = line.Split(";");
                }

                read.Close();
            }
            catch
            {

            }
            finally
            {
                if (lines != null)
                {
                    this.UserName = lines[0];
                    this.UserProfile = lines[1];
                }
            }

            if (this.UserName == string.Empty || this.UserProfile == string.Empty)
            {
                this.EnterInfomation.Visibility = Visibility.Visible;
                this.GridBorder0Radius.Radius = this.GridBorder1Radius.Radius = 8;
                this.GridContent0.IsHitTestVisible = this.Grid1Content.IsHitTestVisible = false;
                return false;
            }

            else
            {
                this.EnterInfomation.Visibility = Visibility.Hidden;
                this.GridBorder0Radius.Radius = this.GridBorder1Radius.Radius = 0;
                this.GridContent0.IsHitTestVisible = this.Grid1Content.IsHitTestVisible = true;
                this.UserNameProfile.Text = this.UserName; this.UserImageProfile.ImageSource = new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}\\DATA\\Assets\\{this.UserProfile}"));
                return true;
            }
        }

        private string UserProfile { set; get; } = string.Empty;
        private string UserName { set; get; } = string.Empty;
    }
}
