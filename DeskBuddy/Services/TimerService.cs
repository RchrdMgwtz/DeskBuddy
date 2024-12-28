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
    private DateTime _targetTime;

    public void Start()
    {
        var interval =
            TimeSpan.FromMinutes(settingsModel.IsStanding ? settingsModel.StandInterval : settingsModel.SitInterval);

        _targetTime = DateTime.Now.Add(interval);

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _timer.Tick += OnTimerTick;
        _timer.Start();
    }

    public void OnApplicationExit() => _timer.Stop();

    private void OnTimerTick(object? sender, EventArgs e)
    {
        var remainingTime = _targetTime - DateTime.Now;

        if (remainingTime.TotalSeconds > 0)
        {
            settingsModel.RemainingTime = remainingTime;
        }
        else
        {
            _timer.Stop();
            settingsModel.RemainingTime = TimeSpan.Zero;
            ShowNotification();
        }
    }

    private void ShowNotification()
    {
        var title = settingsModel.IsStanding ? Messages.Toast_TimeToSit_Title : Messages.Toast_TimeToStand_Title;
        var message = settingsModel.IsStanding ? Messages.Toast_TimeToSit_Message : Messages.Toast_TimeToStand_Message;

        var fileName = settingsModel.IsStanding ? DownFileName : UpFileName;
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
        var newInterval =
            TimeSpan.FromMinutes(settingsModel.IsStanding ? settingsModel.StandInterval : settingsModel.SitInterval);

        _targetTime = DateTime.Now.Add(newInterval);

        switch (e.Argument)
        {
            case OkArgument:
                settingsModel.IsStanding = !settingsModel.IsStanding;
                break;
            case ResetArgument:
                break;
            default:
                settingsModel.IsStanding = !settingsModel.IsStanding;
                break;
        }

        _timer.Start();
    }
}