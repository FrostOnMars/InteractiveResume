using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using InteractiveResume.Model.Planets;

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

    public PathGeometry EllipsePath
    {
        get
        {
            var geometry = new PathGeometry();
            var figure = new PathFigure();
            figure.StartPoint = new Point(0, _planetViewModel.Planet.OrbitalData.semiMinorAxis / 2);  // Starting at the top of the ellipse
            figure.IsClosed = true;
            var segment = new ArcSegment();
            segment.Point = new Point(_planetViewModel.Planet.OrbitalData.semimajorAxis, _planetViewModel.Planet.OrbitalData.semiMinorAxis / 2); // Ending at the bottom of the ellipse
            segment.Size = new Size(_planetViewModel.Planet.OrbitalData.semimajorAxis / 2, _planetViewModel.Planet.OrbitalData.semiMinorAxis / 2);
            segment.IsLargeArc = true;
            segment.SweepDirection = SweepDirection.Clockwise;
            figure.Segments.Add(segment);
            geometry.Figures.Add(figure);
            return geometry;
        }
    }
}