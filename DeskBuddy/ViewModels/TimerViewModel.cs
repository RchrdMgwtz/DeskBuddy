using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace DeskBuddy.ViewModels;

public sealed partial class TimerViewModel : INotifyPropertyChanged
{
    private TimeSpan _remainingTime;

    public TimerViewModel() => CloseCommand = new RelayCommand(Close, CanClose);

    public TimeSpan RemainingTime
    {
        get => _remainingTime;
        set
        {
            if (_remainingTime == value) return;
            _remainingTime = value;
            OnPropertyChanged();
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