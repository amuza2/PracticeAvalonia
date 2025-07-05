using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PracticeAvalonia.Models;
using PracticeAvalonia.Services;

namespace PracticeAvalonia.ViewModels;

public partial class AddUserViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    
    [ObservableProperty] private string _name = string.Empty;
    [ObservableProperty] private string _email = string.Empty;
    [ObservableProperty] private string _department = string.Empty;
    [ObservableProperty] private DateTimeOffset _createdDate = DateTimeOffset.Now;
    [ObservableProperty] private bool _isAddingUser = false;

    public AddUserViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public AddUserViewModel() : this(null!) { }
    
    [RelayCommand]
    private async Task AddUser()
    {
        try
        {
            IsAddingUser = true;

            await Task.Delay(1000);

            var newUser = new User
            {
                Name = Name,
                Email = Email,
                Department = Department,
                CreatedDate = CreatedDate
            };
            var userId = Data.UsersData.AddUser(newUser);
            
            Console.WriteLine($"User added with id: {userId}");
            
            _navigationService.NavigateBack(); 
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            IsAddingUser = false;
        }
        
    }
    
    [RelayCommand]
    private void Cancel()
    {
        _navigationService.NavigateBack();  
    }
}