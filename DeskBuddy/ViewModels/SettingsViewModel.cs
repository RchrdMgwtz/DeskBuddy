using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DeskBuddy.Models;

namespace DeskBuddy.ViewModels;

public sealed partial class SettingsViewModel : INotifyPropertyChanged
{
    private readonly SettingsModel _settingsModel;

    public SettingsViewModel(SettingsModel settingsModel)
    {
        _settingsModel = settingsModel;
        SaveCommand = new RelayCommand(Save, CanSave);
    }

    public int SitIntervalMinutes
    {
        get => _settingsModel.SitInterval;
        set
        {
            _settingsModel.SitInterval = value;
            OnPropertyChanged();
        }
    }

    public int StandIntervalMinutes
    {
        get => _settingsModel.StandInterval;
        set
        {
            _settingsModel.StandInterval = value;
            OnPropertyChanged();
        }
    }

    public ICommand SaveCommand { get; }
    
    public Action? CloseWindow { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void Save()
    {
        _settingsModel.SitInterval = SitIntervalMinutes;
        _settingsModel.StandInterval = StandIntervalMinutes;
        CloseWindow?.Invoke();
    }

    private static bool CanSave()
        => true;
}