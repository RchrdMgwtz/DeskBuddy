using DeskBuddy.ViewModels;

namespace DeskBuddy.Views;

public partial class TimerView
{
    public TimerView(TimerViewModel viewModel)
    {
        InitializeComponent();
        viewModel.CloseWindow = Close;
        DataContext = viewModel;
    }
}