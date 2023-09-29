using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using InteractiveResume.Model.Planets;

namespace InteractiveResume.View_Model.NASA;

public sealed class BigBang : ObservableObject
{
    private static readonly object LockObject = new();
    private static BigBang _instance = null;

    public List<Planet> Planets { get; } = new();

    private BigBang()
    {
        // Private constructor to prevent external instantiation.
        Planets.Add(Planet.Terraform("Mercury"));
        Planets.Add(Planet.Terraform("Venus"));
        Planets.Add(Planet.Terraform("Earth"));
        Planets.Add(Planet.Terraform("Mars"));
        Planets.Add(Planet.Terraform("Jupiter"));
        Planets.Add(Planet.Terraform("Saturn"));
        Planets.Add(Planet.Terraform("Uranus"));
        Planets.Add(Planet.Terraform("Neptune"));
        Planets.Add(Planet.Terraform("Pluto"));
    }

    public static BigBang Instance
    {
        get
        {
            if (_instance != null) return _instance;
            lock (LockObject)
            {
                _instance ??= new BigBang();
            }
            return _instance;
        }
    }

}
public enum PlanetaryData
{
    OrbitalData
}