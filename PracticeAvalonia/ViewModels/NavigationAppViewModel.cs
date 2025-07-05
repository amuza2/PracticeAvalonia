using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PracticeAvalonia.Services;

namespace PracticeAvalonia.ViewModels;

public partial class NavigationAppViewModel : ViewModelBase
{
    [ObservableProperty] private ViewModelBase _currentViewModel;
    private readonly INavigationService _navigationService;
    
    public NavigationAppViewModel()
    {
        _navigationService = new NavigationService(this);
        CurrentViewModel = new HomeViewModel(_navigationService);
    }
    public INavigationService NavigationService => _navigationService;

    [RelayCommand]
    private void NavigateToHome()
    {
        var homeVM = new HomeViewModel(_navigationService);
        _navigationService.NavigateTo(homeVM);
        _navigationService.ClearStack();
        // CurrentViewModel = new HomeViewModel(_navigationService);
    }

    [RelayCommand]
    private void NavigateToUsersList()
    {
        var usersListVM = new UsersListViewModel(_navigationService);
        _navigationService.NavigateTo(usersListVM);
        // CurrentViewModel = new UsersListViewModel(_navigationService);
    }
    [RelayCommand]
    private void NavigateToProductsList()
    {
        var productsListVM = new ProductsListViewModel(_navigationService);
        _navigationService.NavigateTo(productsListVM);
        // CurrentViewModel = new ProductsListViewModel(_navigationService);
    }
    
}