using CRM.MidMiddleware;
using Microsoft.EntityFrameworkCore;
using CRM.Services;




namespace CRM.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly IMailService _emailSender;
        
        const int TAKE_PRODUCT_COUNT_FILTER = 10;
        public OrderService(AppDbContext context, IMailService mailService)
        {
            _context = context;
            _emailSender = mailService;
        }


        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            var orders = await _context.Orders.ToListAsync();
            if (orders.Count == 0)
            {
                throw new NotFoundOrdersException();
            }

            return orders;


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
                ProductID = productId
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
            product = await _context.Products.FindAsync(productId);
            await _context.SaveChangesAsync();
            Console.WriteLine(client.Email);
            if (!string.IsNullOrEmpty(client.Email))
            {
                await _emailSender.SendMail(
                    client.Email,
                    $"New order #{order.Id}",
                    "✅ Your order has been successfully placed!\n" +
                    "Order details:\n" +
                    $"{product.Name}\n" +
                    $"Price: {product.Price}$\n"
                );
            }
            return order;
        }

        public async Task<Order> CancelOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = "Скасоване";
                await _context.SaveChangesAsync();
            }
            return order;
        }

        public async Task<string> GetOrderStatusAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                return order.Status;
            }
            throw new NotFoundOrderByIdException(orderId); 
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

        public async Task<Order> SetOrderStatus(int orderId, string status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            var client = await _context.Clients.FindAsync(order.Сustomer);
            order.Status = status;
            await _context.SaveChangesAsync();
            if (!string.IsNullOrEmpty(client.Email))
            {
                await _emailSender.SendMail(
                    client.Email,
                    $"Order status update #{order.Id}",
                    "✅ Your order status has been changed!\n" +
                    $"New order status: {order.Status}\n"
                );
                
            }
         
            return order;
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
