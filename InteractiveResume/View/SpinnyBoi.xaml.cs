using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace InteractiveResume.View;

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

    public Brush BackgroundBrush
    {
        get => (Brush)GetValue(BackgroundBrushProperty);
        set => SetValue(BackgroundBrushProperty, value);
    }

    public static readonly DependencyProperty BackgroundBrushProperty = DependencyProperty.Register(nameof(BackgroundBrush),
        typeof(Brush), typeof(SpinnyBoi), new PropertyMetadata(Brushes.White));

    public Brush SpinnerBrush
    {
        get => (Brush)GetValue(SpinnerBrushProperty);
        set => SetValue(SpinnerBrushProperty, value);
    }

    public static readonly DependencyProperty SpinnerBrushProperty = DependencyProperty.Register(nameof(SpinnerBrush),
        typeof(Brush), typeof(SpinnyBoi), new PropertyMetadata(Brushes.DodgerBlue));
    //FFD9DADE
}