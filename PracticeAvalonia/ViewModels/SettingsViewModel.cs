using CommunityToolkit.Mvvm.ComponentModel;

namespace PracticeAvalonia.ViewModels;

public partial class SettingsViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _notificationsEnabled = true;

    [ObservableProperty]
    private bool _darkModeEnabled = false;
}