using InteractiveResume.View_Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InteractiveResume.View_Model.NASA;

namespace InteractiveResume.View;

/// <summary>
/// Interaction logic for OrbitalPathControl.xaml
/// </summary>
public partial class OrbitalPathControl : UserControl
{
    public OrbitalPathControl()
    {
        InitializeComponent();
    }

    public void Initialize(PlanetViewModel planetViewModel)
    {
        // Use the data from planetViewModel to initialize this ViewModel
    }

    public void PlanetButton_Click(object sender, RoutedEventArgs e)
    {

    }
}