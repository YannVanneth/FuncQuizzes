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
    /// Interaction logic for ScoreScreen.xaml
    /// </summary>
    public partial class ScoreScreen : Page
    {
        int finalScore;
        public ScoreScreen()
        {
            InitializeComponent();
            finalScore = ((App)Application.Current).totalScore;
            textblockScore.Text = $"{finalScore.ToString()}";
            ((App)Application.Current).totalScore = 0;
        }

        private void buttonagain_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            //if (mainWindow != null)
            //{
            //    mainWindow.Main.Content = new pages.selectcategory();
            //}

            App.SwitchPage(new pages.selectcategory());
        }
    }
}
