
using Microsoft.EntityFrameworkCore;



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
            var orders = await _context.Orders.ToListAsync();
            if (orders.Count == 0)
            {
                return null;
            }
            return orders; ;
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
            

            var likelySet = client.Likely != null ? new HashSet<string>(client.Likely) : new HashSet<string>();
            var offersSet = client.Offers != null ? new HashSet<int>(client.Offers) : new HashSet<int>();
            
            likelySet.Add(productType);
            var productIds = _context.Products
                .Where(p => p.Type == productType)
                .Select(p => p.Id).Take(TAKE_PRODUCT_COUNT_FILTER)
                .ToList();
            offersSet.UnionWith(productIds);
            client.Likely = likelySet.ToList();
            client.Offers = offersSet.ToList();
            
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<Order> CancelOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return null;
            }
            order.Status = "Скасоване";
            await _context.SaveChangesAsync();
            return order;

           
        }

        public async Task<string> GetOrderStatusAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return null;
            }
            return order?.Status;
        }

        public async Task<IEnumerable<Order>> GetOrdersByClientIdAsync(int clientId)
        {
            var order = await _context.Orders
                .Where(x => x.Сustomer == clientId)
                .ToListAsync();
            if (order == null)
            {
                return null;
            }
            return order;
        }

        public async Task KillDataAsync()
        {
            _context.Orders.RemoveRange(_context.Orders);
            await _context.SaveChangesAsync();
        }
        
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
