namespace CRM.Services;

public interface IDriversService
{
    Task<IEnumerable<Driver>> GetAllDrivers();
    Task<Driver> CreateDriver(string name,string lastName, string phoneNumber,string email);
    Task<Driver> SetOrderDriver(int orderId, int driverId);
    Task<Driver> SetDriverFleet(int driverId, int fleetId);

    Task<string> SendCriticalSituations(int driverId, int orderId, string situationType,
        string situationDetails);
}