using CommunityToolkit.Mvvm.ComponentModel;

public partial class PlanetControlViewModel : ObservableObject
{
    [ObservableProperty]
    private double diameter;

    [ObservableProperty]
    private string planetColor;  // Use a string to represent the color

    // Constructor
    public PlanetControlViewModel(double diameter, string planetColor)
    {
        this.Diameter = diameter;
        this.PlanetColor = planetColor;
    }
}