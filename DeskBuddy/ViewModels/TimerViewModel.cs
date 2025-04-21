using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DeskBuddy.Models;
using DeskBuddy.Resources;

namespace DeskBuddy.ViewModels;

public sealed partial class TimerViewModel : INotifyPropertyChanged
{
    private readonly SettingsModel _settingsModel;
    private TimeSpan _remainingTime;

    public TimerViewModel(SettingsModel settingsModel)
    {
        _settingsModel = settingsModel;
        CloseCommand = new RelayCommand(Close, CanClose);
    }


    public string TimerMessage => _settingsModel.IsStanding
        ? Messages.Timer_RemainingUntilSit
        : Messages.Timer_RemainingUntilStand;

    public TimeSpan RemainingTime
    {
        get => _remainingTime;
        set
        {
            if (_remainingTime == value) return;
            _remainingTime = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(TimerMessage));
        }
    }

    public ICommand CloseCommand { get; }

    public Action? CloseWindow { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void Close() => CloseWindow?.Invoke();

    private static bool CanClose() => true;
}