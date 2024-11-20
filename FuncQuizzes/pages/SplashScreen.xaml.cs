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
        private Storyboard _storyboard;

        public SplashScreen()
        {
            InitializeComponent();
            InitializeLoadingScreen();
        }

        private void InitializeLoadingScreen()
        {
            bottomStack.Children.Clear();

            var textBlock = CreateLoadingTextBlock();
            bottomStack.Children.Add(CreateStackPanelWithChild(textBlock));

            var progressBar = CreateProgressBar();
            bottomStack.Children.Add(progressBar);

            StartLoading(textBlock, progressBar);
        }

        private TextBlock CreateLoadingTextBlock()
        {
            var textBlock = new TextBlock
            {
                Text = "កំពុងដំណើរការ 0%",
                Margin = new Thickness(0, 10, 0, 10),
                FontSize = 12,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            if (TryFindResource("Kantumruy_Pro") is FontFamily customFont)
            {
                textBlock.FontFamily = customFont;
            }

            return textBlock;
        }

        private ProgressBar CreateProgressBar()
        {
            var progressBar = new ProgressBar
            {
                Value = 0,
                Height = 10,
                Background = new SolidColorBrush(Colors.Gray),
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF29ADE3"))
            };

            progressBar.Name = "progressBar";
            RegisterName(progressBar.Name, progressBar);

            return progressBar;
        }

        private StackPanel CreateStackPanelWithChild(UIElement child)
        {
            var stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };
            stackPanel.Children.Add(child);
            return stackPanel;
        }

        private void StartLoading(TextBlock textBlock, ProgressBar progressBar)
        {
            var animation = new DoubleAnimation
            {
                From = 0,
                To = 100,
                Duration = TimeSpan.FromSeconds(5),
                AutoReverse = false
            };

            _storyboard = new Storyboard();
            Storyboard.SetTargetName(animation, progressBar.Name);
            Storyboard.SetTargetProperty(animation, new PropertyPath(ProgressBar.ValueProperty));
            _storyboard.Children.Add(animation);

            _storyboard.CurrentTimeInvalidated += (s, e) =>
            {
                var clock = _storyboard.GetCurrentTime(this);
                if (clock.HasValue)
                {
                    double progress = clock.Value.TotalMilliseconds / animation.Duration.TimeSpan.TotalMilliseconds;
                    int percentage = (int)(progress * 100);
                    textBlock.Text = $"កំពុងដំណើរការ {percentage}%";
                    progressBar.Value = percentage;
                }
            };

            _storyboard.Completed += (s, e) => NavigateToHomeScreen();

            _storyboard.Begin(this, true);
        }

        private void NavigateToHomeScreen()
        {
            NavigationService?.Navigate(new HomeScreen());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard SponserTextAnimation = (Storyboard)FindResource("SponserTextAnimation");
            Storyboard LRGrid01Animation = (Storyboard)FindResource("LRGrid01Animation");
            Storyboard RLGrid02Animation = (Storyboard)FindResource("RLGrid02Animation");
            Storyboard FadeIn = (Storyboard)FindResource("FadeIn");
            Storyboard TBAnimation = (Storyboard)FindResource("TBAnimation");

            Storyboard.SetTarget(SponserTextAnimation, SponserText);
            Storyboard.SetTarget(LRGrid01Animation, Grid01_logo);
            Storyboard.SetTarget(RLGrid02Animation, this.ANT_LOGO);
            Storyboard.SetTarget(FadeIn, this.Image01);

            FadeIn.Begin();
            Storyboard.SetTarget(FadeIn, this.Image02);
            FadeIn.Begin();
            Storyboard.SetTarget(FadeIn, this.Image03);
            FadeIn.Begin();
            Storyboard.SetTarget(TBAnimation, this.HeadText_02);

            TBAnimation.Begin();
            LRGrid01Animation.Begin();
            SponserTextAnimation.Begin();
            RLGrid02Animation.Begin();
        }
    }
}
