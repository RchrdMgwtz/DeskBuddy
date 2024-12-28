using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DeskBuddy.Models;

namespace DeskBuddy.ViewModels;

public partial class TimerViewModel : INotifyPropertyChanged
{
    private readonly SettingsModel _settingsModel;

    public TimerViewModel(SettingsModel settingsModel)
    {
        _settingsModel = settingsModel;
        CloseCommand = new RelayCommand(Close, CanClose);
    }

    public ICommand CloseCommand { get; }

    public Action? CloseWindow { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void Close() => CloseWindow?.Invoke();
    
    private static bool CanClose() => true;
}