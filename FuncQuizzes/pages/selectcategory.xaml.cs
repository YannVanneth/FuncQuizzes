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
    /// Interaction logic for selectcategory.xaml
    /// </summary>
    public partial class selectcategory : Page
    {
        public selectcategory()
        {
            InitializeComponent();
        }
        private void loadpage()
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.Main.Content = new pages.chooselevel();
            }
        }
        private void buttoncategorycpp_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button != null)
            {
                if (button.Content.ToString() == "CPP")
                {
                    ((App)Application.Current).GlobalCategoryId = 1;
                    loadpage();
                }
                else if (button.Content.ToString() == "C#")
                {
                    ((App)Application.Current).GlobalCategoryId = 2;
                    loadpage();
                }
                else if (button.Content.ToString() == "FLUTTER")
                {
                    ((App)Application.Current).GlobalCategoryId = 3;
                    loadpage();
                }
                else if (button.Content.ToString() == "ចំណេះដឺងទូទៅ")
                {
                    ((App)Application.Current).GlobalCategoryId = 4;
                    loadpage();
                }
                
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.Main.Content = new pages.HomeScreen();
            }
        }
    }
}
