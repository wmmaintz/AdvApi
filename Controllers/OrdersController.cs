using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApiContext _context;

        public OrdersController(ApiContext context)
        {
            _context = context;
        }

        /* Order model
         * ===================
         * Id
         * Customer
         * Total
         * Placed
         * Completed
         * Status
         */

        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders
                .Include(c => c.Customer)
                .ToListAsync();

        }

        // GET: api/orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/orders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, [FromBody] Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            // Validate that the order exists
            Order order2Validate = await _context.Orders.FindAsync(id);
            if (order2Validate == null) { return BadRequest("ERROR: Order does not exist!"); }
            // var customer = await _context.Customers.FindAsync(order.Customer.Email);
            // if (customer == null) { return BadRequest("ERROR: Customer does not exist!"); }

            // Order and customer exist - proceed

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Order ID#" + order.Id + " updated.");
        }

        // POST: api/orders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            Order newOrder = new Order();
            newOrder.Id = order.Id;
            newOrder.Total = order.Total;
            newOrder.Placed = order.Placed;
            newOrder.Completed = order.Completed;
            newOrder.Status = order.Status;
            newOrder.Customer = order.Customer;
            newOrder.CustomerEmail = order.CustomerEmail;
            var customer = await _context.Customers.FindAsync(order.Customer.Email);
            if (customer == null) { return BadRequest("ERROR: Customer does not exist!"); }
            newOrder.Customer = customer;
            order = newOrder;

            try
            {
                _context.Orders.Add(order);
                // or
                // _context.Entry(order).State = EntityState.Added;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(order.Id))
                {
                    return Problem("ERROR: Order not created.");
                }
                else
                {
                    return Problem("ERROR: Database not updated.");
                }
            }

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            try
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return Problem("ERROR: Database not updated.");
                }
                else
                {
                    return Problem("ERROR: Order not deleted.");
                }
            }

            return Ok("Order ID#" + order.Id + " deleted.");
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
