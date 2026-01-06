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
    }
}