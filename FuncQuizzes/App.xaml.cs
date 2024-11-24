using FuncQuizzes.components;
using System.Configuration;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
           previousPage = (Page)mainWindow.Main.Content;
           mainWindow.Main.Content = page;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            mainWindow.Show();
        }
        
        public int GlobalCategoryId { get; set; }
        public int GlobalLevelId { get; set; }
        public int totalScore { get; set; }

        public static Page? previousPage = new Page();
    }

}
