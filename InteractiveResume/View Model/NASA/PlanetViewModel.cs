using InteractiveResume.Model.Planets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace InteractiveResume.View_Model.NASA;

public partial class PlanetViewModel : ObservableObject
{
    [ObservableProperty]
    private Planet _planet;

    [ObservableProperty]
    private double diameter;

    [ObservableProperty]
    private string planetColor;

    [ObservableProperty]
    private double _x;
    [ObservableProperty]
    private double _y;

    [ObservableProperty]
    private double _semiMajorAxis;

    [ObservableProperty]
    private double _semiMinorAxis;

    [ObservableProperty] public string _name;

    public void UpdateCoordinates()
    {
        
    }

    public PlanetViewModel()
    {
#if DESIGN_TIME
        // Assign mock values to the properties
        // This data will be used only at design-time.
        this.planetColor = "Violet";
        this.Diameter = 50;
#endif
    }

    public PlanetViewModel(Planet planet)
    {
        this._planet = planet;
        planetColor = "Orange";
        Diameter = planet.Diameter/10;
        var point = GetStartingCoordinates();
        X = Math.Max(0, Math.Min(point.X, 800 - Diameter));
        Y = Math.Max(0, Math.Min(point.Y, 800 - Diameter));
        _semiMajorAxis = planet.OrbitalData.semimajorAxis;
        _semiMinorAxis = planet.OrbitalData.semiMinorAxis;
        Name = planet.Name;
    }

    public PathGeometry EllipsePath
    {
        get
        {
            var geometry = new PathGeometry();
            var figure = new PathFigure();
            figure.StartPoint = new Point(0, Planet.OrbitalData.semiMinorAxis / 2);  // Starting at the top of the ellipse
            figure.IsClosed = true;
            var segment = new ArcSegment();
            segment.Point = new Point(Planet.OrbitalData.semimajorAxis, Planet.OrbitalData.semiMinorAxis / 2); // Ending at the bottom of the ellipse
            segment.Size = new Size(Planet.OrbitalData.semimajorAxis / 2, Planet.OrbitalData.semiMinorAxis / 2);
            segment.IsLargeArc = true;
            segment.SweepDirection = SweepDirection.Clockwise;
            figure.Segments.Add(segment);
            geometry.Figures.Add(figure);
            return geometry;
        }
    }

    public Duration OrbitDuration => ComputeScaledOrbitDuration();

    public Duration ComputeScaledOrbitDuration()
    {
        Planet planet = _planet;
        double originalOrbitTime = _planet.OrbitalData.sideralOrbit; // Original orbit time in days
        double scaleFactor = planet.ScaleFactor; // Assuming scaleFactor is between 0 and 5000

        double scaledTime = (originalOrbitTime / scaleFactor);

        // Convert the scaled time from days to the desired unit for the animation. 
        // Here, I'm converting days to seconds for the sake of example.
        // If you want a different time unit, adjust accordingly.
        double scaledTimeInSeconds = scaledTime * 24 * 60 * 60;

        return new Duration(TimeSpan.FromSeconds(scaledTimeInSeconds));
    }

    double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }

    double CalculateDistance(double a, double e, double nu)
    {
        return (a * (1 - e * e)) / (1 + e * Math.Cos(nu));
    }

    (double, double) CalculatePosition(double r, double nu, double omega)
    {
        double x = r * Math.Cos(nu + omega);
        double y = r * Math.Sin(nu + omega);
        return (x, y);
    }

    public Point GetStartingCoordinates()
    {
        double a = _planet.OrbitalData.semimajorAxis;
        double e = _planet.OrbitalData.eccentricity;
        double nu = DegreesToRadians(_planet.OrbitalData.mainAnomaly);
        double omega = DegreesToRadians(_planet.OrbitalData.argPeriapsis);

        double r = CalculateDistance(a, e, nu);
        var (x, y) = CalculatePosition(r, nu, omega);
        return new Point(x, y);
    }

    




    // If you want to have interactions with the planet (for example, 
    // if you want a command to focus on a planet when clicked), 
    // you can add RelayCommands and associated methods here, similar to your Click method.

    // [RelayCommand(CanExecute = nameof(CanFocusOnPlanet))]
    // public void FocusOnPlanet()
    // {
    //     // Logic to focus on the planet or perform some other action.
    // }

    // private bool CanFocusOnPlanet => SomeCondition;
}