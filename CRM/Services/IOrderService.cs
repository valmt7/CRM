

namespace CRM.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<Order> MakeOrderAsync(int customerId, DeliveryType deliveryType, double value, double distance,int productId);
        Task<Order> CancelOrderAsync(int orderId);
        Task<string> GetOrderStatusAsync(int orderId);
        Task<IEnumerable<Order>> GetOrdersByClientIdAsync(int clientId);
        Task KillDataAsync();
    }
}