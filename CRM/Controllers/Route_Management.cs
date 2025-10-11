using CRM.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers;
[ApiController]
[Route("api/routes")]

public class RouteController : Controller
{
    private readonly IRouteService _routeService;

    public RouteController(IRouteService routeService)
    {
        _routeService = routeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Route>>> GetFleets()
    {
        var route = await _routeService.GetRouteAsync();
        return Ok(route);
    }

    [HttpPost]
    public async Task<ActionResult<Route>> CreateRouteAsync(string startLocation, string endLocation)
    {
        return Ok(await _routeService.CreateRouteAsync(startLocation, endLocation));
    }
    
    
}