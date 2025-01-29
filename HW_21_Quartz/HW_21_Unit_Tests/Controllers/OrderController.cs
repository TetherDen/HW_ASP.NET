using HW_21_Unit_Tests.Interfaces;
using HW_21_Unit_Tests.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW_21_Unit_Tests.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrder _orders;
        public OrderController(IOrder orders)
        {
            _orders = orders;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders()
        {
            var orders = _orders.GetAllOrders();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = _orders.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public ActionResult<Order> CreateOrder(Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            var createdOrder = _orders.CreateOrder(order);
            return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(int id)
        {
            var result = _orders.DeleteOrder(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
