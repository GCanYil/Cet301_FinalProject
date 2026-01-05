namespace Cet301_FinalProject;
using Cet301_FinalProject.Views;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(LogsPage), typeof(LogsPage));
    }
}