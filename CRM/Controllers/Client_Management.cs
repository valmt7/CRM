using CRM;
using CRM.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace CRM.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetClient()
        {
            var clients = await _clientService.GetClientAsync();
            return Ok(clients);
        }

        [HttpPost]
       public async Task<IActionResult> MakeClient(string name, string phone, string email, string dostup)
       {
           return Ok(await _clientService.MakeClient(name, phone, email, dostup));
       }
        [HttpPatch("access")]
        public async Task<IActionResult> SetClientAccess(int id, string access)
        {
            var client = await _clientService.SetClientAccess(id, access);
            return Ok(client);
        }

        [HttpGet("offers")]
        public async Task<IActionResult> GetOffers(int clientId)
        {
            var client = await _clientService.GetOffers(clientId);
            return Ok(client);
        }
        
        [HttpDelete("database")]
        public async Task<IActionResult> KillData()
        {
            await _clientService.KillDataAsync();
            return Ok();
        }

    }
}
