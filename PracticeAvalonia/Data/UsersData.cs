using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using PracticeAvalonia.Models;
using PracticeAvalonia.ViewModels;

namespace PracticeAvalonia.Data;

public partial class UsersData : ViewModelBase
{
    // Users
    [ObservableProperty] private static ObservableCollection<User> _users = new ObservableCollection<User>()
    {
        new User { Id = 1, Name = "John Doe", Email = "john@example.com", Department = "IT" },
        new User { Id = 2, Name = "Jane Smith", Email = "jane@example.com", Department = "HR" },
        new User { Id = 3, Name = "Bob Johnson", Email = "bob@example.com", Department = "Sales" }
    };
    public static ObservableCollection<User> GetUsers() => _users;
    public static void UpdateUsers(User updateUser)
    {
        var listUsers = _users.ToList();
        var index = listUsers.FindIndex(u => u.Id == updateUser.Id);
        if (index >= 0)
        {
            _users[index] = updateUser;
        }
    }
    
    public static int AddUser(User newUser)
    {
        newUser.Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1;
        _users.Add(newUser);
        return newUser.Id;
    }
    
    public static void DeleteUser(User user)
    {
        _users.Remove(user);
    }
    
    // Products
    [ObservableProperty] private static ObservableCollection<Product> _products = new ObservableCollection<Product>()
    {
        new Product { Id = 1, Name = "Laptop", Price = 999.99m, Category = "Electronics" },
        new Product { Id = 2, Name = "Mouse", Price = 29.99m, Category = "Electronics" },
        new Product { Id = 3, Name = "Keyboard", Price = 79.99m, Category = "Electronics" }
    };
    public static ObservableCollection<Product> GetProducts() => _products;

    public static int AddProduct(Product product)
    {
        product.Id = _products.Count > 0 ? _products.Max(p => p.Id) + 1 : 1; 
        _products.Add(product);
        return product.Id;
    }
    
    public static void UpdateProduct(Product updateProduct)
    {
        var listProducts = _products.ToList();
        var index = listProducts.FindIndex(p => p.Id == updateProduct.Id);
        if(index >= 0)
            _products[index] = updateProduct;
    }
    public static void DeleteProduct(Product product)
    {
        _products.Remove(product);
    }
}
