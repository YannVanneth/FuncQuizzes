using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace FuncQuizzes.components
{
    public partial class FuncQuizzesSlider : UserControl
    {
        private List<SlideBanner> content;
        private DispatcherTimer slideTimer;
        private int currentSlideIndex = 0;

        public FuncQuizzesSlider()
        {
            InitializeComponent();
            InitializeSliderContent();
            SetupTimer();
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(FuncQuizzesSlider));

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(FuncQuizzesSlider));

        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        public static readonly DependencyProperty ImageWidthProperty =
            DependencyProperty.Register("ImageWidth", typeof(double), typeof(FuncQuizzesSlider));

        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof(double), typeof(FuncQuizzesSlider));

        private void GetStartbtn_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.Main.Content = new pages.chooselevel();
            }
        }

        private void InitializeSliderContent()
        {
            content = new List<SlideBanner>
            {
                new SlideBanner("C++ Programming", new BitmapImage(new Uri("pack://application:,,,/assets/images/CandCpp.png"))),
                new SlideBanner("Let's go! With Python", new BitmapImage(new Uri("pack://application:,,,/assets/images/Python.png"))),
                new SlideBanner("Hello, JavaScript", new BitmapImage(new Uri("pack://application:,,,/assets/images/JavaScript.png"))),
                new SlideBanner("Hah C Again!", new BitmapImage(new Uri("pack://application:,,,/assets/images/Cpp.png"))),

            };

            UpdateSlide();
        }

        private void SetupTimer()
        {
            slideTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            slideTimer.Tick += SlideTimer_Tick!;
            slideTimer.Start();
        }

        private void SlideTimer_Tick(object sender, EventArgs e)
        {
            currentSlideIndex = (currentSlideIndex + 1) % content.Count;
            UpdateSlide();
        }

        private void UpdateSlide()
        {

            this.BannerContent.Children.Clear();


            if (currentSlideIndex == 0)
            {
                this.index0.Background = new SolidColorBrush(Colors.DodgerBlue);
                this.index1.Background = new SolidColorBrush(Colors.SlateGray);
                this.index2.Background = new SolidColorBrush(Colors.SlateGray);
                this.index3.Background = new SolidColorBrush(Colors.SlateGray);
            }
            else if (currentSlideIndex == 1)
            {
                this.index0.Background = new SolidColorBrush(Colors.SlateGray);
                this.index1.Background = new SolidColorBrush(Colors.DodgerBlue);
                this.index2.Background = new SolidColorBrush(Colors.SlateGray);
                this.index3.Background = new SolidColorBrush(Colors.SlateGray);
            }

            else if (currentSlideIndex == 2)
            {
                this.index0.Background = new SolidColorBrush(Colors.SlateGray);
                this.index1.Background = new SolidColorBrush(Colors.SlateGray);
                this.index2.Background = new SolidColorBrush(Colors.DodgerBlue);
                this.index3.Background = new SolidColorBrush(Colors.SlateGray);
            }

            else if (currentSlideIndex == 3)
            {
                this.index0.Background = new SolidColorBrush(Colors.SlateGray);
                this.index1.Background = new SolidColorBrush(Colors.SlateGray);
                this.index2.Background = new SolidColorBrush(Colors.SlateGray);
                this.index3.Background = new SolidColorBrush(Colors.DodgerBlue);
            }

            var banner = content[currentSlideIndex];
            
            if (banner != null)
            {
                this.BannerContent.Children.Add(banner);
            }
        }
    }
}
