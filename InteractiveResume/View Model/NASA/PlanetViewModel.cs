using InteractiveResume.Model.Planets;
using System;
using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using InteractiveResume.Model;

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



    public Duration OrbitDuration => ComputeScaledOrbitDuration();
    public static void UpdateCoordinates()
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


    public PathGeometry FullEllipsePath
    {
        get
        {
            var ellipse = new EllipseGeometry
            {
                Center = new Point(0, 0),
                RadiusX = Planet.OrbitalData.semimajorAxis,
                RadiusY = Planet.OrbitalData.semiMinorAxis
            };

            var figure = new PathFigure
            {
                StartPoint = new Point(Planet.OrbitalData.semimajorAxis, 0),
                IsClosed = true
            };

            figure.Segments.Add(new ArcSegment
            {
                Point = new Point(-Planet.OrbitalData.semimajorAxis, 0),
                Size = new Size(Planet.OrbitalData.semimajorAxis, Planet.OrbitalData.semiMinorAxis),
                IsLargeArc = true,
                SweepDirection = SweepDirection.Clockwise
            });

            figure.Segments.Add(new ArcSegment
            {
                Point = new Point(Planet.OrbitalData.semimajorAxis, 0),
                Size = new Size(Planet.OrbitalData.semimajorAxis, Planet.OrbitalData.semiMinorAxis),
                IsLargeArc = true,
                SweepDirection = SweepDirection.Clockwise
            });

            return new PathGeometry
            {
                Figures =
                {
                    figure
                }
            };
        }
    }

    public Duration ComputeScaledOrbitDuration()
    {
        if (Planet.OrbitalData == null) return new Duration();
        var planet = Planet;
        var originalOrbitTime = planet.OrbitalData.sideralOrbit; // Original orbit time in days
        double scaleFactor = planet.ScaleFactor + 3000000; // Assuming scaleFactor is between 0 and 5000
        //TODO: REMOVE ADDITION TO SCALE FACTOR. TESTING THIS ONLY.
        var scaledTime = (originalOrbitTime / scaleFactor);

        var scaledTimeInSeconds = scaledTime * 24 * 60 * 60;

        return new Duration(TimeSpan.FromSeconds(scaledTimeInSeconds));
    }

    /// <summary>
    /// Converts an angle from degrees to radians.
    /// </summary>
    /// <param name="degrees">Angle in degrees.</param>
    /// <returns>Angle in radians.</returns>
    private static double DegreesToRadians(double degrees)
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
    private static double CalculateDistance(double a, double e, double nu)
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
    private static (double, double) CalculatePosition(double r, double nu, double omega)
    {
        var x = r * Math.Cos(nu + omega);
        var y = r * Math.Sin(nu + omega);
        return (x, y);
    }

    /// <summary>
    /// Gets the starting coordinates for a planet based on its orbital elements.
    /// </summary>
    /// <returns>The starting coordinates as a Point.</returns>
    public Point GetStartingCoordinates()
    {
        // Extracting the orbital elements from the planet's data
        var a = _planet.OrbitalData.semimajorAxis; // Semi-major axis
        var e = _planet.OrbitalData.eccentricity;  // Eccentricity of the orbit
        var nu = DegreesToRadians(_planet.OrbitalData.mainAnomaly); // True anomaly in radians
        var omega = DegreesToRadians(_planet.OrbitalData.argPeriapsis); // Argument of periapsis in radians

        // Calculate the distance from the central body at the current true anomaly
        var r = CalculateDistance(a, e, nu);

        // Convert the polar coordinates to Cartesian coordinates
        var (x, y) = CalculatePosition(r, nu, omega);

        return new Point(x + 400, y+400);
    }

}