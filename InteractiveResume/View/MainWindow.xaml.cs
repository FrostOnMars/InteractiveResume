using System;
using System.Windows;
using System.Windows.Media;
using InteractiveResume.View_Model;

namespace InteractiveResume.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel viewModel => DataContext as MainWindowViewModel;
        public MainWindow()
        {
            DataContext = new MainWindowViewModel();
            InitializeComponent();
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
            MainWindowGrid.Height= ActualHeight;
            MainWindowGrid.Width = ActualWidth;
            MediaPlayer.Source = new Uri(@"C:\Code\InteractiveResume\InteractiveResume\Resources\Deep Space Banjo.wav", UriKind.Absolute);

            MediaPlayer.Play();
        }

        private void Button1_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                MediaPlayer.Volume = 100;
                MediaPlayer.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Media failed to load/play. Error: {ex.Message}", "Media Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
    }
}
