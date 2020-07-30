using Entities;
using Entities.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Entities.Repositories.OrderRepository;

namespace AdvApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApiContext _ctx;
        private readonly ILogger<CustomersController> _logger;
        private readonly OrderRepository _repo;

        public OrdersController(DbContextOptions<ApiContext> options, ILogger<CustomersController> logger)
        {
            ApiContext ctx = new ApiContext(options);
            OrderRepository repo = new OrderRepository(ctx);
            _ctx = ctx;
            _logger = logger;
            _repo = repo;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {

            IEnumerable<Order> orders = await _repo.GetAllOrdersAsync();
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }

        // GET: api/orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            if (id == 0)
            {
                return BadRequest("ERROR: Order ID is invalid");
            }

            Order order = await _ctx.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        // PUT: api/orders
        [HttpPut]
        public async Task<ActionResult<OrderResponse>> UpdateOrder([FromBody] Order chgdOrder)
        {
            if (chgdOrder.Id == 0)
            {
                return BadRequest("ERROR: Order ID is invalid");
            }

            OrderResponse or = await _repo.UpdateOrderAsync(chgdOrder);

            if (or.OrResult == null)
            {
                return Ok(or.OrOrder);
            }
            return or.OrResult.Substring(0, 7) switch
            {
                "NullEma" => BadRequest("Invalid order id"),
                "NotFoun" => Problem("ERROR: Order not found"),
                _ => Problem(or.OrResult)
            };
        }

        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult<OrderResponse>> CreateOrder([FromBody] Order newOrder)
        {
            if (newOrder.Id == 0)
            {
                return BadRequest("ERROR: Order ID is invalid");
            }

            // To protect from overposting attacks, enable the specific properties you want to bind to, for
            // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
            Order order = new Order {
                Id = newOrder.Id,
                Total = newOrder.Total,
                Placed = newOrder.Placed,
                Completed = newOrder.Completed,
                Status = newOrder.Status,
                CustomerEmail = newOrder.CustomerEmail
            };

            OrderResponse or = await _repo.CreateOrderAsync(newOrder);

            if (or.OrResult == null)
            {
                return Ok(or.OrOrder);
                // _ => Ok(CreatedAtAction("GetOrder", new { id = order.Id }, order));
            }
            return or.OrResult.Substring(0, 7) switch
            {
                "NullEma" => BadRequest("Invalid customer email"),
                "Custome" => Problem("ERROR: Customer existed before insertion"),
                _ => Problem(or.OrResult)
            };
        }

        // DELETE: api/orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            if (id == 0)
            {
                return BadRequest("ERROR: Order ID is invalid");
            }

            OrderResponse or = await _repo.DeleteOrderAsync(id);

            if (or.OrResult == null)
            {
                return Ok(or.OrOrder);
            }
            return or.OrResult.Substring(0, 7) switch
            {
                "NullEma" => BadRequest("Invalid customer email"),
                _ => Problem(or.OrResult)
            };
        }

    }
}
