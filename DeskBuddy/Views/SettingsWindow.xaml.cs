using System.Globalization;
using System.Windows;

namespace DeskBuddy.Views;

public partial class SettingsWindow
{
    public TimeSpan SitInterval { get; private set; }
    public TimeSpan StandInterval { get; private set; }

    public SettingsWindow(TimeSpan sitInterval, TimeSpan standInterval)
    {
        InitializeComponent();

        SitInterval = sitInterval;
        StandInterval = standInterval;
        
        SitIntervalTextBox.Text = SitInterval.TotalMinutes.ToString(CultureInfo.CurrentCulture);
        StandIntervalTextBox.Text = StandInterval.TotalMinutes.ToString(CultureInfo.CurrentCulture);
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (double.TryParse(SitIntervalTextBox.Text, out var sitMinutes) &&
            double.TryParse(StandIntervalTextBox.Text, out var standMinutes))
        {
            SitInterval = TimeSpan.FromMinutes(sitMinutes);
            StandInterval = TimeSpan.FromMinutes(standMinutes);
            DialogResult = true;
            Close();
        }
        else
        {
            MessageBox.Show("Please enter valid numbers for the intervals.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}