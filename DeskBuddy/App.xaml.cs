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
    private const int SitInterval = 45;
    private const int StandInterval = 15;

    private NotifyIcon _trayIcon = new();
    private ITimerService? _timerService;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        ShutdownMode = ShutdownMode.OnExplicitShutdown;

        var builder = new ContainerBuilder();
        ConfigureServices(builder);
        var container = builder.Build();

        var initialView = container.Resolve<InitialView>();
        initialView.ShowDialog();

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

        _trayIcon.MouseClick += (_, e) =>
        {
            if (e.Button == MouseButtons.Left)
            {
                ShowTimerWindow(container);
            }
        };

        var contextMenu = new ContextMenuStrip();
        contextMenu.Items.Add(Messages.Button_Settings, null, (_, _) => ShowConfigurationWindow(container));
        contextMenu.Items.Add(Messages.Button_Exit, null, (_, _) => ShutdownApplication());

        _trayIcon.ContextMenuStrip = contextMenu;
    }

    private static void ShowTimerWindow(IContainer container)
    {
        var timerView = container.Resolve<TimerView>();
        timerView.Show();
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

    private static void ConfigureServices(ContainerBuilder builder)
    {
        builder.RegisterType<TimerService>().As<ITimerService>();

        // Settings
        builder.RegisterInstance(new SettingsModel
        {
            SitInterval = SitInterval,
            StandInterval = StandInterval
        }).SingleInstance();
        builder.RegisterType<SettingsViewModel>();
        builder.RegisterType<SettingsView>();

        // Timer
        builder.RegisterType<TimerViewModel>().AsSelf().SingleInstance();
        builder.RegisterType<TimerView>();

        // Initial
        builder.RegisterType<InitialView>();
    }
}