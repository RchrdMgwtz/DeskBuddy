using System.IO;
using System.Windows.Threading;
using DeskBuddy.Models;
using DeskBuddy.Resources;
using Microsoft.Toolkit.Uwp.Notifications;

namespace DeskBuddy.Services;

public class TimerService(SettingsModel settingsModel) : ITimerService
{
    private const string DownFileName = "Down.png";
    private const string UpFileName = "Up.png";
    private const string ResourcesDirectory = "Resources";
    private const string OkArgument = "ok";
    private const string ResetArgument = "reset";

    private DispatcherTimer _timer = new();
    private bool _isStanding;

    public void Start()
    {
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMinutes(_isStanding ? settingsModel.StandInterval : settingsModel.SitInterval)
        };
        _timer.Tick += Timer_Tick;
        _timer.Start();
    }

    public void OnApplicationExit() => _timer.Stop();

    private void Timer_Tick(object? sender, EventArgs e)
    {
        _timer.Stop();
        ShowNotification();
    }

    private void ShowNotification()
    {
        var title = _isStanding ? Messages.Toast_TimeToSit_Title : Messages.Toast_TimeToStand_Title;
        var message = _isStanding ? Messages.Toast_TimeToSit_Message : Messages.Toast_TimeToStand_Message;

        var fileName = _isStanding ? DownFileName : UpFileName;
        var filePath = Path.Combine(Environment.CurrentDirectory, ResourcesDirectory, fileName);

        new ToastContentBuilder()
            .AddAppLogoOverride(new Uri(filePath))
            .AddText(title)
            .AddText(message)
            .AddButton(new ToastButton(Messages.Button_Ok, OkArgument)
                .SetBackgroundActivation())
            .AddButton(new ToastButton(Messages.Button_Reset, ResetArgument)
                .SetBackgroundActivation())
            .SetToastScenario(ToastScenario.Reminder)
            .Show();

        ToastNotificationManagerCompat.OnActivated += ToastActivated;
    }

    private void ToastActivated(ToastNotificationActivatedEventArgsCompat e)
    {
        var newInterval = TimeSpan.FromMinutes(_isStanding ? settingsModel.StandInterval : settingsModel.SitInterval);

        switch (e.Argument)
        {
            case OkArgument:
                _isStanding = !_isStanding;
                _timer.Interval = newInterval;
                break;
            case ResetArgument:
                break;
            default:
                _isStanding = !_isStanding;
                _timer.Interval = newInterval;
                break;
        }

        _timer.Start();
    }
}