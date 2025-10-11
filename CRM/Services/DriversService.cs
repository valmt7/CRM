using CRM.MidMiddleware;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace CRM.Services;

public class DriversService : IDriversService
{
    private readonly AppDbContext _context;
    private readonly IMailService _emailSender;
    const int DEFAULT_ORDER_ID = -1;
    const int DEFAULT_FLEET_ID = -1;
    const int DEFAULT_ROUTE_ID = -1;
    
    
    public DriversService(AppDbContext context, IMailService emailSender)
    {
        _context = context;
        _emailSender = emailSender;
    }
    public async Task<IEnumerable<Driver>> GetAllDrivers()
    {
        var drivers = await _context.Drivers.ToListAsync();
        if (drivers.Count == 0)
        {
            throw new NotFoundDriversExeption();
        }
        return drivers;
    }

    public async Task<Driver> CreateDriver(string firstName, string lastName,string email,string phoneNumber)
    {
        var driver = new Driver
        {
            Name = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            OrderId = DEFAULT_ORDER_ID,
            RouteId = DEFAULT_ROUTE_ID,
            FleetId = DEFAULT_FLEET_ID,
        };
        await _context.Drivers.AddAsync(driver);
        await _context.SaveChangesAsync();
        return driver;
    }
    public async Task<Driver> SetOrderDriver(int orderId, int driverId)
    {
        var order =  await _context.Orders.FindAsync(orderId);
        if (order == null)
        {
            throw new NotFoundOrderByIdException(orderId);
        }
        var driver = await _context.Drivers.FindAsync(driverId);
        if (driver == null)
        {
            throw new NotFoundDriversExeption();
        }
        driver.OrderId = orderId;
        await _context.SaveChangesAsync();
        var product = await _context.Products.FindAsync(order.ProductID);
        if (!string.IsNullOrEmpty(driver.Email))
        {
            await _emailSender.SendMail(
                driver.Email,
                $"New order #{order.Id}",
                "✅ You have a new order!\n" +
                "Order details:\n" +
                $"{product.Name}\n" +
                $"from {product.WarehouseLocation} - to {order.EndPoint}\n" +
                $"Don't forget to keep an eye on the technical condition of your car!!"
            );
        }
        return driver;
    }

    public async Task<Driver> SetDriverFleet(int driverId, int fleetId)
    {
        var driver = await _context.Drivers.FindAsync(driverId);
        if (driver == null)
        {
            throw new NotFoundDriversExeption();
        }
        driver.FleetId = fleetId;
        await _context.SaveChangesAsync();
        Console.WriteLine(driver.Email);
        if (!string.IsNullOrEmpty(driver.Email))
        {
            await _emailSender.SendMail(
                driver.Email,
                $"You have new fleet #{fleetId}",
                $"You fleet has been changed");
        }
        
        return driver;
    }

    public async Task<string> SendCriticalSituations(int driverId, int orderId, string situationType,
        string situationDetails)
    {
        var driver = await _context.Drivers.FindAsync(driverId);
        if (driver == null)
        {
            throw new NotFoundDriversExeption();
        }
        var managersEmail = _context.Managers.Select(m=>m.Email).ToList();
        if (managersEmail.Count != 0)
        {
            foreach (var managerEmail in managersEmail)
            {

                await _emailSender.SendMail(managerEmail,
                    $"Critical situations #{driverId}, type: {situationType}, orderId{orderId}",
                    $"Critical situation details: {situationDetails}");
            }
        }
        return "Success";
    }
}