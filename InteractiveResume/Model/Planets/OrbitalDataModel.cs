using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveResume.Model.Planets;

[Serializable]
public class OrbitalDataModel
{
    public string SemimajorAxis { get; set; }
    public string Perihelion { get; set; } 
    public string Aphelion { get; set; } 
    public string Eccentricity { get; set; }
    public string OrbitalPeriod { get; set; } 
    public string OrbitalSpeed { get; set; }
}