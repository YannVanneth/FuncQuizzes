using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace FuncQuizzes.pages
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Page
    {
        private DispatcherTimer _timer;
        private int _progress;
        private TextBlock _text;

        public SplashScreen()
        {
            InitializeComponent();
            Loading(ref _text);
        }

        private void Loading(ref TextBlock textblock)
        {
            //_timer = new DispatcherTimer();
            //_timer.Interval = TimeSpan.FromMilliseconds(300);
            //_timer.Start();
            StartLoading();

            this.bottomStack.Children.Clear();

            var stackpanel = new StackPanel() 
            {
                Orientation = Orientation.Horizontal,
            };

            textblock = new TextBlock()
            {
                Text = "កំពុងដំណើរការ",
                Margin = new Thickness(0, 10, 0, 10),
                FontSize = 12,
                HorizontalAlignment = HorizontalAlignment.Left,
            };

            if (FindResource("Kantumruy_Pro") is FontFamily kantumruyFont)
            {
                textblock.FontFamily = kantumruyFont;
            }

            stackpanel.Children.Add(textblock);
            this.bottomStack.Children.Add(stackpanel);

            var myprogress = new ProgressBar
            {
                Name = "progressValue",
                Value = 0,
                Height = 10,
                Background = new SolidColorBrush(Colors.Gray),
                Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF29ADE3")),
            };

            this.bottomStack.Children.Add(myprogress);
            AnimateProgressBar(myprogress);
        }

        private void AnimateProgressBar(ProgressBar progressBar)
        {
            var animation = new DoubleAnimation
            {
                From = 0,
                To = 100,
                Duration = TimeSpan.FromSeconds(5),
                AutoReverse = false,
            };

            Storyboard.SetTarget(animation, progressBar);
            Storyboard.SetTargetProperty(animation, new PropertyPath(ProgressBar.ValueProperty));

            var storyboard = new Storyboard();
            storyboard.Children.Add(animation);
            storyboard.Begin();
        }

        private void StartLoading()
        {
            _progress = 0;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(33.1);
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            UpdateProgress();
        }

        private void UpdateProgress()
        {

            var progressBar = this.bottomStack.Children.OfType<ProgressBar>().FirstOrDefault();

            if (progressBar != null && _progress < 100)
            {
                _progress += 1; 
                _text.Text = $"កំពុងដំណើរការ {_progress.ToString()}%";
                progressBar.Value = _progress; 
            }
            else
            {
                _timer.Stop();
                App.SwitchPage(new pages.HomeScreen());
            }
        }
    }
}
