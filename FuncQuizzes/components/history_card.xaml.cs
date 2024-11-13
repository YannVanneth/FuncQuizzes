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
    /// Interaction logic for history_card.xaml
    /// </summary>
    public partial class history_card : UserControl
    {
        public history_card()
        {
            InitializeComponent();
        }

        public ImageSource ImageContent
        {
            get { return (ImageSource)GetValue(ImageContentProperty); }
            set { SetValue(ImageContentProperty, value); }
        }

        public static readonly DependencyProperty ImageContentProperty =
            DependencyProperty.Register("ImageContent", typeof(ImageSource), typeof(history_card));

        public SolidColorBrush TextColor
        {
            get { return (SolidColorBrush)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register("TextColor", typeof(SolidColorBrush), typeof(history_card));
        public SolidColorBrush CardColor
        {
            get { return (SolidColorBrush)GetValue(CardColorProperty); }
            set { SetValue(CardColorProperty, value); }
        }

        public static readonly DependencyProperty CardColorProperty =
            DependencyProperty.Register("CardColor", typeof(SolidColorBrush), typeof(history_card));

        public SolidColorBrush LevelColor
        {
            get { return (SolidColorBrush)GetValue(LevelColorProperty); }
            set { SetValue(LevelColorProperty, value); }
        }

        public static readonly DependencyProperty LevelColorProperty =
            DependencyProperty.Register("LevelColor", typeof(SolidColorBrush), typeof(history_card));

        public string Level
        {
            get { return (string)GetValue(LevelProperty); }
            set { SetValue(LevelProperty, value); }
        }

        //public string Level
        //{
        //    get { return (string)GetValue(LevelProperty); }
        //    set { SetValue(LevelProperty, value); }
        //}

        public static readonly DependencyProperty LevelProperty =
            DependencyProperty.Register("Level", typeof(string), typeof(history_card));

        public string CorrectQuestNumber
        {
            get { return (string)GetValue(CorrectQuestNumberProperty); }
            set { SetValue(CorrectQuestNumberProperty, value); }
        }

        public static readonly DependencyProperty CorrectQuestNumberProperty =
            DependencyProperty.Register("CorrectQuestNumber", typeof(string), typeof(history_card));

        public string IncorrectQuestNumber
        {
            get { return (string)GetValue(IncorrectQuestNumberProperty); }
            set { SetValue(IncorrectQuestNumberProperty, value); }
        }

        public static readonly DependencyProperty IncorrectQuestNumberProperty =
            DependencyProperty.Register("IncorrectQuestNumber", typeof(string), typeof(history_card));

        public string QuestDate
        {
            get { return (string)GetValue(QuestDateProperty); }
            set { SetValue(QuestDateProperty, value); }
        }

        public static readonly DependencyProperty QuestDateProperty =
            DependencyProperty.Register("QuestDate", typeof(string), typeof(history_card));

        public string QuestDuration
        {
            get { return (string)GetValue(QuestDurationProperty); }
            set { SetValue(QuestDurationProperty, value); }
        }

        public static readonly DependencyProperty QuestDurationProperty =
            DependencyProperty.Register("QuestDuration", typeof(string), typeof(history_card));

        public string TotalScore
        {
            get { return (string)GetValue(TotalScoreProperty); }
            set { SetValue(TotalScoreProperty, value); }
        }

        public static readonly DependencyProperty TotalScoreProperty =
            DependencyProperty.Register("TotalScore", typeof(string), typeof(history_card));

        public ImageSource Grade
        {
            get { return (ImageSource)GetValue(GradeProperty); }
            set { SetValue(GradeProperty, value); }
        }

        public static readonly DependencyProperty GradeProperty =
            DependencyProperty.Register("Grade", typeof(ImageSource), typeof(history_card));
    }
}
