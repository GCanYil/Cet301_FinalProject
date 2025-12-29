namespace Cet301_FinalProject.Models;
using SQLite;

[Table("Customer")]
public class Customer
{
    [PrimaryKey][AutoIncrement]public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
}