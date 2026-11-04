using System.Reflection;
using Microsoft.Web.WebView2.WinForms;

namespace GoodDogWordle;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new GameForm());
    }
}

internal sealed class GameForm : Form
{
    private readonly WebView2 browser = new()
    {
        Dock = DockStyle.Fill
    };

    public GameForm()
    {
        Text = "Good Dog Wordle";
        Width = 540;
        Height = 880;
        MinimumSize = new Size(380, 650);
        StartPosition = FormStartPosition.CenterScreen;
        Controls.Add(browser);
        Load += LoadGame;
    }

    private async void LoadGame(object? sender, EventArgs e)
    {
        try
        {
            await browser.EnsureCoreWebView2Async();
            await using var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("GoodDogWordle.index.html")
                ?? throw new InvalidOperationException("The embedded game page could not be found.");
            using var reader = new StreamReader(stream);
            browser.CoreWebView2.NavigateToString(await reader.ReadToEndAsync());
        }
        catch (Exception exception)
        {
            MessageBox.Show(
                this,
                $"Good Dog Wordle couldn't start.\n\n{exception.Message}\n\nIf the WebView2 Runtime is missing, install the Microsoft Edge WebView2 Runtime and try again.",
                "Unable to start Good Dog Wordle",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            Close();
        }
    }
}
