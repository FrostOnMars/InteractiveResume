using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InteractiveResume.View_Model.NASA;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InteractiveResume.View_Model;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ClickCommand))]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string? firstName = "Mikayla";

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ClickCommand))]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string? lastName = "Martin";

    [ObservableProperty] 
    private string? fullName;

    [ObservableProperty]
    private ObservableCollection<PlanetViewModel> planetViewModels;



    public MainWindowViewModel()
    {
        PlanetViewModels = new ObservableCollection<PlanetViewModel>();

        foreach (var p in BigBang.Instance.Planets)
        {
            PlanetViewModels.Add(new PlanetViewModel(p));
        }
        FullName = $"{firstName} {lastName}";
    }

    [RelayCommand(CanExecute = nameof(CanClick))]
    public void Click()
    {
        FirstName = "Jon";
        LastName = "Martin";
        FullName = $"{FirstName} {LastName}";
    }

    private bool CanClick => FirstName == "Mikayla" && LastName == "Martin";

    //everything below is in the wrong place and will be moved later
    #region to be moved later

    public void GeneratePlanetPaths()
    {

    }

    public double LogScale(double distanceKm, double minDistanceKm, double maxDistanceKm, double minScale, double maxScale)
    {
        // Ensure the distance is clamped between min and max
        distanceKm = Math.Max(minDistanceKm, Math.Min(maxDistanceKm, distanceKm));

        // Calculate the logarithmic scales for min, max and current distance
        double logMin = Math.Log(minDistanceKm);
        double logMax = Math.Log(maxDistanceKm);
        double logValue = Math.Log(distanceKm);

        // Map the log scales to window units
        double scale = minScale + (maxScale - minScale) * ((logValue - logMin) / (logMax - logMin));

        return scale;
    }

#endregion

}
