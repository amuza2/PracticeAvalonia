using CommunityToolkit.Mvvm.Input;
using PracticeAvalonia.Models;
using PracticeAvalonia.Services;

namespace PracticeAvalonia.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    public HomeViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }
    public HomeViewModel() : this(null!) { }

    [RelayCommand]
    private void GoToUserProfile()
    {
        // _navigationService.NavigateToUserProfile(1);   
    }
    
    [RelayCommand]
    private void GoToProductDetails()
    {
        // Navigate with a complex object
        var sampleProduct = new Product
        {
            Id = 1,
            Name = "Sample Product",
            Price = 99.99m,
            Category = "Electronics",
            Description = "This is a sample product for demonstration."
        };
        var productDetailsViewModel = new ProductDetailsViewModel(sampleProduct, _navigationService);
        _navigationService.NavigateTo(productDetailsViewModel);
    }
}