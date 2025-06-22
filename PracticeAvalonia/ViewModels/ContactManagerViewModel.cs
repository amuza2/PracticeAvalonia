using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PracticeAvalonia.Models;

namespace PracticeAvalonia.ViewModels;

public partial class ContactManagerViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<Contact> _contacts = new();
    
    [ObservableProperty]
    private Contact? _selectedContact;
    
    [ObservableProperty]
    private string _searchText = string.Empty;
    
    [ObservableProperty]
    private string _newName = string.Empty;
    
    [ObservableProperty]
    private string _newEmail = string.Empty;
    
    [ObservableProperty]
    private string _newPhone = string.Empty;
    
    public IEnumerable<Contact> FilteredContacts => 
        string.IsNullOrWhiteSpace(SearchText)
            ? Contacts
            : Contacts.Where(c => c.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

    public ContactManagerViewModel()
    {
        LoadSampleData();
    }

    private void LoadSampleData()
    {
        Contacts.Add(new Contact { Name = "John Doe", Email = "john.doe@email.com", Phone = "555-0123" });
        Contacts.Add(new Contact { Name = "Jane Smith", Email = "jane.smith@email.com", Phone = "555-0456" });
        Contacts.Add(new Contact { Name = "Bob Johnson", Email = "bob.johnson@email.com", Phone = "555-0789" });
        Contacts.Add(new Contact { Name = "Alice Brown", Email = "alice.brown@email.com", Phone = "555-0321" });
        Contacts.Add(new Contact { Name = "Charlie Wilson", Email = "charlie.wilson@email.com", Phone = "555-0654" });
    }

    [RelayCommand(CanExecute = nameof(CanAddContact))]
    private void AddContact()
    {
        Contacts.Add(new Contact
        {
            Name = NewName.Trim(),
            Email = NewEmail.Trim(),
            Phone = NewPhone.Trim()
        });
        Clearform();
    }

    [RelayCommand]
    private void Clearform()
    {
        NewName = NewEmail = NewPhone = string.Empty;
    }

    [RelayCommand]
    private void ClearSearch()
    {
        SearchText = string.Empty;
    }

    [RelayCommand]
    private void DeleteContact(Contact contact)
    {
        if (SelectedContact == contact)
        {
            SelectedContact = null;
        }
        
        Contacts.Remove(contact);
    }

    partial void OnSearchTextChanged(string value)
    {
        OnPropertyChanged(nameof(FilteredContacts));
    }

    private bool CanAddContact() =>
        !string.IsNullOrWhiteSpace(NewName) &&
        !string.IsNullOrWhiteSpace(NewEmail) &&
        IsValidEmail(NewEmail);
    
    partial void OnNewNameChanged(string value) => AddContactCommand.NotifyCanExecuteChanged();
    partial void OnNewEmailChanged(string value) => AddContactCommand.NotifyCanExecuteChanged();

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}