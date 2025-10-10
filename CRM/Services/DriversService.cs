using CRM.MidMiddleware;
using Microsoft.EntityFrameworkCore;

namespace CRM.Services;

public class DriversService : IDriversService
{
    private readonly AppDbContext _context;
    const int DEFAULT_ORDER_ID = -1;
    const int DEFAULT_FLEET_ID = -1;
    const int DEFAULT_ROUTE_ID = -1;
    
    
    public DriversService(AppDbContext context)
    {
        _context = context;
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
        return driver;
    }
}