using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
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
    private string? lastName = "Baltierra";

    [ObservableProperty] 
    private string? fullName;


    public MainWindowViewModel()
    {
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
}
