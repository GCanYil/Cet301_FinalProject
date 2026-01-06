using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Media;
using Cet301_FinalProject.Models;
using Cet301_FinalProject.Services;
using Cet301_FinalProject.Views;

namespace Cet301_FinalProject.Views;

//Rider foreach'ları otomatik dolduruyor ve VARIABLE değerinin adını değiştirdiğimde hata alıyorum. R
//Refresh fonksiyonuna özel sandım ama bu dosyada hata alıyorum, o yüzden VARIABLE olarak bıraktım.

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
        if (_cartList.Count == 0)
        {
            PickerCustomer.IsEnabled = true;
        }
    }
    public async void ConfirmOrder(object sender, EventArgs e)
    {
        if (_cartList.Count == 0)
        {
            await DisplayAlert("Error", "There are no products in cart", "OK");
            return;
        }

        bool stockError = false;
        foreach (var VARIABLE in _cartList)
        {
            var allProducts = await _dbService.GetProducts();
            var currentProduct = allProducts.FirstOrDefault(product => product.Id == VARIABLE.PId);
            if (currentProduct.StockAmount < VARIABLE.Amount)
            {
                stockError = true;
                VARIABLE.RowColor = Colors.OrangeRed;
            }
            else
            {
                VARIABLE.RowColor = Color.FromArgb("#252525");
            }
        }

        if (stockError)
        {
            RefreshCart();
            await DisplayAlert("Error", "Not enough items in stock", "OK");
            return;
        }
        else
        {
            var selectedCustomer = (Customer)PickerCustomer.SelectedItem;
            decimal total = 0;
            foreach (var VARIABLE in _cartList)
            {
                total += VARIABLE.TPrice;
            }
            var newOrder = new Order
            {
                CustomerId = selectedCustomer.Id,
                Date = DateTime.Now,
                TotalPrice = total
            };
            await _dbService.CreateOrder(newOrder);

            foreach (var VARIABLE in _cartList)
            {
                var orderItem = new OrderItem
                {
                    OrderId = newOrder.Id,
                    ProductId = VARIABLE.PId,
                    Amount = VARIABLE.Amount,
                    UnitPrice = VARIABLE.Price
                };
                await _dbService.CreateOrderItem(orderItem);
                var allProducts = await _dbService.GetProducts();
                var currentProduct = allProducts.FirstOrDefault(product => product.Id == VARIABLE.PId);
                currentProduct.StockAmount -= VARIABLE.Amount;
                await _dbService.UpdateProduct(currentProduct);
            }
        }

        await DisplayAlert("Success", "Order set", "OK");
        
        _cartList.Clear();
        PickerCustomer.IsEnabled = true;
        RefreshCart();
        PickerCustomer.SelectedItem = null;
    }
}