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
    public async Task<ActionResult<IEnumerable<Fleet>>> GetFleets()
    {
        var fleet = await _routeService.GetRouteAsync();
        return Ok(fleet);
    }
    
}