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
            if (string.IsNullOrEmpty(jsonString)) continue;
            planet.OrbitalData = JsonConvert.DeserializeObject<OrbitalDataModel>(jsonString);
            ResizeEllipseToFitScreen(ref planet.OrbitalData.semimajorAxis, ref planet.OrbitalData.perihelion);
            SpeedUpVelocity()
        }

    }
    public void ResizeEllipseToFitScreen(ref double semiMajorAxis, ref double semiMinorAxis)
    {
        double maxScreenHeight = 750;
        double maxScreenWidth = 750;
        

        // Calculate the logarithmic scales for the semi-major and semi-minor axes
        double logSemiMajorAxis = Math.Log(semiMajorAxis);
        double logSemiMinorAxis = Math.Log(semiMinorAxis);

        // Map the logarithmic scales to fit within the screen dimensions logarithmically
        double logMaxScreenWidth = Math.Log(maxScreenWidth);
        double logMaxScreenHeight = Math.Log(maxScreenHeight);

        double scaledLogSemiMajorAxis = logSemiMajorAxis - logMaxScreenWidth;
        double scaledLogSemiMinorAxis = logSemiMinorAxis - logMaxScreenHeight;

        // Apply the exponential function to revert the logarithmic scaling
        semiMajorAxis = Math.Exp(scaledLogSemiMajorAxis);
        semiMinorAxis = Math.Exp(scaledLogSemiMinorAxis);
    }

    public void SpeedUpVelocity(ref double velocity)
    {
        velocity *= 100;
    }
    public async Task<JObject> GetPlanetDataAsync(string planetName)
    {
        using (HttpClient client = new HttpClient())
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
