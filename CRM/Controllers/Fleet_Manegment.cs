using CRM;
using CRM.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.Controllers
{
    [ApiController]
    [Route("api/")]
    public class FleetController : Controller
    {
        private readonly IFleetService _fleetService;

        public FleetController(IFleetService fleetService)
        {
            _fleetService = fleetService;
        }

        [HttpGet("fleets")]
        public async Task<ActionResult<IEnumerable<Fleet>>> GetFleets()
        {
            return Ok(await _fleetService.GetFleetAsync());
        }

        [HttpPost("fleets")]
        public async Task<ActionResult<Fleet>> CreateFleet(string name, string location)
        {
            return Ok(await _fleetService.CreateFleet(name, location));
        }

        [HttpPatch("fleet/service")]
        public async Task<ActionResult<Fleet>> UpdateFleetService(int id, int state)
        {
            return Ok(await _fleetService.UpdateFleetState(id,state));
        }

        [HttpGet("fleet/order")]
        public async Task<ActionResult<Fleet>> SetFleetOrder(int id, int order)
        {
            return Ok(await _fleetService.SetFleetOrder(id, order));
        }

        [HttpGet("fleet/location")]
        public async Task<ActionResult<string>> SetFleetLocation(int id)
        {
            return (await _fleetService.GetFleetlocation(id));
        }

        [HttpPatch("fleet/location")]
        public async Task<ActionResult<Fleet>> UpdateFleetLocation(int id, string location)
        {
            return Ok(await _fleetService.UpdateFleetLocation(id, location));
        }

        [HttpGet("fleet/route")]
        public async Task<ActionResult<string>> SetFleetRoute(int id, int route)
        {
            return Ok(await _fleetService.SetFleetRoute(id, route));
        }
    }
}