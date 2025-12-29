namespace Cet301_FinalProject.Models;
using SQLite;

[Table("OrderItem")]
public class OrderItem
{
    [PrimaryKey][AutoIncrement]public int Id { get; set; }
    public int Amount { get; set; }
    public decimal UnitPrice { get; set; }
    
    [Indexed]public int OrderId { get; set; }
    [Indexed]public int ProductId { get; set; }
}