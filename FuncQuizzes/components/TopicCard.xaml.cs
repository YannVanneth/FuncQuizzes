﻿using System;
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
    /// Interaction logic for TopicCard.xaml
    /// </summary>
    public partial class TopicCard : UserControl
    {
        public TopicCard()
        {
            InitializeComponent();
        }

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource), typeof(TopicCard));

        public SolidColorBrush BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.Register("Background", typeof(SolidColorBrush), typeof(TopicCard));

        public double Size
        {
            get { return (double)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }

        public static readonly DependencyProperty HeightProperty = DependencyProperty.Register("Size", typeof(double), typeof(TopicCard));

        public double Raduis
        {
            get { return (double)GetValue(RaduisProperty); }
            set { SetValue(RaduisProperty, value); }
        }

        public static readonly DependencyProperty RaduisProperty = DependencyProperty.Register("Raduis", typeof(double), typeof(TopicCard));
    }
}
