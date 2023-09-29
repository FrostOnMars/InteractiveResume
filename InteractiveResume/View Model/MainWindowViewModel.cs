using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InteractiveResume.Model.Planets;
using InteractiveResume.View_Model.NASA;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace InteractiveResume.View_Model;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ClickCommand))]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string? _firstName = "Mikayla";

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ClickCommand))]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string? _lastName = "Martin";

    [ObservableProperty] 
    private string? _fullName;

    [ObservableProperty]
    private ObservableCollection<PlanetViewModel> _planetViewModels;

    [ObservableProperty] private double _screenWidth;
    [ObservableProperty] private double _screenHeight;

    public MainWindowViewModel()
    {
        PlanetViewModels = new ObservableCollection<PlanetViewModel>();
        var instance = BigBang.Instance;
        var orbitalData = new OrbitalData();
        // Subscribe to the event
        orbitalData.ErrorOccurred += HandleOrbitalDataError;
        orbitalData.GetData(PlanetaryData.OrbitalData, ScreenWidth, ScreenHeight);
        // Unsubscribe from the event, now that we are done getting the data.
        orbitalData.ErrorOccurred -= HandleOrbitalDataError;

        //foreach (var p in BigBang.Instance.Planets)
        //{
        //    PlanetViewModels.Add(new PlanetViewModel(p));
        //}

        PlanetViewModels ??= new ObservableCollection<PlanetViewModel>();
        foreach (var p in BigBang.Instance.Planets)
        {
            PlanetViewModels.Add(new PlanetViewModel(p));
        }

        FullName = $"{_firstName} {_lastName}";

        Debug.WriteLine($"Number of Planets: {PlanetViewModels.Count}");

    }


    private static void HandleOrbitalDataError(string errorMessage)
    {
        // Here, you need to make sure that you display the message box on the UI thread.
        // If using MVVM properly, you'd typically send a message to the view 
        // or use some service to show the error.
        MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
