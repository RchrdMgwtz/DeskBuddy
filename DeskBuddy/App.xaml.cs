using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using ABI.Windows.Storage;
using DeskBuddy.Resources;
using DeskBuddy.Views;
using Microsoft.Toolkit.Uwp.Notifications;
using Application = System.Windows.Application;
using StorageFile = Windows.Storage.StorageFile;

namespace DeskBuddy;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private NotifyIcon _trayIcon = new();
    private DispatcherTimer _timer = new();
    private TimeSpan _sitInterval = TimeSpan.FromMinutes(0.1);
    private TimeSpan _standInterval = TimeSpan.FromMinutes(0.25);
    private bool _isStanding;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        ShutdownMode = ShutdownMode.OnExplicitShutdown;

        InitializeTrayIcon();
        InitializeTimer();
    }

    private void InitializeTrayIcon()
    {
        _trayIcon = new NotifyIcon
        {
            Icon = new Icon("Resources/Icon.ico"),
            Visible = true,
            Text = Messages.ApplicationTitle
        };

        var contextMenu = new ContextMenuStrip();
        contextMenu.Items.Add(Messages.Button_Settings, null, (_, _) => ShowConfigurationWindow());
        contextMenu.Items.Add(Messages.Button_Exit, null, (_, _) => ShutdownApplication());

        _trayIcon.ContextMenuStrip = contextMenu;
    }

    private void InitializeTimer()
    {
        _timer = new DispatcherTimer
        {
            Interval = _isStanding ? _standInterval : _sitInterval
        };
        _timer.Tick += Timer_Tick;
        _timer.Start();
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        _timer.Stop();
        ShowNotification();
    }

    private void ShowNotification()
    {
        var title = _isStanding ? Messages.Toast_TimeToSit_Title : Messages.Toast_TimeToStand_Title;
        var message = _isStanding ? Messages.Toast_TimeToSit_Message : Messages.Toast_TimeToStand_Message;

        var fileName = _isStanding ? "Down.png" : "Up.png";
        var filePath = Path.Combine(Environment.CurrentDirectory, "Resources", fileName);

        new ToastContentBuilder()
            .AddAppLogoOverride(new Uri(filePath))
            .AddText(title)
            .AddText(message)
            .AddButton(new ToastButton(Messages.Button_Ok, "ok")
                .SetBackgroundActivation())
            .AddButton(new ToastButton(Messages.Button_Reset, "reset")
                .SetBackgroundActivation())
            .SetToastScenario(ToastScenario.Reminder)
            .Show();

        ToastNotificationManagerCompat.OnActivated += ToastActivated;
    }

    private void ToastActivated(ToastNotificationActivatedEventArgsCompat e)
    {
        switch (e.Argument)
        {
            case "ok":
                _isStanding = !_isStanding;
                _timer.Interval = _isStanding ? _standInterval : _sitInterval;
                break;
            case "reset":
                break;
            default:
                _isStanding = !_isStanding;
                _timer.Interval = _isStanding ? _standInterval : _sitInterval;
                break;
        }

        _timer.Start();
    }

    private void ShowConfigurationWindow()
    {
        var settingsWindow = new SettingsWindow(_sitInterval, _standInterval);

        if (settingsWindow.ShowDialog() == false) return;

        _sitInterval = settingsWindow.SitInterval;
        _standInterval = settingsWindow.StandInterval;
        _timer.Interval = _isStanding ? _standInterval : _sitInterval;
    }

    private void ShutdownApplication()
    {
        _trayIcon.Visible = false;
        _trayIcon.Dispose();
        Current.Shutdown();
    }
}