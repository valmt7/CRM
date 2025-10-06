using CRM;
using CRM.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.Controllers
{
    [ApiController]
    [Route("api/fleets")]
    public class FleetController : Controller
    {
        private readonly IFleetService _fleetService;

        public FleetController(IFleetService fleetService)
        {
            _fleetService = fleetService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fleet>>> GetFleets()
        {
            var fleet = await _fleetService.GetFleetAsync();
            if (fleet == null)
            {
                return NotFound();
            }
            return Ok(fleet);
            
        }

        [HttpPost]
        public async Task<ActionResult<Fleet>> CreateFleet(string name, string location)
        {
            var fleet = await _fleetService.CreateFleet(name, location);
            return Ok(fleet);
        }

        [HttpPatch("service")]
        public async Task<ActionResult<Fleet>> UpdateFleetService(int id, int state)
        {
            var fleet = await _fleetService.UpdateFleetState(id, state);
            if (fleet == null)
            {
                return NotFound();
            }
            return Ok(fleet);
        }

        [HttpGet("order")]
        public async Task<ActionResult<Fleet>> SetFleetOrder(int id, int order)
        {
            var fleet = await _fleetService.SetFleetOrder(id, order);
            if (fleet == null)
            {
                return NotFound();
            }
            return Ok(fleet);
        }

        [HttpGet("location")]
        public async Task<ActionResult<string>> SetFleetLocation(int id)
        {
            var fleet = await _fleetService.GetFleetlocation(id);
            if (fleet == null)
            {
                return NotFound();
            }
            return Ok(fleet);
        }

        [HttpPatch("location")]
        public async Task<ActionResult<Fleet>> UpdateFleetLocation(int id, string location)
        {
            var fleet = await _fleetService.UpdateFleetLocation(id, location);
            if (fleet == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet("route")]
        public async Task<ActionResult<string>> SetFleetRoute(int id, int route)
        {
            var fleet = await _fleetService.SetFleetRoute(id, route);
            if (fleet == null)
            {
                return NotFound();
            }
            return Ok(fleet);
        }
    }
}