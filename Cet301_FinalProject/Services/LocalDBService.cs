namespace Cet301_FinalProject.Services;
using SQLite;
using Cet301_FinalProject.Models;

public class LocalDBService
{
    private SQLiteAsyncConnection _connection;

    public async Task Init()
    {
        if (_connection != null)
        {
            return;
        }

        var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Cet301.db");
        _connection = new SQLiteAsyncConnection(databasePath);

        await _connection.CreateTableAsync<Customer>();
        await _connection.CreateTableAsync<Product>();
        await _connection.CreateTableAsync<Order>();
        await _connection.CreateTableAsync<OrderItem>();
    }

    public async Task<List<Customer>> GetCustomers()
    {
        await Init();
        return await _connection.Table<Customer>().ToListAsync();
    }

    public async Task<List<Product>> GetProducts()
    {
        await Init();
        return await _connection.Table<Product>().ToListAsync();
    }

    public async Task<List<Order>> GetAllOrders()
    {
        await Init();
        return await _connection.Table<Order>().ToListAsync();
    }

    public async Task<List<Order>> GetOrderByCustomerId(int customerId)
    {
        await Init();
        return await _connection.Table<Order>().Where(x => x.CustomerId == customerId).ToListAsync();
    }

    public async Task<List<OrderItem>> GetOrderDetail(int orderId)
    {
        await Init();
        return await _connection.Table<OrderItem>().Where(x => x.OrderId == orderId).ToListAsync();
    }

    public async Task<List<OrderItem>> GetOrderOfProduct(int productId)
    {
        await Init();
        return await _connection.Table<OrderItem>().Where(x => x.ProductId == productId).ToListAsync();
    }

    public async Task CreateCustomer(Customer customer)
    {
        await Init();
        await _connection.InsertAsync(customer);
    }

    public async Task CreateProduct(Product product)
    {
        await Init();
        await _connection.InsertAsync(product);
    }

    public async Task CreateOrder(Order order)
    {
        await Init();
        await _connection.InsertAsync(order);
    }

    public async Task CreateOrderItem(OrderItem orderItem)
    {
        await Init();
        await _connection.InsertAsync(orderItem);
    }
}