using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PracticeAvalonia.ViewModels;

public partial class CounterViewModel : ViewModelBase
{
    [ObservableProperty] private int _count;

    [RelayCommand]
    private void Increment() => Count++;

    [RelayCommand]
    private void Reset() => Count = 0;
}