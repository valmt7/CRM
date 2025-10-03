using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRM;
namespace CRM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        private readonly AppDbContext _context;

        public ClientController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("GetClient")]
        public async Task<ActionResult<IEnumerable<Client>>> GetOrders()
        {
            return await _context.Clients.ToListAsync();
        }

        [HttpGet("CreateClient")]
        public async Task<ActionResult<IEnumerable<Client>>> MakeClient(string name,string phone,string email, string dostup)
        {
            var client = new Client
            {
                Name = name,
                Email = email,
                Dostup = dostup,
                Phone = phone,
                Likely = new List<string>( ){"None"}
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return Ok(client);
        }
    }
}
