using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DeskBuddy.Models;

namespace DeskBuddy.ViewModels;

public sealed partial class SettingsViewModel : INotifyPropertyChanged
{
    private readonly SettingsModel _settingsModel;

    private double _sitIntervalMinutes;
    private double _standIntervalMinutes;

    public SettingsViewModel(SettingsModel settingsModel)
    {
        _settingsModel = settingsModel;

        SitIntervalMinutes = _settingsModel.SitInterval.TotalMinutes;
        StandIntervalMinutes = _settingsModel.StandInterval.TotalMinutes;

        SaveCommand = new RelayCommand(Save, CanSave);
    }

    public double SitIntervalMinutes
    {
        get => _sitIntervalMinutes;
        set
        {
            _sitIntervalMinutes = value;
            OnPropertyChanged();
        }
    }

    public double StandIntervalMinutes
    {
        get => _standIntervalMinutes;
        set
        {
            _standIntervalMinutes = value;
            OnPropertyChanged();
        }
    }

    public ICommand SaveCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void Save()
    {
        _settingsModel.SitInterval = TimeSpan.FromMinutes(SitIntervalMinutes);
        _settingsModel.StandInterval = TimeSpan.FromMinutes(StandIntervalMinutes);
    }

    private static bool CanSave()
        => true;
}