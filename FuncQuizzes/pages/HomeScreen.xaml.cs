using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

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

            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.Main.Content = new pages.AboutUs();

                if(mainWindow.Main.Content is HomeScreen)
                {
                  this.ActiveBorder(this.HomePageBorder.BorderBrush);
                }
            }

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
    }
}
