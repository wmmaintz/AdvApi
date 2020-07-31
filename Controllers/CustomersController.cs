using Entities;
using Entities.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using static Entities.Configuration.Utils;
using static Entities.Repositories.CustomerRepository;

namespace AdvApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly ApiContext _ctx;
        private readonly ILogger<CustomersController> _logger;
        private readonly CustomerRepository _repo;

        public CustomersController(DbContextOptions<ApiContext> options, ILogger<CustomersController> logger)
        {
            ApiContext ctx = new ApiContext(options);
            CustomerRepository repo = new CustomerRepository(ctx);
            _ctx = ctx;
            _repo = repo;
            _logger = logger;
        }

        /// <summary>
        /// GET: api/customers
        /// </summary>
        /// <returns>List of Customer Objects</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            IEnumerable<Customer> customers = await _repo.GetAllCustomersAsync();
            if (customers == null)
            {
                return NotFound();
            }
            return Ok(customers);
        }

        /// <summary>
        /// GET: api/customers/orders
        /// </summary>
        /// <returns>List of Customer Objects</returns>
        [HttpGet("orders")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomersWOrders()
        {
            IEnumerable<Customer> customers = await _repo.GetCustomersWOrdersAsync();
            if (customers == null)
            {
                return NotFound();
            }
            return Ok(customers);
        }


        /// <summary>
        /// GET: api/customers/orders
        /// </summary>
        /// <returns>List of Customer Objects</returns>
        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomersByName(string name)
        {
            if (name == null)
            {
                return BadRequest("ERROR: A customer name was not provided");
            }
            IEnumerable<Customer> customers = await _repo.GetCustomersByNameAsync(name);
            if (customers == null)
            {
                return NotFound("A customer similar to " + name + " was not found");
            }
            return Ok(customers);
        }

        /// <summary>
        /// GET: api/customers/name@domain.ext
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Customer Object</returns>
        [HttpGet("{email}")]
        public async Task<ActionResult<Customer>> GetCustomerByEmail(string email)
        {
            if (email == null)
            {
                return BadRequest("ERROR: Customer email is invalid");
            }
            Customer customer = await _repo.GetCustomerByEmailAsync(email);

            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
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
        [HttpPut]
        public async Task<ActionResult<Customer>> UpdateCustomer([FromBody] Customer chgdCustomer)
        {
            if (chgdCustomer.Email == null)
            {
                return BadRequest("ERROR: Customer email is invalid");
            }

            CustomerResponse cr = await _repo.UpdateCustomerAsync(chgdCustomer);

            if (cr.CrResult == null)
            {
                return Ok(cr.CrCustomer);
            }
            return cr.CrResult.Substring(0, 7) switch
            {
                "NullEma" => BadRequest("Invalid customer email"),
                "NotFoun" => Problem("ERROR: Customer not found"),
                _ => Problem(cr.CrResult)
            };
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
        public async Task<ActionResult<Customer>> CreateCustomer(Customer newCustomer)
        {
            if (newCustomer.Email == null)
            {
                return BadRequest("ERROR: Customer email is invalid");
            }

            CustomerResponse cr = await _repo.CreateCustomerAsync(newCustomer);

            if (cr.CrResult == null)
            {
                return Ok(cr.CrCustomer);
                // _ => Ok(CreatedAtAction("GetCustomer", new { email = customer.Email }, customer)),
            }
            return cr.CrResult.Substring(0,6) switch
            {
                "NullId" => BadRequest("Invalid customer email"),
                "Custom" => Problem("ERROR: Customer existed before insertion"),
                _ => Problem(cr.CrResult)
            };
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
                return BadRequest("ERROR: Customer Email is invalid");
            }

            CustomerResponse cr = await _repo.DeleteCustomerAsync(email);

            if (cr.CrResult == null)
            {
                return Ok();
            }
            return cr.CrResult.Substring(0, 6) switch
            {
                "NullId" => BadRequest("Invalid customer email"),
                "NotFou" => Problem("ERROR: Customer email not found"),
                _ => Problem(cr.CrResult)
            };
        }

    }
}
