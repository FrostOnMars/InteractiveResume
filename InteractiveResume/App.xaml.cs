using InteractiveResume.View;
using InteractiveResume.View_Model.NASA;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace InteractiveResume;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public IServiceProvider ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        ServiceProvider = ConfigureServices();
        // Now use ServiceProvider to fetch your main window and show it
        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
    public IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Register services and view models
        services.AddTransient<OrbitalPathControlViewModel>();
        // ... any other services or view models

        return services.BuildServiceProvider();
    }
}