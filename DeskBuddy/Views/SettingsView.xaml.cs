using DeskBuddy.ViewModels;

namespace DeskBuddy.Views;

public partial class SettingsView
{
    public SettingsView(SettingsViewModel viewModel)
    {
        InitializeComponent();
        viewModel.CloseWindow = Close;
        DataContext = viewModel;
    }
}