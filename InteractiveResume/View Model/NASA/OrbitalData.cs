using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using doubleeractiveResume.Model.Planets;
using InteractiveResume.Model;
using InteractiveResume.Model.Planets;
using Newtonsoft.Json;

namespace InteractiveResume.View_Model.NASA;

public class OrbitalData
{
    private BigBang _instance = BigBang.Instance;
    private DataSources _dataSources = new DataSources();
    private PlanetaryData _planetaryData { get; set; }

    public void GetData()
    {
        //var taskList = _instance.Planets.Select(planet => GetPlanetDataAsync(planet.Name)).ToList();
        //var jObjectsResults = Task.WhenAll(taskList).Result.ToList();
        foreach (var planet in _instance.Planets)
        {
            var jsonString = GetPlanetData(planet.Name);
            //try catch here eventually
            if (string.IsNullOrEmpty(jsonString)) continue;
            planet.OrbitalData = JsonConvert.DeserializeObject<OrbitalDataModel>(jsonString);
        }

        double scaleFactor = 0;
        
        foreach (var planet in _instance.Planets.OrderByDescending(p => p.OrbitalData.semimajorAxis))
        {
            planet.ScaleFactor = 40000;
            if (planet.OrbitalData == null) continue;
            //another try catch

            // Use local variables to hold the values
            var semiMajor = planet.OrbitalData.semimajorAxis;
            var semiMinor = planet.OrbitalData.semiMinorAxis;
            var velocity = planet.PlanetaryVelocity;

            planet.PlanetaryVelocity = CalculateRealOrbitalVelocity(planet.OrbitalData.semimajorAxis,
                planet.OrbitalData.semiMinorAxis, planet.OrbitalData.sideralOrbit);
            planet.EllipseCircumference = ResizeEllipseToFitScreen(ref semiMajor, ref semiMinor);
            planet.EllipseCircumference = FindScaleFactorAndResize(ref semiMajor, ref semiMinor, ref scaleFactor);

            //TODO: "2,000" should be replaced with a value from a slider that allows the user to choose between 0 - 5000 for simulation speed.
            velocity = ComputeSimulationVelocity(velocity, planet.ScaleFactor);

            // Assign the modified values back to the properties
            planet.OrbitalData.semimajorAxis = semiMajor;
            planet.OrbitalData.semiMinorAxis = semiMinor;
            planet.PlanetaryVelocity = velocity;
        }

        ScalePlanetDiameters(_instance.Planets);

    }

    /// <summary>
    /// Calculates the average orbital velocity of a planet with an elliptical orbit.
    /// </summary>
    /// <param name="semiMajorAxisKm">Semi-major axis of the planet's orbit in kilometers.</param>
    /// <param name="semiMinorAxisKm">Semi-minor axis of the planet's orbit in kilometers.</param>
    /// <param name="siderealOrbitDays">Sidereal orbital period of the planet in days.</param>
    /// <returns>The average orbital velocity in km/h.</returns>
    public double CalculateRealOrbitalVelocity(double semiMajorAxisKm, double semiMinorAxisKm, double siderealOrbitDays)
    {
        // Convert sidereal orbit from days to seconds
        var siderealOrbitSeconds = siderealOrbitDays * 24 * 60 * 60;

        // Calculate the approximate circumference of the real elliptical orbit using Ramanujan's formula
        var orbitCircumferenceKm = Math.PI * (3 * (semiMajorAxisKm + semiMinorAxisKm) -
                                              Math.Sqrt((3 * semiMajorAxisKm + semiMinorAxisKm) * (semiMajorAxisKm + 3 * semiMinorAxisKm)));

        // Calculate and return the real average orbital velocity
        return orbitCircumferenceKm / siderealOrbitSeconds * 3600;  // Multiply by 3600 to convert km/s to km/h
    }

