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
    /// Interaction logic for SlideBanner.xaml
    /// </summary>
    public partial class SlideBanner : UserControl
    {
        public SlideBanner(string text, ImageSource image)
        {
            InitializeComponent();
            this.Text = text;
            this.ContentImage = image;
        }
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(SlideBanner));

        public ImageSource ContentImage
        {
            get { return (ImageSource)GetValue(ContentImageProperty); }
            set { SetValue(ContentImageProperty, value); }
        }

        public static readonly DependencyProperty ContentImageProperty =
            DependencyProperty.Register("ContentImage", typeof(ImageSource), typeof(SlideBanner));
    }
}
