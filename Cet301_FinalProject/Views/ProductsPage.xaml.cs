using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cet301_FinalProject.Models;
using Cet301_FinalProject.Services;

namespace Cet301_FinalProject.Views;

public partial class ProductsPage : ContentPage
{
    private LocalDBService _dbService = new LocalDBService();
    public ProductsPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (App.IsAdmin)
        {
            AddProductPanel.IsVisible = true;
        }
        else
        {
            AddProductPanel.IsVisible = false;
        }
        
        await LoadProducts();
    }

    public async void RefreshUI()
    {
        await LoadProducts();
    }

    public async void AddProduct(object sender, EventArgs e)
    {
        var newproduct = new Product
        {
            Name = EntryProductName.Text,
            Price = decimal.Parse(EntryProductPrice.Text),
            StockAmount = int.Parse(EntryProductStock.Text)
        };
        await _dbService.CreateProduct(newproduct);
        EntryProductName.Text = "";
        EntryProductPrice.Text = "";
        EntryProductStock.Text = "";
        RefreshUI();
    }

    public async Task LoadProducts()
    {
        var products = await _dbService.GetProducts();
        Products.ItemsSource = products;
    }
    public void ChangeStock(object sender, EventArgs e)
    {
        
    }
    
    public async void DeleteProduct(object sender, EventArgs e)
    {
        if (App.IsAdmin)
        {
            var button = (Button)sender;
            var product = (Product)button.CommandParameter;
            await _dbService.DeleteProduct(product);
            RefreshUI();
        }
        else
        {
            await DisplayAlert("Error","Only admins can delete customers", "OK");
        }
    }
}