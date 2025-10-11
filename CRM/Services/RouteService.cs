using CRM.MidMiddleware;
using Microsoft.EntityFrameworkCore;
namespace CRM.Services;
using DotNetEnv;
using System.Net.Http;
using Newtonsoft.Json.Linq;

public class RouteService : IRouteService
{

    private readonly AppDbContext _context;
    private readonly string apiKeyDirection;
    private readonly string apiKeyGeocoding;
  
    public RouteService(AppDbContext context,IConfiguration config)
    {
        _context = context;
        apiKeyDirection = config["GOOGLE_MAPS_DIRECTION_API_KEY"];
        apiKeyGeocoding = config["GOOGLE_MAPS_GEOCODE_API_KEY"];

    }

    public async Task<IEnumerable<Route>> GetRouteAsync()
    {

        var routes = await _context.Routes.ToListAsync();
        if (routes.Count == 0)
        {
            throw new NotFoundRoutesExeption();
        }

        return routes;
    }

    public async Task<Route> CreateRouteAsync(string startLocation, string endLocation)
    {
    string url = $"https://maps.googleapis.com/maps/api/directions/json?" +
                 $"origin={Uri.EscapeDataString(startLocation)}&" +
                 $"destination={Uri.EscapeDataString(endLocation)}&" +
                 $"key={apiKeyDirection}&language=uk";

    using HttpClient client = new HttpClient();
    string response = await client.GetStringAsync(url);
    JObject json = JObject.Parse(response);
    string status = (string)json["status"];
    Console.WriteLine(url);
    Console.WriteLine(response);
    if (status != "OK")
        throw new Exception($"Маршрут не знайдено. Status: {status}");
    
    if (json["routes"]?.HasValues != true)
        throw new NotFoundRoutesExeption();

    var steps = json["routes"][0]["legs"][0]["steps"];
    var towns = new List<string>();
    string lastTown = "";

    foreach (var step in steps)
    {
        string lat = (string)step["start_location"]["lat"];
        string lng = (string)step["start_location"]["lng"];
        string geoUrl = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={lat},{lng}&key={apiKeyGeocoding}&language=uk";

        string geoResp = await client.GetStringAsync(geoUrl);
        JObject geoJson = JObject.Parse(geoResp);

        var addressComponents = geoJson["results"]?[0]?["address_components"];
        if (addressComponents != null)
        {
            foreach (var comp in addressComponents)
            {
                var types = comp["types"];
                if (types != null && types.ToString().Contains("locality"))
                {
                    string town = (string)comp["long_name"];
                    if (!string.IsNullOrEmpty(town) && town != lastTown)
                    {
                        towns.Add(town);
                        lastTown = town;
                    }
                }
            }
        }
    }
    
    if (!towns.Contains(startLocation))
        towns.Insert(0, startLocation);

    if (!towns.Contains(endLocation))
        towns.Add(endLocation);

    var route = new Route
    {
        Locations = towns.Distinct().ToList()
    };

    _context.Routes.Add(route);
    await _context.SaveChangesAsync();

    return route;
}
}


