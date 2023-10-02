using System.Security.RightsManagement;

namespace InteractiveResume.Model;

public class DataSources
{
    public SystemeSolaire SolarSystemAPI { get; set; } = new();
    
    //initialize SQLite database info here
}