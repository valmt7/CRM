using CRM.Services.Admin;
using Microsoft.AspNetCore.Mvc;
namespace CRM.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("success")]
        public async Task<IActionResult> SuccessOrder(int orderId)
        {
            return Ok(await _adminService.SuccessOrder(orderId));
        }

    }
}