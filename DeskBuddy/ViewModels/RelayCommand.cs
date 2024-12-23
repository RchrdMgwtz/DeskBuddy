using System.Windows.Input;

namespace DeskBuddy.ViewModels;

public class RelayCommand(Action execute, Func<bool> canExecute) : ICommand
{
    public bool CanExecute(object? parameter)
        => canExecute();

    public void Execute(object? parameter)
        => execute();

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}