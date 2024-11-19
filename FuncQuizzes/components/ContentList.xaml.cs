using System;
using System.Collections.Generic;
using System.Data;
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
using System.IO;
using Path = System.IO.Path;
using System.Data.SQLite;

namespace FuncQuizzes.components
{
    /// <summary>
    /// Interaction logic for ContentList.xaml
    /// </summary>
    public partial class ContentList : UserControl
    {
        public ContentList()
        {
            InitializeComponent();
            LoadContent();
        }

        private void InnerScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;

            if (scrollViewer != null)
            {
                if (e.Delta < 0) scrollViewer.LineRight();
                else scrollViewer.LineLeft();

                e.Handled = true;
            }
        }

        private void TopicCard_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Clicked");
        }

        private void LoadContent()
        {
            features.Database database = new features.Database("DATA\\FuncQuizzes.sqlite");
            DataTable data = database.SelectFromTable("tbl_category");

            this.ListContent.Children.Clear();

            foreach (DataRow row in data.Rows)
            {
                try
                {
                    TopicCard topicCard = new TopicCard
                    {
                        Image = new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}\\DATA\\Assets\\{row[2]}.png")),
                        BackgroundColor = new SolidColorBrush(Colors.Transparent),
                        Cursor = Cursors.Hand,
                        Size = 100,
                        Raduis = 17
                    };

                    topicCard.MouseDown += TopicCardEvent;

                    this.ListContent.Children.Add(topicCard);
                }
                catch (Exception ex)
                {
                    this.ListContent.Children.Add(new TextBlock() { Text = $"{row[2]}" });
                }
            }
        }
        public void TopicCardEvent(object sender, MouseButtonEventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data source=DATA\\FuncQuizzes.sqlite"))
            {
                con.Open();
                string questionQuery = "SELECT id_category FROM tbl_category WHERE category_image = @category_image";
                TopicCard path = sender as TopicCard;

                if (path != null)
                {
                    string categoryName = Path.GetFileNameWithoutExtension(path.Image.ToString());

                    SQLiteCommand cmd = new SQLiteCommand(questionQuery, con);
                    cmd.Parameters.AddWithValue("@category_image", categoryName);

                    object result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int categoryId))
                    {
                        ((App)Application.Current).GlobalCategoryId = categoryId;
                        new pages.selectcategory().loadpage();
                    }
                    else
                    {
                        MessageBox.Show("Category not found.");
                    }
                }
            }
        }
    }
}
