using CRM.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers;
[ApiController]
[Route("api/")]

public class RouteController : Controller
{
    private readonly IRouteService _routeService;

    public RouteController(IRouteService routeService)
    {
        _routeService = routeService;
    }

    [HttpGet("routes")]
    public async Task<ActionResult<IEnumerable<Fleet>>> GetFleets()
    {
        return Ok(await _routeService.GetRouteAsync());
    }
    
}