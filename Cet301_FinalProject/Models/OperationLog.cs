namespace Cet301_FinalProject.Models;
using SQLite;

[Table("OperationLogs")]
public class OperationLog
{
    [PrimaryKey][AutoIncrement]public int Id { get; set; }
    public string OperationType { get; set; }
    public string Message { get; set; }
    public DateTime Date { get; set; }
}