using System;
using System.Windows;
using InteractiveResume.View_Model;
using InteractiveResume.View_Model.EventArgs;
using InteractiveResume.View_Model.NASA;
using Microsoft.Extensions.DependencyInjection;

namespace InteractiveResume.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    //determine how big the screen is (but start with static value - 800 x 800)
    //
    //public event EventHandler<PlanetViewModelEventArgs> OrbitalPathControlLoaded;
    //public void RaiseOrbitalPathControlLoadedEvent(PlanetViewModel viewModel)
    //{
    //    OrbitalPathControlLoaded?.Invoke(this, new PlanetViewModelEventArgs(viewModel));
    //}


    private MainWindowViewModel viewModel => DataContext as MainWindowViewModel;
    public MainWindow()
    {
        DataContext = ((App)Application.Current).ServiceProvider.GetRequiredService<MainWindowViewModel>(); 
        InitializeComponent();
        viewModel.ScreenWidth = SystemParameters.PrimaryScreenWidth;
        viewModel.ScreenHeight = SystemParameters.PrimaryScreenHeight;
        Loaded += MainWindow_Loaded;
    }

    private void LoopThatBanjoBaby()
    {
        MediaPlayer.Position = TimeSpan.Zero; // Reset the media's position to the start.
        MediaPlayer.Play(); // Play the media again.

    }
    private void MediaPlayer_MediaEnded(object sender, RoutedEventArgs e)
    {
        LoopThatBanjoBaby();
    }


    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        // Access the ActualHeight and ActualWidth properties of the window to automatically set the grid size
        TheMainWindow.Height = ActualHeight;
        TheMainWindow.Width = ActualWidth;
        MediaPlayer.Source = new Uri(@"C:\Code\InteractiveResume\InteractiveResume\Resources\Deep Space Banjo.wav", UriKind.Absolute);
        MediaPlayer.Play();


        // uh oh bad code, shouldn't be calling on other view models from here
        //foreach (var planetViewModel in viewModel.PlanetViewModels)
        //{
        //    RaiseOrbitalPathControlLoadedEvent(planetViewModel);
        //}
    }

    private void Button1_OnClick(object sender, RoutedEventArgs e)
    {
        
    }

    private void MediaPlayer_MediaFailed(object sender, ExceptionRoutedEventArgs e)
    {
        MessageBox.Show($"Media failed to load/play. Error: {e.ErrorException.Message}", "Media Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }


    private void Button2_OnClick(object sender, RoutedEventArgs e)
    {

    }

    private void Button3_OnClick(object sender, RoutedEventArgs e)
    {

    }

    private void Button4_OnClick(object sender, RoutedEventArgs e)
    {

    }

    private void Button5_OnClick(object sender, RoutedEventArgs e)
    {

    }

    private void Button6_OnClick(object sender, RoutedEventArgs e)
    {

    }
    private void Button7_OnClick(object sender, RoutedEventArgs e)
    {

    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        var header = SqlController.GetAllResumeHeaders();
        var education = SqlController.GetAllResumeEducationModels();
        ;
    }

    private void OnEnter(object sender, DragEventArgs e)
    {

    }

    private void OnLeave(object sender, DragEventArgs e)
    {

    }
}