using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveResume.Model.Planets;

public class Planet
{
    public string Name { get; set; }
    public string Description { get; set; }
    public OrbitalDataModel OrbitalData { get; set; }
    public double Velocity { get; set; }
#region need to move this region to the pathing region
    public double Scale { get; set; }
    public double Distance { get; set; }

    #endregion

    private Planet() { }

    public static Planet Terraform(string name)
    {
        return new Planet
        {
            Name = name
        };
    }
}