namespace Cet301_FinalProject;

public partial class App : Application
{
    public static bool IsAdmin = false;
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}