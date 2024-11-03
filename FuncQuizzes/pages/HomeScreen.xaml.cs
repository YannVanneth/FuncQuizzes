using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace FuncQuizzes.pages
{
    /// <summary>
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : Page
    {
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
    }
}
