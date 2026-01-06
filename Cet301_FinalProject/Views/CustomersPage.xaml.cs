using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cet301_FinalProject.Models;
using Cet301_FinalProject.Services;

namespace Cet301_FinalProject.Views;

public partial class CustomersPage : ContentPage
{
    private LocalDBService _dbService = new LocalDBService();
    public CustomersPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (App.IsAdmin)
        {
            AddCustomerPanel.IsVisible = true;
        }
        else
        {
            AddCustomerPanel.IsVisible = false;
        }
        
        await LoadCustomers();
    }

    public async void RefreshUI()
    {
        await LoadCustomers();
    }
    
    public async void AddCustomer(object sender, EventArgs e)
    {
        var newCustomer = new Customer
        {
            Name = EntryCustomerName.Text,
            Email = EntryCustomerEmail.Text,
            Address = EntryCustomerAddress.Text,
            Phone = EntryCustomerPhone.Text,
        };
        await _dbService.CreateCustomer(newCustomer);
        EntryCustomerName.Text = "";
        EntryCustomerEmail.Text = "";
        EntryCustomerAddress.Text = "";
        EntryCustomerPhone.Text = "";
        RefreshUI();
    }

    public async Task LoadCustomers()
    {
        var customers = await _dbService.GetCustomers();
        Customers.ItemsSource = customers;
    }
    public async void DeleteCustomer(object sender, EventArgs e)
    {
        if (App.IsAdmin)
        {
            var button = (Button)sender;
            var customer = (Customer)button.CommandParameter;
            await _dbService.DeleteCustomer(customer);
            RefreshUI();
        }
        else
        {
            await DisplayAlert("Error","Only admins can delete customers", "OK");
        }
    }
}