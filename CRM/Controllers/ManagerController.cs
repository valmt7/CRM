using CRM.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [ApiController]
    [Route("api/managers")]
    public class ManagerController : Controller
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manager>>> GetManagers()
        {
            return Ok(await _managerService.GetAllManagers());
        }

        [HttpPost]
        public async Task<ActionResult<Manager>> AddManager(string managerName, string managerEmail, string managerLastName,
            string managerPhone)
        {
            var manager = await _managerService.AddManager(managerName, managerEmail, managerLastName, managerPhone);
            return Created(Url.Link("managers",new {managerId = manager.Id}), manager);
        }

    }
}