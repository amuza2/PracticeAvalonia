using System;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PracticeAvalonia.Models;
using PracticeAvalonia.Services;

namespace PracticeAvalonia.ViewModels;

public partial class EditUserViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly User _originalUser;
    
    [ObservableProperty] private string _name = string.Empty;
    [ObservableProperty] private string _email = string.Empty;
    [ObservableProperty] private string _department = string.Empty;
    [ObservableProperty] private DateTimeOffset _createdDate;
    [ObservableProperty] private bool _isSaving = false;

    public EditUserViewModel(User user, INavigationService navigationService)
    {
        _navigationService = navigationService;
        _originalUser = user;

        Name = user.Name;
        Email = user.Email;
        Department = user.Department;
        CreatedDate = user.CreatedDate;
    }

    public EditUserViewModel() : this(new User(), null!){}

    [RelayCommand]
    private async Task SaveUser()
    {
        try
        {
            IsSaving = true;
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Email))
            {
                return;
            }

            await Task.Delay(1000);

            _originalUser.Name = Name;
            _originalUser.Email = Email;
            _originalUser.Department = Department;
            _originalUser.CreatedDate = CreatedDate;

            Data.UsersData.UpdateUsers(_originalUser);

            _navigationService.NavigateBack();
        }
        catch
        {
            // Handle error
            
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
    public bool CanGoBack => _navigationService.CanNavigateBack;
    
}