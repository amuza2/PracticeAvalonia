using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PracticeAvalonia.Models;
using PracticeAvalonia.Services;
using PracticeAvalonia.Data;

namespace PracticeAvalonia.ViewModels;

public partial class UsersListViewModel : ViewModelBase
{
    private readonly INavigationService? _navigationService;
    [ObservableProperty] private ObservableCollection<User> _users;
    [ObservableProperty] private User? _selectedUser;

    public UsersListViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        Users = Data.UsersData.GetUsers();
    }
    public UsersListViewModel() : this(null!) { }
    
    [RelayCommand]
    private void ViewUserProfile(User user)
    {
        if (_navigationService != null)
        {
            var userProfileViewModel = new UserProfileViewModel(user, _navigationService);
            _navigationService.NavigateTo(userProfileViewModel);
        }
    }

    [RelayCommand]
    private void AddUser()
    {
        if (_navigationService != null)
        {
            var addUserViewModel = new AddUserViewModel(_navigationService);
            _navigationService.NavigateTo(addUserViewModel);
        }
    }

    [RelayCommand]
    private void EditUser(User user)
    {
        if (_navigationService != null)
        {
            var editUserViewModel = new EditUserViewModel(user, _navigationService);
            _navigationService.NavigateTo(editUserViewModel);   
        }
    }

    [RelayCommand]
    private void DeleteUser(User user)
    {
        try
        {
            Data.UsersData.DeleteUser(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}