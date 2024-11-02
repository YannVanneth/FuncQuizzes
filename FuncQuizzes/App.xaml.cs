using System.Configuration;
using System.Data;
using System.Windows;

namespace FuncQuizzes
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public int GlobalCategoryId { get; set; }
        public int GlobalLevelId { get; set; }
        public int totalScore { get; set; }
    }

}
