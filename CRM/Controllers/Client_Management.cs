using CRM;
using CRM.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace CRM.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet("clients")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return Ok(await _clientService.GetClientAsync());
        }

        [HttpPost("clients")]
       public async Task<IActionResult> MakeClient(string name, string phone, string email, string dostup)
       {
           return Ok(await _clientService.MakeClient(name, phone, email, dostup));
       }
        [HttpPatch("clients/access")]
        public async Task<IActionResult> SetClientAccess(int id, string access)
        {
            return Ok(await _clientService.SetClientAccess(id, access));
        }

        [HttpGet("clients/offers")]
        public async Task<IActionResult> GetOffers(int clientId)
        {
            return Ok(await _clientService.GetOffers(clientId));
        }
    }
}
