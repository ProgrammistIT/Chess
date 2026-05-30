using Microsoft.Extensions.DependencyInjection;

namespace Chess;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(new AppShell());
        window.MinimumWidth = 400;
        window.MinimumHeight = 800;
        return window;
    }
}