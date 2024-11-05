using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Path = System.IO.Path;

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
            App.SwitchPage(new pages.selectcategory());
        }

        private void InitializeSliderContent()
        {
            var slide01 = new SlideBanner(new BitmapImage(new Uri($"{System.IO.Directory.GetCurrentDirectory()}\\DATA\\Assets\\BannerA.png")));
            slide01.MouseDown += SliderClick;
            slide01.Cursor = Cursors.Hand;

            var slide02 = new SlideBanner(new BitmapImage(new Uri($"{System.IO.Directory.GetCurrentDirectory()}\\DATA\\Assets\\BannerB.png")));
            slide02.MouseDown += SliderClick;
            slide02.Cursor = Cursors.Hand;

            var slide03 = new SlideBanner(new BitmapImage(new Uri($"{System.IO.Directory.GetCurrentDirectory()}\\DATA\\Assets\\BannerC.png")));
            slide03.MouseDown += SliderClick;
            slide03.Cursor = Cursors.Hand;

            var slide04 = new SlideBanner(new BitmapImage(new Uri($"{System.IO.Directory.GetCurrentDirectory()}\\DATA\\Assets\\BannerD.png")));
            slide04.MouseDown += SliderClick;
            slide04.Cursor = Cursors.Hand;

            content = new List<SlideBanner>
            {
                slide01, slide02, slide03, slide04
            };

            UpdateSlide();
        }

        public void SliderClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var path = (SlideBanner)sender;

            if (path != null)
            {
                if (Path.GetFileNameWithoutExtension(path.ContentImage.ToString()) == "BannerC")
                {
                    ((App)Application.Current).GlobalCategoryId = 1;
                    new pages.selectcategory().loadpage();
                }
                else if (Path.GetFileNameWithoutExtension(path.ContentImage.ToString()) == "BannerA")
                {
                    ((App)Application.Current).GlobalCategoryId = 2;
                    new pages.selectcategory().loadpage();
                }
                else if (Path.GetFileNameWithoutExtension(path.ContentImage.ToString()) == "BannerB")
                {
                    ((App)Application.Current).GlobalCategoryId = 3;
                    new pages.selectcategory().loadpage();
                }
                else if (Path.GetFileNameWithoutExtension(path.ContentImage.ToString()) == "BannerD")
                {
                    ((App)Application.Current).GlobalCategoryId = 4;
                    new pages.selectcategory().loadpage();
                }
            }
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

            this.BannerContent.Child = null;


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
            banner.HorizontalAlignment = HorizontalAlignment.Stretch;
            banner.VerticalAlignment = VerticalAlignment.Stretch;
            banner.Height = 218;
            banner.Width = 768;

            if (banner != null)
            {
                this.BannerContent.Child = banner;
            }
        }

        private void Dot_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Border dot = sender as Border;
            if (dot != null)
            {
                // Apply active animation
                ColorAnimation activeAnimation = (ColorAnimation)FindResource("DotActiveAnimation");
                dot.Background.BeginAnimation(SolidColorBrush.ColorProperty, activeAnimation);
            }
        }

        private void Dot_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Border dot = sender as Border;
            if (dot != null)
            {
                // Apply inactive animation
                ColorAnimation inactiveAnimation = (ColorAnimation)FindResource("DotInactiveAnimation");
                dot.Background.BeginAnimation(SolidColorBrush.ColorProperty, inactiveAnimation);
            }
        }
    }
}
