using Microsoft.EntityFrameworkCore;

namespace CRM.Services.Admin;
using CRM.MidMiddleware;
public class AdminService : IAdminService
{
    private readonly AppDbContext _context;
    private readonly IMailService _emailSender;

    public AdminService(AppDbContext context,  IMailService emailSender)
    {
        _context = context;
        _emailSender = emailSender;
    }
     public async Task<Order> SuccessOrder(int orderId)
        {
            DateTime endTime = DateTime.Now;
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new NotFoundOrderByIdException(orderId);
            }
            order.Status = "Success";  
            order.EndTime = endTime;
            TimeSpan timeSpan = endTime - order.StartTime;
            var drivers = await _context.Drivers.Where(x=>x.OrderId == orderId).ToListAsync();
            if (drivers.Count==0)
            {
                throw new NotFoundDriversExeption();
            }
            var driver = drivers[0];
            var fleet = await _context.Fleets.FindAsync(driver.FleetId);
            if (fleet == null)
            {
                throw new NotFoundFleetsExeption();
            }
            
            var product = await _context.Products.FindAsync(order.ProductID);
            if (product == null)
            {
                throw new NotFoundPrductsExeption();
            }
            var client = await _context.Clients.FindAsync(order.Сustomer);
            if (client == null)
            {
                throw new NotFoundClientsExeption();
            }
            Report report = new Report
            {
                DeliveryTime = Math.Round(Convert.ToDouble(timeSpan.TotalHours),2),
                FleetStatus = fleet.State,
                RouteId = driver.RouteId,
                Spending = order.DeliveryCost,
                Profit = product.Price - order.DeliveryCost,
                CreateDate = endTime,
                ClientId = order.Сustomer,
            };
            driver.OrderId = -1;
            driver.RouteId = -1;
           
            if (!string.IsNullOrEmpty(client.Email))
            {
                await _emailSender.SendMail(
                    client.Email,
                    $"Order status update #{order.Id}",
                    "✅ Your order status has been changed!\n" +
                    $"New order status: Success\n"
                );
                
            }
            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();
            return order;
        }
}