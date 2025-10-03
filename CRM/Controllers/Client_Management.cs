using CRM;
using CRM.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace CRM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet("GetClient")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return Ok(await _clientService.GetClientAsync());
        }

        [HttpGet("CreateClient")]
       public async Task<IActionResult> MakeClient(string name, string phone, string email, string dostup)
       {
           return Ok(await _clientService.MakeClient(name, phone, email, dostup));
       }
    }
}
