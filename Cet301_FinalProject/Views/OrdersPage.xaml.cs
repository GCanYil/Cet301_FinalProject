using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet301_FinalProject.Views;

public partial class OrdersPage : ContentPage
{
    public OrdersPage()
    {
        InitializeComponent();
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