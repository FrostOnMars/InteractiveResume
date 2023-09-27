using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using InteractiveResume.Model;

namespace InteractiveResume.View_Model.NASA;

public class OrbitalData
{
    private BigBang _instance = BigBang.Instance;
    private DataSources _dataSources = new DataSources();
    private PlanetaryData _planetaryData { get; set; }

    public void GetData()
    {
        var taskList = _instance.Planets.Select(planet => GetPlanetDataAsync(planet.Name)).ToList();
        var jObjectsResults = Task.WhenAll(taskList).Result.ToList();
    }
    public async Task<JObject> GetPlanetDataAsync(string planetName)
    {
        using (HttpClient client = new HttpClient())
        {
            var response = await client.GetStringAsync($"https://api.le-systeme-solaire.net/rest/bodies/{planetName}");
            return JObject.Parse(response);
        }
    }

    public JObject GetPlanetData(string planetName)
    {
        var client = new RestClient(_dataSources.SolarSystemAPI.AssembleURL(PlanetaryData.OrbitalData));
        var request = new RestRequest($"https://api.le-systeme-solaire.net/rest/bodies/{planetName}", Method.Get);

        var response = client.Execute(request);

        if (response.IsSuccessful)
        {
            return JObject.Parse(response.Content);
        }
        else
        {
            // Handle the error, e.g., throw an exception or return a default JObject.
            return new JObject(); // Returning an empty JObject for simplicity; you may want to handle errors differently.
        }
    }
}
