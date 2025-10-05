namespace CRM.Services;

public interface IRouteService
{
    Task<IEnumerable<Route>> GetRouteAsync();
}