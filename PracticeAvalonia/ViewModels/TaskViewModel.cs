using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PracticeAvalonia.ViewModels;

public partial class TaskViewModel : ViewModelBase
{
    [ObservableProperty] private string _taskName = string.Empty;
    [ObservableProperty] private ObservableCollection<string> _tasks = new();

    public TaskViewModel()
    {
        Tasks.Add("Task 1");
        Tasks.Add("Task 2");
        Tasks.Add("Task 3");
    }
    
    [RelayCommand]
    private void AddTask()
    {
        if (!string.IsNullOrWhiteSpace(TaskName))
        {
            Tasks.Add(TaskName);
            TaskName = string.Empty;
        }
    }

    [RelayCommand]
    private void DeleteTask(string task)
    {
        Tasks.Remove(task);
    }

    [RelayCommand]
    private void ClearTasks()
    {
        Tasks.Clear();
    }
}