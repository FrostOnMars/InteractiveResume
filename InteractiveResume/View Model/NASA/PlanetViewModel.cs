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
        this.planetColor = "Orange";
        this.Diameter = 50;
#endif
    }

    public PlanetViewModel(Planet planet)
    {
        this._planet = planet;
        planetColor = "Orange";
        Diameter = planet.Diameter;
        var point = GetStartingCoordinates();
        X = point.X;
        Y = point.Y;
        _semiMajorAxis = planet.OrbitalData.semimajorAxis;
        _semiMinorAxis = planet.OrbitalData.semiMinorAxis;
        Name = planet.Name;
    }

    public PathGeometry EllipsePath
    {
        get
        {
            var geometry = new PathGeometry();
            var figure = new PathFigure
            {
                StartPoint = new Point(0, Planet.OrbitalData.semiMinorAxis / 2),  // Starting at the top of the ellipse
                IsClosed = true
            };
            var segment = new ArcSegment
            {
                Point = new Point(Planet.OrbitalData.semimajorAxis, Planet.OrbitalData.semiMinorAxis / 2), // Ending at the bottom of the ellipse
                Size = new Size(Planet.OrbitalData.semimajorAxis / 2, Planet.OrbitalData.semiMinorAxis / 2),
                IsLargeArc = true,
                SweepDirection = SweepDirection.Clockwise
            };
            figure.Segments.Add(segment);
            geometry.Figures.Add(figure);
            var testx = XEllipsePath;
            var testy = YEllipsePath;
            return geometry;
        }
    }

    public PathGeometry XEllipsePath
    {
        get
        {
            var geometry = new PathGeometry();
            var figure = new PathFigure
            {
                StartPoint = new Point(_planet.OrbitalData.semimajorAxis, 0), // half the width of the ellipse, starting from the rightmost point
                IsClosed = true
            };

            var segment = new ArcSegment
            {
                Point = new Point(-_planet.OrbitalData.semimajorAxis, 0), // half the width of the ellipse, moving to the leftmost point

                Size = new Size(_planet.OrbitalData.semimajorAxis, _planet.OrbitalData.semiMinorAxis),
                IsLargeArc = true,
                SweepDirection = SweepDirection.Clockwise
            };

            figure.Segments.Add(segment);
            geometry.Figures.Add(figure);
            return geometry;
        }
    }

    public PathGeometry YEllipsePath
    {
        get
        {
            var geometry = new PathGeometry();
            var figure = new PathFigure
            {
                StartPoint = new Point(0, _planet.OrbitalData.semiMinorAxis), // half the height of the ellipse, starting from the bottommost point
                IsClosed = true
            };

            var segment = new ArcSegment
            {
                Point = new Point(0, -_planet.OrbitalData.semiMinorAxis), // half the height of the ellipse, moving to the topmost point

                Size = new Size(_planet.OrbitalData.semimajorAxis, _planet.OrbitalData.semiMinorAxis),
                IsLargeArc = true,
                SweepDirection = SweepDirection.Clockwise
            };

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
        double scaleFactor = planet.ScaleFactor + 3000000; // Assuming scaleFactor is between 0 and 5000
        //TODO: REMOVE ADDITION TO SCALE FACTOR. TESTING THIS ONLY.
        double scaledTime = (originalOrbitTime / scaleFactor);

        // Convert the scaled time from days to the desired unit for the animation. 
        // Here, I'm converting days to seconds for the sake of example.
        // If you want a different time unit, adjust accordingly.
        double scaledTimeInSeconds = scaledTime * 24 * 60 * 60;

        return new Duration(TimeSpan.FromSeconds(scaledTimeInSeconds));
    }

    /// <summary>
    /// Converts an angle from degrees to radians.
    /// </summary>
    /// <param name="degrees">Angle in degrees.</param>
    /// <returns>Angle in radians.</returns>
    double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }

    /// <summary>
    /// Calculates the distance from the central body at a given true anomaly.
    /// Uses the formula: r = (a * (1 - e^2)) / (1 + e * cos(ν)) [Polar Form Equation for an Ellipse]
    /// </summary>
    /// <param name="a">Semi-major axis.</param>
    /// <param name="e">Eccentricity of the orbit.</param>
    /// <param name="nu">True anomaly (angle between the direction of periapsis and the current position).</param>
    /// <returns>Distance from the central body.</returns>
    double CalculateDistance(double a, double e, double nu)
    {
        return (a * (1 - e * e)) / (1 + e * Math.Cos(nu));
    }

    /// <summary>
    /// Calculates the position (x, y) in a 2D plane based on polar coordinates.
    /// </summary>
    /// <param name="r">Distance from central body.</param>
    /// <param name="nu">True anomaly.</param>
    /// <param name="omega">Argument of periapsis (angle between the reference direction and the periapsis).</param>
    /// <returns>Position (x, y) in a 2D plane.</returns>
    (double, double) CalculatePosition(double r, double nu, double omega)
    {
        double x = r * Math.Cos(nu + omega);
        double y = r * Math.Sin(nu + omega);
        return (x, y);
    }

    /// <summary>
    /// Gets the starting coordinates for a planet based on its orbital elements.
    /// </summary>
    /// <returns>The starting coordinates as a Point.</returns>
    public Point GetStartingCoordinates()
    {
        // Extracting the orbital elements from the planet's data
        double a = _planet.OrbitalData.semimajorAxis; // Semi-major axis
        double e = _planet.OrbitalData.eccentricity;  // Eccentricity of the orbit
        double nu = DegreesToRadians(_planet.OrbitalData.mainAnomaly); // True anomaly in radians
        double omega = DegreesToRadians(_planet.OrbitalData.argPeriapsis); // Argument of periapsis in radians

        // Calculate the distance from the central body at the current true anomaly
        double r = CalculateDistance(a, e, nu);

        // Convert the polar coordinates to Cartesian coordinates
        var (x, y) = CalculatePosition(r, nu, omega);

        return new Point(x + 400, y+400);
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