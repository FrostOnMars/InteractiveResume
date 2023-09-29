using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace InteractiveResume.View;

/// <summary>
/// Interaction logic for PlanetControl.xaml
/// </summary>
[ObservableObject]
public partial class PlanetControl : UserControl
{
    [ObservableProperty]
    private double _diameter;
    public PlanetControl()
    {
        InitializeComponent();
        //Diameter = Ellipse.Height;
    }
}