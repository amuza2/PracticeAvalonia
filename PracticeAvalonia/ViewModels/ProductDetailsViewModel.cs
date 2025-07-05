using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PracticeAvalonia.Models;
using PracticeAvalonia.Services;

namespace PracticeAvalonia.ViewModels;

public partial class ProductDetailsViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;

    [ObservableProperty] private Product _product;
    [ObservableProperty] private int _quantity = 1;
    [ObservableProperty] private decimal _totalPrice;
    public ProductDetailsViewModel() : this(new Product(), null!) {}
    
    public ProductDetailsViewModel(Product product, INavigationService navigateService)
    {
        _navigationService = navigateService;
        Product = product;
        CalculateTotalPrice();
    }

    partial void OnQuantityChanged(int value)
    {
        CalculateTotalPrice();   
    }
    private void CalculateTotalPrice()
    {
        TotalPrice = Product.Price * Quantity;
    }

    [RelayCommand]
    private void AddToCart()
    {
        if (Quantity > 0)
        {
            var message = $"Added {Quantity} x {Product.Name} to cart";
        }
    }
    
    [RelayCommand]
    private void GoBack()
    {
        _navigationService.NavigateBack();   
    }

    public bool CanGoBack => _navigationService.CanNavigateBack;
}