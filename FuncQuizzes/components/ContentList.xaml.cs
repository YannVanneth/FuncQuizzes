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
                        Image = new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}\\DATA\\Assets\\{row[1]}.png")),
                        BackgroundColor = new SolidColorBrush(Colors.Transparent),
                        Cursor = Cursors.Hand
                    };

                    topicCard.MouseDown += TopicCardEvent;

                    this.ListContent.Children.Add(topicCard);
                }
                catch (Exception ex)
                {
                    this.ListContent.Children.Add(new TextBlock() { Text = $"{row[1]}" });
                }
            }
        }


        private void TopicCardEvent(object sender, MouseButtonEventArgs e)
        {
            TopicCard path = (TopicCard)sender;

            MessageBox.Show($"{System.IO.Path.GetFileName(path.Image.ToString())}");
        }
    }
}
