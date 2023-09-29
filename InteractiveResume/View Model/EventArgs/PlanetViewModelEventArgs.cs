using InteractiveResume.View_Model.NASA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveResume.View_Model.EventArgs
{
    public class PlanetViewModelEventArgs : System.EventArgs
    {
        public PlanetViewModel PlanetViewModel { get; }

        public PlanetViewModelEventArgs(PlanetViewModel planetViewModel)
        {
            PlanetViewModel = planetViewModel;
        }
    }
}
