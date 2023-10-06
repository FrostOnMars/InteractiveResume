using System;
using System.Linq;

namespace InteractiveResume.View_Model.NASA;

public class TransformData : OrbitalData
{
    //this is copied from OrbitalData, and it needs to be refactored
    public delegate void ErrorHandler(string errorMessage);
    public static event ErrorHandler? ErrorOccurred;
    public static void ToUsableSizes(double screenWidth, double screenHeight)
    {
        try
        {
            foreach (var planet in BigBang.Instance.Planets.OrderByDescending(p => p.OrbitalData.semimajorAxis))
            {
                // This is the time scale factor
                planet.ScaleFactor = 40000;
                if (planet.OrbitalData == null) continue;

                // Modify the semi major and semi minor axes of the ellipse
                planet.OrbitalData.semimajorAxis = planet.OrbitalData.semimajorAxis;
                planet.OrbitalData.semiMinorAxis = CalculateSemiMinorAxis(planet.OrbitalData.semimajorAxis, planet.OrbitalData.eccentricity);
            }

            ScaleOrbitsWithScreenDimensions(BigBang.Instance.Planets, screenWidth, screenHeight);
            ScalePlanetDiameters(BigBang.Instance.Planets);
        }
        catch (Exception ex)
        {
            // Raise the event when an error occurs.
            ErrorOccurred?.Invoke(ex.Message);
        }
    }
}