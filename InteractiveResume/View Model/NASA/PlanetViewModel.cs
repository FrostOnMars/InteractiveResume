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

    public PlanetViewModel(Planet planet)
    {
        this._planet = planet;
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