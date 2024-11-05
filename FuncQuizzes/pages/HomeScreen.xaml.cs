using FuncQuizzes.components;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
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
        }

        private void HistoryPage_Click(object sender, RoutedEventArgs e)
        {
            this.ActiveBorder(this.HistoryPageBorder.BorderBrush);
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
            if (this.SearchBox.Text == "ស្វែងរក")
            {
                this.SearchBox.Text = "";
                this.SearchBox.Foreground = Brushes.Black;
            }
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.SearchBox.Text == "ស្វែងរក" || this.SearchBox.Text == string.Empty)
            {
                this.SearchBox.Text = "ស្វែងរក";
                this.SearchBox.Foreground = Brushes.Black;
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            features.Database database = new features.Database("DATA\\FuncQuizzes.sqlite");
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
    }
}
