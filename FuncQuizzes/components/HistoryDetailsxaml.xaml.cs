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
    /// Interaction logic for HistoryDetailsxaml.xaml
    /// </summary>
    public partial class HistoryDetailsxaml : UserControl
    {
        public HistoryDetailsxaml()
        {
            InitializeComponent();
        }
        // Property to set the question text
        public string QuestionText
        {
            get => questionname.Text; // Assuming `questionname` is the TextBlock for the question
            set => questionname.Text = value;
        }

        // Property to set the answer text
        public string AnswerText
        {
            get => textanswer.Text; // Assuming `textanswer` is the TextBlock for the answer
            set => textanswer.Text = value;
        }

        // Property to set correctness text and apply color based on correctness
        public string IsCorrectText
        {
            get => iscorrectanswer.Text; // Assuming `iscorrectanswer` is the TextBlock for correctness
            set
            {
                iscorrectanswer.Text = value;
                // Set color based on correctness value
                iscorrectanswer.Foreground = value == "ត្រឹមត្រូវ" ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Red);
            }
        }
    }
}
