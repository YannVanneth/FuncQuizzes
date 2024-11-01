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
    }
}
