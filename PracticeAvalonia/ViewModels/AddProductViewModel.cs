using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PracticeAvalonia.Models;
using PracticeAvalonia.Services;

namespace PracticeAvalonia.ViewModels;

public partial class AddProductViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    
    [ObservableProperty] private string _name;
    [ObservableProperty] private decimal _price;
    [ObservableProperty] private string _category;
    [ObservableProperty] private string _description;
    [ObservableProperty] private bool _isSaving = false;

    public AddProductViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }
    public AddProductViewModel() : this(null!) { }

    [RelayCommand]
    private void Cancel()
    {
        _navigationService.NavigateBack();
    }

    [RelayCommand]
    private async Task AddProduct()
    {
        try
        {
            IsSaving = true;

            await Task.Delay(1000);

            Product newProduct = new Product()
            {
                Name = Name,
                Price = Price,
                Category = Category,
                Description = Description
            };
            Data.UsersData.AddProduct(newProduct);
            
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
}