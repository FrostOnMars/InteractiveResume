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

namespace InteractiveResume.View
{
    /// <summary>
    /// Interaction logic for SpinnyBoi.xaml
    /// </summary>
    public partial class SpinnyBoi : UserControl
    {
        public SpinnyBoi()
        {
            InitializeComponent();
        }

        public Duration Duration
        {
            get => (Duration)GetValue(DurationProperty);
            set => SetValue(DurationProperty, value);
        }

        public static readonly DependencyProperty DurationProperty = DependencyProperty.Register(nameof(Duration),
            typeof(Duration), typeof(SpinnyBoi), new PropertyMetadata(default(Duration)));

        public Brush Brush
        {
            get => (Brush)GetValue(BrushProperty);
            set => SetValue(BrushProperty, value);
        }

        public static readonly DependencyProperty BrushProperty = DependencyProperty.Register(nameof(Brush),
            typeof(Duration), typeof(SpinnyBoi), new PropertyMetadata(default(Duration)));
    }
}
