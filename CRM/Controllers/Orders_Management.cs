using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace CRM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetOrders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }
        private double GetCost(DeliveryType delivery_type,double value, double distance)
        {
            double delivery_cost = 0;
            switch (delivery_type)
            {
                case DeliveryType.Fast:
                    delivery_cost = 10;
                    break;
                case DeliveryType.Express:
                    delivery_cost = 20;
                    break;
                case DeliveryType.Standart:
                    delivery_cost = 5;
                    break;
            }
            return Math.Round(distance/10*value/10*(delivery_cost/100),2);
        }
        [HttpPost("Cancel_Order")]
        public async Task<ActionResult<Order>> CancelOrder(int order_id)
        {
            var orders = await _context.Orders.ToListAsync();
            var order = await _context.Orders.FindAsync(order_id);
            if (order != null)
            {
                order.Status = "Скасоване";
            }
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("Ordermake")]
        public async Task<ActionResult<Order>> MakeOrder(int customer_id, DeliveryType delivery_type, double value,double distance)
        {

            var order = new Order
            {
                DeliveryCost = GetCost(delivery_type,value,distance),
                DeliveryType = delivery_type,
                Status = "в процесі",
                Distance = distance,
                Value = value,
                Сustomer = customer_id
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return Ok(order);

        }

        [HttpGet("GetOrderStatus")]
        public async Task<ActionResult<string>> GetOrderStatus(int order_id)
        {
            var order = await _context.Orders.FindAsync(order_id);
            return order.Status;

        }
        [HttpDelete("KillDATABASE")]
        public async Task<ActionResult<Order>> KillData()
        {
            foreach (var o in _context.Orders)
            {
                _context.Orders.Remove(o);
            }

           await _context.SaveChangesAsync();
           return Ok();
        }

        [HttpGet("GetOrderByClientId")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderByClientId(int client_id)
        {
            return await _context.Orders.Where(x => x.Сustomer == client_id).ToListAsync();
        }
    }
}