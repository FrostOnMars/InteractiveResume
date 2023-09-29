using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace InteractiveResume.View_Model.NASA;

public partial class OrbitalPathControlViewModel : ObservableObject
{
    [ObservableProperty]
    private PlanetViewModel _planetViewModel;
    [ObservableProperty]
    private double _x;
    [ObservableProperty]
    private double _y;

    [ObservableProperty]
    private double _semiMajorAxis;

    [ObservableProperty]
    private double _semiMinorAxis;

    public OrbitalPathControlViewModel(PlanetViewModel planetViewModel)
    {
        _planetViewModel = planetViewModel;
        X = planetViewModel.X;
        Y = planetViewModel.Y;
        _semiMajorAxis = planetViewModel.SemiMajorAxis;
        _semiMinorAxis = planetViewModel.SemiMinorAxis;
    }

    public OrbitalPathControlViewModel()
    {
        _planetViewModel = new PlanetViewModel
        {
            X = 50,
            Y = 50,
            SemiMajorAxis = 200,
            SemiMinorAxis = 150
        };
        X = 50;
        Y = 50;
        _semiMajorAxis = _planetViewModel.SemiMajorAxis;
        _semiMinorAxis = _planetViewModel.SemiMinorAxis;
    }

    public PathGeometry EllipsePath
    {
        get
        {
            var geometry = new PathGeometry();
            var figure = new PathFigure
            {
                StartPoint = new Point(0, _planetViewModel.Planet.OrbitalData.semiMinorAxis / 2),  // Starting at the top of the ellipse
                IsClosed = true
            };
            var segment = new ArcSegment
            {
                Point = new Point(_planetViewModel.Planet.OrbitalData.semimajorAxis, _planetViewModel.Planet.OrbitalData.semiMinorAxis / 2), // Ending at the bottom of the ellipse
                Size = new Size(_planetViewModel.Planet.OrbitalData.semimajorAxis / 2, _planetViewModel.Planet.OrbitalData.semiMinorAxis / 2),
                IsLargeArc = true,
                SweepDirection = SweepDirection.Clockwise
            };
            figure.Segments.Add(segment);
            geometry.Figures.Add(figure);
            return geometry;
        }
    }
}