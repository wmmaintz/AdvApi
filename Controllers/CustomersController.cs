using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace AdvApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApiContext _context;

        public CustomersController(ApiContext context)
        {
            _context = context;
        }

        /* Customer model
         * ===============
         * customer.Id 
         * customer.Email 
         * customer.Name  
         * customer.State 
         * customer.Orders
         */

        /// <summary>
        /// GET: api/customers
        /// </summary>
        /// <returns>List of Customer Objects</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            IList<Customer> customers = await _context.Customers.ToListAsync();
            return Ok(customers.OrderBy(c => c.Name));
        }

        /// <summary>
        /// GET: api/customers/name@domain.ext
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Customer Object</returns>
        [HttpGet("{email}")]
        public async Task<ActionResult<Customer>> GetCustomer(string email)
        {
            Customer customer = await _context.Customers
                // .Where(c => EF.Functions.Collate(c.Email, "SQL_Latin1_Generatl_CP1_CS_AS"))
                .FindAsync(email.ToLower());

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        /// <summary>
        /// PUT: api/customers/name@domain.ext
        /// Change the customer object identified by the first parameter
        /// with the values in the provided Customer object
        /// To protect from overposting attacks, enable the specific properties you want to bind to, for
        /// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="customer"></param>
        /// <returns>Customer Object</returns>
        [HttpPut("{email}")]
        public async Task<IActionResult> PutCustomer([FromBody] Customer customer)
        {
            if (   (customer.Email == null)
                || (
                    (customer.Email != null)
                 && (customer.Name  == null)
                 && (customer.State == null)
                 && (customer.Orders == null)
                   )
            )
            {
                return BadRequest();
            }

            Customer newCustomer = await _context.Customers.FindAsync(customer.Email.ToLower());
            if (customer.Email == null) { return NotFound(); }
            if (customer.Name != null) { newCustomer.Name = customer.Name; }
            if (customer.State != null) { newCustomer.State = customer.State; }
            if (customer.Orders != null) { newCustomer.Orders = customer.Orders; }
            customer = newCustomer;

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(customer.Email))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(customer);
        }


        /// <summary>
        /// POST: api/customers
        /// Add a new customer to the database
        /// To protect from overposting attacks, enable the specific properties you want to bind to, for
        /// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            if ( customer.Email == null)
            {
                return BadRequest();
            }
            Customer newCustomer = new Customer();
            newCustomer.Email = customer.Email.ToLower();
            newCustomer.Name = customer.Name;
            newCustomer.State = customer.State;
            newCustomer.Orders = customer.Orders;
            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { email = newCustomer.Email }, newCustomer);
        }

        /// <summary>
        /// DELETE: api/customers/name@domain.ext
        /// Delete the customer identified by the supplied email address
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Status Code</returns>
        [HttpDelete("{email}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(string email)
        {
            if (email == null)
            {
                return BadRequest();
            }
            var customer = new Customer { Email = email };
            try
            {
                _context.Customers.Attach(customer);
                if (customer.Orders != null)
                {
                    return BadRequest("Customer has order records on file.");
                }
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
            catch (DbException)
            {
                return NotFound();
            }

            return Ok();
        }

        private bool CustomerExists(string email)
        {
            return _context.Customers.Any(e => e.Email == email.ToLower());
        }
    }
}
