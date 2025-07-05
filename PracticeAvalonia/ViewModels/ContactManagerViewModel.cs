using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PracticeAvalonia.Models;

namespace PracticeAvalonia.ViewModels;

public partial class ContactManagerViewModel : ViewModelBase
{
    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(AddContactCommand))] private string _newName = string.Empty;
    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(AddContactCommand))] private string _newEmail = string.Empty;
    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(AddContactCommand))] private string _newPhone = string.Empty;
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(FilteredContacts))] private string _searchText = string.Empty;
    [ObservableProperty] private Contact? _selectedContact;
    
    [ObservableProperty] private ObservableCollection<Contact> _contacts = new();

    public IEnumerable<Contact> FilteredContacts =>
        string.IsNullOrWhiteSpace(SearchText)
            ? Contacts
            : Contacts.Where(c => c.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

    public ContactManagerViewModel()
    {
        SampleData();
    }

    private void SampleData()
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
        var newContact = new Contact
        {
            Name = NewName,
            Email = NewEmail,
            Phone = NewPhone
        };
        Contacts.Add(newContact);
        NewName = NewEmail = NewPhone = string.Empty;
    }

    private bool CanAddContact()
    {
        return !string.IsNullOrWhiteSpace(NewName) &&
               !string.IsNullOrWhiteSpace(NewPhone) &&
               IsValidEmail(NewEmail);
    }

    private bool IsValidEmail(string newEmail)
    {
        try
        {
            var email = new System.Net.Mail.MailAddress(newEmail);
            return email.Address == newEmail;
        }
        catch
        {
            return false;
        }
    }

    // partial void OnNewNameChanged(string value)
    // {
    //     AddContactCommand.NotifyCanExecuteChanged();
    // }
    // partial void OnNewPhoneChanged(string value)
    // {
    //     AddContactCommand.NotifyCanExecuteChanged();
    // }
    // partial void OnNewEmailChanged(string value)
    // {
    //     AddContactCommand.NotifyCanExecuteChanged();
    // }

    [RelayCommand]
    private void DeleteContact(Contact contact)
    {
        Contacts.Remove(contact);
        if (SelectedContact == contact) SelectedContact = null;
        OnPropertyChanged(nameof(FilteredContacts));
    }

    // partial void OnSearchTextChanged(string text)
    // {
    //     OnPropertyChanged(nameof(FilteredContacts));
    // }
    
}