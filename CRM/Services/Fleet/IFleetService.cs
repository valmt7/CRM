namespace CRM.Services;

public interface IFleetService
{
    Task<IEnumerable<Fleet>> GetFleetAsync();
    Task<Fleet> CreateFleet(string name,string location);

    Task<Fleet> UpdateFleetState(int id, int state);
    Task<Fleet> SetFleetOrder(int id, int order);

    Task<string> GetFleetlocation(int id);
    Task<Fleet> UpdateFleetLocation(int id, string location);

    Task<Fleet> SetFleetRoute(int id, int route);


}