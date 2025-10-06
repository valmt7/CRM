using CRM.MidMiddleware;
using CRM.Services;
using Microsoft.AspNetCore.Mvc;


namespace CRM.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _orderService.GetOrdersAsync();
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> MakeOrder(int customerId, DeliveryType deliveryType, double value, double distance,int productId)
        {
            var order = await _orderService.MakeOrderAsync(customerId, deliveryType, value, distance,productId);
            return Ok(order);
        }

        [HttpPatch("cancel")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var order = await _orderService.CancelOrderAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet("status")]
        public async Task<ActionResult<string>> GetOrderStatus(int orderId)
        {
            var status = await _orderService.GetOrderStatusAsync(orderId);
            return Ok(status);
        }

        [HttpGet("client")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderByClientId(int clientId)
        {
            var order = await _orderService.GetOrdersByClientIdAsync(clientId);
            return Ok(order);
        }
        
        [HttpDelete("database")]

        public async Task<IActionResult> KillData()
        {
            await _orderService.KillDataAsync();
            return Ok();
        }
        
    }
}