namespace CRM.Services;

public interface IRouteService
{
    Task<IEnumerable<Route>> GetRouteAsync();
    Task<Route> CreateRouteAsync(string startLocation, string endLocation);
}