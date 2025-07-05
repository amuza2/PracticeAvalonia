using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PracticeAvalonia.Models;
using PracticeAvalonia.Services;

namespace PracticeAvalonia.ViewModels;

public partial class UserProfileViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    [ObservableProperty] private int _userId;
    [ObservableProperty] private User? _user;
    [ObservableProperty] private bool _isLoading = true;
    [ObservableProperty] private string _errorMessage = string.Empty;
    
    public UserProfileViewModel(User user, INavigationService navigationService)
    {
        // if(userId <= 0) throw new ArgumentException("User Id must be greater than 0");
        // if(navigationService == null) throw new ArgumentNullException(nameof(navigationService));
        
        _navigationService = navigationService;
        User = user;
        LoadUserData();
    }
    public UserProfileViewModel( ) : this(null!, null!) {}
    
    private async void LoadUserData()
    {
        try
        {
            IsLoading = true;
            ErrorMessage = string.Empty;
            await Task.Delay(1000);

            User = new User
            {
                Id = User.Id,
                Name = User.Name,
                Email = User.Email,
                Department = User.Department,
                CreatedDate = User.CreatedDate,
            };
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Failed to load user: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }
    
    [RelayCommand]
    private void EditUser()
    {
        var userEditViewModel = new EditUserViewModel(User, _navigationService);
        _navigationService.NavigateTo(userEditViewModel);;   
    }

    [RelayCommand]
    private void GoBack()
    {
        if(_navigationService.CanNavigateBack)
            _navigationService.NavigateBack();   
    }
}