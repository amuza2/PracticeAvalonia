using System.Collections.Generic;
using CommunityToolkit.Mvvm.Input;
using PracticeAvalonia.Models;
using PracticeAvalonia.ViewModels;
using PracticeAvalonia.Views;

namespace PracticeAvalonia.Services;

public class NavigationService : ViewModelBase,INavigationService
{
    private readonly NavigationAppViewModel _navigateAppViewModel;
    private readonly Stack<ViewModelBase> _navigationStock = new();

    public void ClearStack() => _navigationStock.Clear();
    public NavigationService(NavigationAppViewModel navigateAppViewModel)
    {
        _navigateAppViewModel = navigateAppViewModel;
    }
    public void NavigateTo<T>() where T : ViewModelBase, new()
    {
        var viewModel = new T();
        NavigateToInternal(viewModel);
    }
    public void NavigateTo<T>(T viewModel) where T : ViewModelBase
    {
        NavigateToInternal(viewModel);
    }
    private void NavigateToInternal(ViewModelBase viewModel)
    {
        if (_navigateAppViewModel.CurrentViewModel != null)
        {
            _navigationStock.Push(_navigateAppViewModel.CurrentViewModel);
        }
        
        _navigateAppViewModel.CurrentViewModel = viewModel;
        OnPropertyChanged(nameof(CanNavigateBack));
    }
    public void NavigateBack()
    {
        if (CanNavigateBack)
        {
            var previousViewModel = _navigationStock.Pop();
            _navigateAppViewModel.CurrentViewModel = previousViewModel;
            OnPropertyChanged(nameof(CanNavigateBack));
        }
    }
    public bool CanNavigateBack => _navigationStock.Count > 0;
}