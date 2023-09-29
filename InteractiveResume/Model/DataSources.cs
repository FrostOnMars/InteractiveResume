using InteractiveResume.View_Model.NASA;

namespace InteractiveResume.Model;

public class DataSources
{
    public SystemeSolaire SolarSystemAPI { get; set; } = new();
}

public class SystemeSolaire
{
    public string BaseUrl = "https://api.le-systeme-solaire.net/";
    public string OrbitalDataUrl = "/rest/bodies/";

    public string AssembleURL(PlanetaryData type)
    {
        return type switch
        {
            PlanetaryData.OrbitalData => $"{BaseUrl}{OrbitalDataUrl}",
            //PlanetaryData.PlanetDescription => $"{BaseUrl}{PlanetDescriptionUrl}",
            //PlanetaryData.PlanetCoordinates => $"{BaseUrl}{CoordinatesUrl}",
            _ => string.Empty,
        };
    }

}