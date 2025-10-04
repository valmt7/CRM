using CRM;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace CRM.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        const int TAKE_PRODUCT_COUNT_FILTER = 10;
        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> MakeOrderAsync(int customerId, DeliveryType deliveryType, double value, double distance, int productId)
        {
            var order = new Order
            {
                DeliveryCost = GetCost(deliveryType, value, distance),
                DeliveryType = deliveryType,
                Status = "в процесі",
                Distance = distance,
                Value = value,
                Сustomer = customerId,
                Product_ID = productId
            };
            _context.Orders.Add(order);
            var client = await  _context.Clients.FindAsync(customerId);
            var product = await _context.Products.FindAsync(productId);
            var productType = product.Type;
            client.Likely.Add(productType);
            var list = _context.Products.Where(x => x.Type == productType).Take(TAKE_PRODUCT_COUNT_FILTER).ToList();
            client.Offers.AddRange(list);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task CancelOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = "Скасоване";
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> GetOrderStatusAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            return order?.Status;
        }

        public async Task<IEnumerable<Order>> GetOrdersByClientIdAsync(int clientId)
        {
            return await _context.Orders
                                 .Where(x => x.Сustomer == clientId)
                                 .ToListAsync();
        }

        public async Task KillDataAsync()
        {
            _context.Orders.RemoveRange(_context.Orders);
            await _context.SaveChangesAsync();
        }

        // Вспомогательный метод для расчёта стоимости доставки
        private double GetCost(DeliveryType deliveryType, double value, double distance)
        {
            double deliveryCost = deliveryType switch
            {
                DeliveryType.Fast => 10,
                DeliveryType.Express => 20,
                DeliveryType.Standart => 5,
                _ => 0
            };
            return Math.Round(distance / 10 * value / 10 * (deliveryCost / 100), 2);
        }
    }
}
