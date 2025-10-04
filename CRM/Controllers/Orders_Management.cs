using CRM;
using CRM.Services;
using Microsoft.AspNetCore.Mvc;


namespace CRM.Controllers
{
    [ApiController]
    [Route("api/")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("orders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return Ok(await _orderService.GetOrdersAsync());
        }

        [HttpPost("orders")]
        public async Task<ActionResult<Order>> MakeOrder(int customerId, DeliveryType deliveryType, double value, double distance,int productId)
        {
            var order = await _orderService.MakeOrderAsync(customerId, deliveryType, value, distance,productId);
            return Ok(order);
        }

        [HttpPatch("orders/cancel")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            await _orderService.CancelOrderAsync(orderId);
            return Ok();
        }

        [HttpGet("orders/status")]
        public async Task<ActionResult<string>> GetOrderStatus(int orderId)
        {
            return Ok(await _orderService.GetOrderStatusAsync(orderId));
        }

        [HttpGet("orders/client")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderByClientId(int clientId)
        {
            return Ok(await _orderService.GetOrdersByClientIdAsync(clientId));
        }

        [HttpDelete("KillDATABASE")]
        [HttpDelete("KillDATABASE")]
        public async Task<IActionResult> KillData()
        {
            await _orderService.KillDataAsync();
            return Ok();
        }
        
    }
}