using System;
using System.Windows;
using System.Windows.Controls;
using InteractiveResume.View_Model.NASA;
using InteractiveResume.View_Model.EventArgs;

namespace InteractiveResume.View;

/// <summary>
/// Interaction logic for OrbitalPathControl.xaml
/// </summary>
public partial class OrbitalPathControl : UserControl
{
    public event EventHandler<PlanetViewModelEventArgs> OrbitalPathControlLoaded;

    public OrbitalPathControl()
    {
        //this.DataContext = new OrbitalPathControlViewModel(planetViewModelInstance);

        InitializeComponent();
        // Assuming mainWindow is an instance of your MainWindow.
        var mainWindow = Application.Current.MainWindow as MainWindow;
        //if (mainWindow != null)
        //{
        //    mainWindow.OrbitalPathControlLoaded += HandleOrbitalPathControlLoaded;
        //}

        //mainWindow.OrbitalPathControlLoaded += HandleOrbitalPathControlLoaded;
    }

    private void HandleOrbitalPathControlLoaded(object sender, PlanetViewModelEventArgs e)
    {
        this.DataContext = e.PlanetViewModel;
    }

    public void PlanetButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void OrbitalPathControl_Loaded(object sender, RoutedEventArgs e)
    {
        // Assuming OrbitalPathControlViewModel is the DataContext for your OrbitalPathControl
        if (DataContext is OrbitalPathControlViewModel viewModel)
        {
            var planetViewModel = viewModel.PlanetViewModel;  // Access the PlanetViewModel property

            // Now, invoke the event
            OrbitalPathControlLoaded?.Invoke(this, new PlanetViewModelEventArgs(planetViewModel));
        }
    }

}