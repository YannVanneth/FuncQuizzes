using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace FuncQuizzes
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MainWindow mainWindow = new MainWindow();

        public static void SwitchPage(Page page)
        {
            mainWindow.Main.Content = page;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            mainWindow.Show();
        }
    }

}
