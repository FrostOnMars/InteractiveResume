using System.Windows;
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

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Access the ActualHeight and ActualWidth properties of the window to automatically set the grid size
            MainWindowGrid.Height= ActualHeight;
            MainWindowGrid.Width = ActualWidth;
        }

        private void Button1_OnClick(object sender, RoutedEventArgs e)
        {

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
