using CRM.MidMiddleware;
using CRM.Services;
using Microsoft.AspNetCore.Mvc;
namespace CRM.Controllers
{
    [ApiController]
    [Route("api/reports")]

    public class ReportController : Controller
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            var reports = await _reportService.GetAllReportsAsync();
            return Ok(reports);
        }

        [HttpGet("avgtime")]
        public async Task<IActionResult> GetAverageDeliveryTime(int clientId, string filter)
        {
            DateTime now = DateTime.Now;
            return Ok(await _reportService.GetAverageDeliveryTime(clientId, filter, now));
        }

        [HttpGet("{clientId}")]
        public async Task<IActionResult> GetReport(int clientId)
        {
            var reports = await _reportService.GetAllClientReports(clientId);
            return Ok(reports);
        }
        
    }
}
