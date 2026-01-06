using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cet301_FinalProject.Models;
using Cet301_FinalProject.Services;
using Cet301_FinalProject.Views;

namespace Cet301_FinalProject.Views;

public partial class OrdersPage : ContentPage
{
    private LocalDBService _dbService = new LocalDBService();
    public OrdersPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var customers = await _dbService.GetCustomers();
        PickerCustomer.ItemsSource = customers;
        var products = await _dbService.GetProducts();
        PickerProduct.ItemsSource = products;
    }

    public class CartItem
    {
        public int PId { get; set; }
        public string PName { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public decimal TPrice => Price * Amount;
        public Color RowColor { get; set; } = Color.FromArgb("#252525");
    }
    public void AddToCart(object sender, EventArgs e)
    {
        
    }
    
    public void DeleteFromCart(object sender, EventArgs e)
    {
        
    }
    public void ConfirmOrder(object sender, EventArgs e)
    {
        
    }
}