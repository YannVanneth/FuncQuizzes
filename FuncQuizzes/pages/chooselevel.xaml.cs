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

namespace FuncQuizzes.pages
{
    /// <summary>
    /// Interaction logic for chooselevel.xaml
    /// </summary>
    public partial class chooselevel : Page
    {
        public chooselevel()
        {
            InitializeComponent();
        }

        private void chooselevelback_Click(object sender, RoutedEventArgs e)
        {
            MainWindow? mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null)
            {
                mainWindow.Main.Content = new pages.HomeScreen();
            }
        }

        private void loadpage()
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.Main.Content = new pages.questionpage();
            }
        }

        private void buttonbeginer_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button != null)
            {
                if (button.Content.ToString() == "កម្រិតទាប")
                {
                    ((App)Application.Current).GlobalLevelId = 1;
                    loadpage();
                }
                else if (button.Content.ToString() == "កម្រិតមធ្យម")
                {
                    ((App)Application.Current).GlobalLevelId = 2;
                    loadpage();
                }
                else if (button.Content.ToString() == "កម្រិតខ្ពស់")
                {
                    ((App)Application.Current).GlobalLevelId = 3;
                    loadpage();
                }
                
            }
        }

        private void chooselevelhome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.Main.Content = new pages.selectcategory();
            }
        }
    }
}
