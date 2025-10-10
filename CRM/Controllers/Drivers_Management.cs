using CRM.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [ApiController]
    [Route("api/drivers")]
    public class DriversController : Controller
    {
        private readonly IDriversService _driversService;

        public DriversController(IDriversService driversService)
        {
            _driversService = driversService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Driver>>> GetDrivers()
        {
            return Ok(await _driversService.GetAllDrivers());
        }

        [HttpPost]
        public async Task<ActionResult<Driver>> CreateDriver(string name, string lastName, string phoneNumber,
            string email)
        {
            return Ok(await _driversService.CreateDriver(name, lastName, phoneNumber, email));
        }
        [HttpPatch("order")]

        public async Task<ActionResult<Driver>> SetOrderDriver(int orderId, int driverId)
        {
            return Ok(await _driversService.SetOrderDriver(orderId, driverId));
        }
      
    }
}