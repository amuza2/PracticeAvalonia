using PracticeAvalonia.Models;
using PracticeAvalonia.ViewModels;

namespace PracticeAvalonia.Services;

public interface INavigationService
{
    void NavigateTo<T>() where T : ViewModelBase, new();
    void NavigateTo<T>(T viewModel) where T : ViewModelBase;
    void ClearStack();
    void NavigateBack();
    bool CanNavigateBack { get; }
}