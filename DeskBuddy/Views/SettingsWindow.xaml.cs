using DeskBuddy.Models;
using DeskBuddy.ViewModels;

namespace DeskBuddy.Views;

public partial class SettingsWindow
{
    public SettingsWindow(SettingsModel model)
    {
        InitializeComponent();
        DataContext = new SettingsViewModel(model);
    }
}