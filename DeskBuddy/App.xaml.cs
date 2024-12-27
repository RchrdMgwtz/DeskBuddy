using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using Autofac;
using DeskBuddy.Models;
using DeskBuddy.Resources;
using DeskBuddy.Services;
using DeskBuddy.ViewModels;
using DeskBuddy.Views;

namespace DeskBuddy;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private NotifyIcon _trayIcon = new();
    private ITimerService? _timerService;
    private readonly TimeSpan _sitInterval = TimeSpan.FromMinutes(0.1);
    private readonly TimeSpan _standInterval = TimeSpan.FromMinutes(0.25);

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        ShutdownMode = ShutdownMode.OnExplicitShutdown;

        var builder = new ContainerBuilder();
        ConfigureServices(builder);
        var container = builder.Build();

        InitializeTrayIcon(container);

        _timerService = container.Resolve<ITimerService>();
        _timerService.Start();
    }

    protected override void OnExit(ExitEventArgs e) => _timerService?.OnApplicationExit();

    private void InitializeTrayIcon(IContainer container)
    {
        _trayIcon = new NotifyIcon
        {
            Icon = new Icon("Resources/Icon.ico"),
            Visible = true,
            Text = Messages.ApplicationTitle
        };

        var contextMenu = new ContextMenuStrip();
        contextMenu.Items.Add(Messages.Button_Settings, null, (_, _) => ShowConfigurationWindow(container));
        contextMenu.Items.Add(Messages.Button_Exit, null, (_, _) => ShutdownApplication());

        _trayIcon.ContextMenuStrip = contextMenu;
    }


    private static void ShowConfigurationWindow(IContainer container)
    {
        var settingsView = container.Resolve<SettingsView>();
        settingsView.ShowDialog();
    }

    private void ShutdownApplication()
    {
        _trayIcon.Visible = false;
        _trayIcon.Dispose();
        Current.Shutdown();
    }

    private void ConfigureServices(ContainerBuilder builder)
    {
        builder.RegisterType<TimerService>().As<ITimerService>();

        // Settings
        builder.RegisterInstance(new SettingsModel
        {
            SitInterval = _sitInterval,
            StandInterval = _standInterval
        }).SingleInstance();
        builder.RegisterType<SettingsViewModel>();
        builder.RegisterType<SettingsView>();
    }
}