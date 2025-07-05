using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PracticeAvalonia.Models;
using PracticeAvalonia.Services;

namespace PracticeAvalonia.ViewModels;

public partial class ProductsListViewModel : ViewModelBase
{
    private readonly INavigationService? _navigationService;
    [ObservableProperty] private ObservableCollection<Product> _products = new();
    [ObservableProperty] private Product? _selectedProduct;

    public ProductsListViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        Products = Data.UsersData.GetProducts();
    }

    public ProductsListViewModel() : this(null!) { }

    [RelayCommand]
    private void ViewProductDetails(Product product)
    {
        var productDetailsViewModel = new ProductDetailsViewModel(product, _navigationService);
        _navigationService?.NavigateTo(productDetailsViewModel);
    }

    [RelayCommand]
    private void EditProduct(Product product)
    {
        var productEditViewModel = new EditProductViewModel(_navigationService, product);
        _navigationService?.NavigateTo(productEditViewModel);
    }
    
    [RelayCommand]
    private void DeleteProduct(Product product)
    {
        Data.UsersData.DeleteProduct(product);
    }

    [RelayCommand]
    private void AddProduct()
    {
        var productAddViewModel = new AddProductViewModel(_navigationService);
        _navigationService?.NavigateTo(productAddViewModel);
    }
}