    public void ScalePlanetDiameters(List<Planet> planets)
    {
        // 1. Find max and min radii.
        double maxRadius = planets.Max(p => p.OrbitalData.equaRadius);
        double minRadius = planets.Min(p => p.OrbitalData.equaRadius);

        // Define the desired average size and its range.
        const double averageDiameter = 50;
        const double sizeRange = 20;  // give or take based on real-life sizes

        // 2. Determine scaling factor based on average size.
        double averageRadius = planets.Average(p => p.OrbitalData.equaRadius);
        double scaleFactor = averageDiameter / (2 * averageRadius);

        // 3. Scale each planet's diameter.
        foreach (var planet in planets)
        {
            double scaledDiameter = planet.OrbitalData.equaRadius * 2 * scaleFactor;

            // Ensure that the scaled values fall within the desired range.
            double diameterDifference = averageDiameter - scaledDiameter;
            if (Math.Abs(diameterDifference) > sizeRange)
            {
                scaledDiameter += Math.Sign(diameterDifference) * sizeRange;
            }

            planet.Diameter = scaledDiameter;
        }
    }

    public double FindScaleFactorAndResize(ref double semiMajorAxis, ref double semiMinorAxis, ref double existingScaleFactor )
    {
        double maxScreenHeight = 750;
        double maxScreenWidth = 750;

        // If we have an existing scale factor, apply it and return
        if (existingScaleFactor != 0)
        {
            semiMajorAxis *= existingScaleFactor;
            semiMinorAxis *= existingScaleFactor;
            return existingScaleFactor;
        }

        double originalSemiMajor = semiMajorAxis;
        double originalSemiMinor = semiMinorAxis;

        // Define a reasonable threshold; this will prevent us from scaling down too much
        double threshold = 0.95; // 95%

        // Start the scale factor at 1 and decrease it iteratively
        double scaleFactor = 1.0;

        while (semiMajorAxis > maxScreenWidth || semiMinorAxis > maxScreenHeight)
        {
            scaleFactor *= threshold; // Decrease the scale factor
            semiMajorAxis = originalSemiMajor * scaleFactor;
            semiMinorAxis = originalSemiMinor * scaleFactor;
        }

        return scaleFactor;
    }


    public double ResizeEllipseToFitScreen(ref double semiMajorAxis, ref double semiMinorAxis)
    {
        double maxScreenHeight = 750;
        double maxScreenWidth = 750;

        // Calculate the logarithmic scales for the semi-major and semi-minor axes
        var logSemiMajorAxis = Math.Log(semiMajorAxis);
        var logSemiMinorAxis = Math.Log(semiMinorAxis);

        // Map the logarithmic scales to fit within the screen dimensions logarithmically
        var logMaxScreenWidth = Math.Log(maxScreenWidth);
        var logMaxScreenHeight = Math.Log(maxScreenHeight);

        var scaledLogSemiMajorAxis = logSemiMajorAxis - logMaxScreenWidth;
        var scaledLogSemiMinorAxis = logSemiMinorAxis - logMaxScreenHeight;

        // Apply the exponential function to revert the logarithmic scaling
        semiMajorAxis = Math.Exp(scaledLogSemiMajorAxis);
        semiMinorAxis = Math.Exp(scaledLogSemiMinorAxis);

        // Calculate the scaled circumference using Ramanujan's formula
        var scaledCircumference = Math.PI * (3 * (semiMajorAxis + semiMinorAxis) -
                                             Math.Sqrt((3 * semiMajorAxis + semiMinorAxis) * (semiMajorAxis + 3 * semiMinorAxis)));

        return scaledCircumference;
    }

    public double ComputeSimulationVelocity(double realVelocityKmH, double scalingFactor)
    {
        return realVelocityKmH * scalingFactor;
    }


    public async Task<JObject> GetPlanetDataAsync(string planetName)
    {
        using (var client = new HttpClient())
        {
            var response = await client.GetStringAsync($"https://api.le-systeme-solaire.net/rest/bodies/{planetName}");
            return JObject.Parse(response);
        }
    }

    public string? GetPlanetData(string planetName)
    {
        var client = new RestClient(_dataSources.SolarSystemAPI.AssembleURL(PlanetaryData.OrbitalData));
        var request = new RestRequest($"https://api.le-systeme-solaire.net/rest/bodies/{planetName}", Method.Get);

        var response = client.Execute(request);

        return response.IsSuccessful 
            ? response.Content 
            : string.Empty;
    }


}
