using DeskBuddy.ViewModels;

namespace DeskBuddy.Views;

public partial class InitialView
{
    public InitialView(SettingsViewModel viewModel)
    {
        InitializeComponent();
        viewModel.CloseWindow = Close;
        DataContext = viewModel;
    }
}