namespace Cet301_FinalProject.Models;
using SQLite;

[Table("Order")]
public class Order
{
    [PrimaryKey][AutoIncrement]public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalPrice { get; set; }
    
    [Indexed]public int CustomerId { get; set; }
}