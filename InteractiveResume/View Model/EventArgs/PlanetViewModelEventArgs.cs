using InteractiveResume.View_Model.NASA;

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
