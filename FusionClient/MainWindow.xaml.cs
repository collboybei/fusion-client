using System.Diagnostics;
using System.IO;
using System.Windows;
using CmlLib.Core;
using CmlLib.Core.Auth;
using CmlLib.Core.Downloader;

namespace FusionClient;

public partial class MainWindow : Window
{
    private readonly MinecraftLauncher _launcher;
    private MSession? _session;
    private readonly string _gameDir = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "FusionClient", ".minecraft");

    public MainWindow()
    {
        InitializeComponent();
        Directory.CreateDirectory(_gameDir);
        var path = new MinecraftPath(_gameDir);
        _launcher = new MinecraftLauncher(path);
        VersionBox.ItemsSource = new[] { "release", "1.21.11", "1.21.10", "1.21.8", "1.21.1", "1.20.1" };
        VersionBox.SelectedIndex = 0;
        RamSlider.ValueChanged += (_, _) => RamLabel.Text = $"{(int)RamSlider.Value} GB";
    }

    private async void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            SetBusy(true, "Microsoft-Anmeldung wird vorbereitet …");
            // CmlLib's device-code login opens the Microsoft sign-in flow.
            var loginHandler = JELoginHandlerBuilder.BuildDefault();
            _session = await loginHandler.Authenticate();
            AccountLabel.Text = _session.Username;
            StatusLabel.Text = $"Angemeldet als {_session.Username}.";
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Anmeldung nicht möglich",
                MessageBoxButton.OK, MessageBoxImage.Error);
            StatusLabel.Text = "Microsoft-Anmeldung fehlgeschlagen.";
        }
        finally { SetBusy(false); }
    }

    private async void PlayButton_Click(object sender, RoutedEventArgs e)
    {
        if (_session is null)
        {
            MessageBox.Show(this,
                "Bitte melde dich zuerst mit einem Microsoft-Konto an, das Minecraft: Java Edition besitzt.",
                "Anmeldung erforderlich", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var selected = VersionBox.SelectedItem?.ToString() ?? "release";
        try
        {
            SetBusy(true, $"Minecraft {selected} wird vorbereitet …");
            var versions = await _launcher.GetAllVersionsAsync();
            var version = selected == "release"
                ? versions.FirstOrDefault(v => v.Type == "release")?.Name
                : selected;

            if (string.IsNullOrWhiteSpace(version))
                throw new InvalidOperationException("Die ausgewählte Minecraft-Version wurde nicht gefunden.");

            var launchOption = new MLaunchOption
            {
                Session = _session,
                MaximumRamMb = (int)RamSlider.Value * 1024,
                MinimumRamMb = 1024
            };

            var process = await _launcher.CreateProcessAsync(version, launchOption);
            process.Start();
            StatusLabel.Text = $"Minecraft {version} wurde gestartet.";
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Minecraft konnte nicht gestartet werden",
                MessageBoxButton.OK, MessageBoxImage.Error);
            StatusLabel.Text = "Spielstart fehlgeschlagen.";
        }
        finally { SetBusy(false); }
    }

    private void SetBusy(bool busy, string? status = null)
    {
        PlayButton.IsEnabled = !busy;
        LoginButton.IsEnabled = !busy;
        Progress.Visibility = busy ? Visibility.Visible : Visibility.Collapsed;
        if (status is not null) StatusLabel.Text = status;
    }
}
