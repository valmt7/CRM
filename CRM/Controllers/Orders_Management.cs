using CRM;
using CRM.Services;
using Microsoft.AspNetCore.Mvc;


namespace CRM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetOrders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return Ok(await _orderService.GetOrdersAsync());
        }

        [HttpPost("Ordermake")]
        public async Task<ActionResult<Order>> MakeOrder(int customerId, DeliveryType deliveryType, double value, double distance)
        {
            var order = await _orderService.MakeOrderAsync(customerId, deliveryType, value, distance);
            return Ok(order);
        }

        [HttpPost("Cancel_Order")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            await _orderService.CancelOrderAsync(orderId);
            return Ok();
        }

        [HttpGet("GetOrderStatus")]
        public async Task<ActionResult<string>> GetOrderStatus(int orderId)
        {
            return Ok(await _orderService.GetOrderStatusAsync(orderId));
        }

        [HttpGet("GetOrderByClientId")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderByClientId(int clientId)
        {
            return Ok(await _orderService.GetOrdersByClientIdAsync(clientId));
        }

        [HttpDelete("KillDATABASE")]
        public async Task<IActionResult> KillData()
        {
            await _orderService.KillDataAsync();
            return Ok();
        }
    }
}