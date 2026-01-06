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
    public List<CartItem> _cartList = new List<CartItem>();
    public async void AddToCart(object sender, EventArgs e)
    {
        if (PickerCustomer.SelectedItem == null || PickerProduct.SelectedItem == null || EntryAmount.Text == null)
        {
            await DisplayAlert("Error", "Please do not leave any empty space", "OK");
            return;
        }

        var selectedProduct = (Product)PickerProduct.SelectedItem;
        var newItem = new CartItem
        {
            PId = selectedProduct.Id,
            PName = selectedProduct.Name,
            Price = selectedProduct.Price,
            Amount = int.Parse(EntryAmount.Text)
        };
        _cartList.Add(newItem);
        PickerCustomer.IsEnabled = false;
        RefreshCart();
        EntryAmount.Text = "";
        PickerProduct.SelectedItem = null;
    }

    public void RefreshCart()
    {
        Cart.ItemsSource = null;
        Cart.ItemsSource = _cartList;
        decimal total = 0;
        foreach (var VARIABLE in _cartList)
        {
            total += VARIABLE.TPrice;
        }
        LabelTotal.Text = $"Total = {total:F2} TL";
    }
    
    public void DeleteFromCart(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var item = (CartItem)button.CommandParameter;
        _cartList.Remove(item);
        RefreshCart();
    }
    public void ConfirmOrder(object sender, EventArgs e)
    {
        
    }
}