namespace Cet301_FinalProject;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
    }

    //OnAppearing fonksiyonunu AI'dan aldım hocam.
    protected override void OnAppearing()
    {
        base.OnAppearing();
        RefreshUI();
    }

    private void RefreshUI()
    {
        if (App.IsAdmin)
        {
            LBLUserStatus.Text = "User Status: Admin";
            LBLUserStatus.TextColor = Colors.GreenYellow;
            LoginPanel.IsVisible = false;
            AdminPanel.IsVisible = true;
        }
        else
        {
            LBLUserStatus.Text = "User Status: Standard";
            LBLUserStatus.TextColor = Colors.Gold;
            LoginPanel.IsVisible = true;
            AdminPanel.IsVisible = false;
        }
    }
    
    private void LoginClicked(object sender, EventArgs e) {}
    private void LogoutClicked(object sender, EventArgs e) {}
    
    private void ToCustomers(object sender, EventArgs e) {}
    private void ToProducts(object sender, EventArgs e) {}
    private void ToOrders(object sender, EventArgs e) {}
    private void ToLogs(object sender, EventArgs e) {}
    
}