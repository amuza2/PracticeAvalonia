using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PracticeAvalonia.Models;
using PracticeAvalonia.Services;

namespace PracticeAvalonia.ViewModels;

public partial class EditProductViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;

    private int id;
    [ObservableProperty] private string _name;
    [ObservableProperty] private decimal _price;
    [ObservableProperty] private string _category;
    [ObservableProperty] private string _description;
    [ObservableProperty] private bool _isSaving = false;

    public EditProductViewModel(INavigationService navigationService, Product product)
    {
        _navigationService = navigationService;
        id = product.Id;
        Name = product.Name;
        Price = product.Price;
        Category = product.Category;
        Description = product.Description;
    }

    public EditProductViewModel() : this(null!, new Product()) { }
        
    [RelayCommand]
    private async Task SaveProduct()
    {
        try
        {
            IsSaving = true;

            await Task.Delay(1000);
            
            Product updatedProduct = new Product()
            {
                Id = id,
                Name = Name,
                Price = Price,
                Category = Category,
                Description = Description
            };
            Data.UsersData.UpdateProduct(updatedProduct);
            _navigationService.NavigateBack();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            IsSaving = false;
        }
    }
    
    [RelayCommand]
    private void Cancel()
    {
        _navigationService.NavigateBack(); 
    }

    [RelayCommand]
    private void GoBack()
    {
        _navigationService.NavigateBack();   
    }
}