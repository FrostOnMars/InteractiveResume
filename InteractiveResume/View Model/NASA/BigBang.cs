using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using InteractiveResume.Model.Planets;

namespace InteractiveResume.View_Model.NASA;

public sealed class BigBang : ObservableObject
{
    //Create singleton code to ensure only one instance of the BigBang is created.
    //Everyone must share this.

    private static readonly object LockObject = new();
    private static BigBang _instance = null;

    public List<Planet> Planets { get; } = new();

    private BigBang() { }

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