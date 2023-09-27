using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveResume.View_Model.NASA
{
    public class OrbitalData
    {
        public async Task<JObject> GetPlanetDataAsync(string planetName)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync($"https://api.le-systeme-solaire.net/rest/bodies/{planetName}");
                return JObject.Parse(response);
            }
        }
    }
}
