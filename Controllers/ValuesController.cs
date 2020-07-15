using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdvApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: Values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new string[] { "value1", "value2" });
        }

        // GET Values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0) return BadRequest("ERROR: id not provided");
            return Ok("Get id#" + id);
        }

        // POST Values
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            if (value == null) return BadRequest("ERROR: HTTP Post value from body not provided");
            return Ok("Post " + value);
        }

        // PUT Values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            if (id == 0) return BadRequest("ERROR: id not provided");
            if (value == null) return BadRequest("ERROR: HTTP Put value from body not provided");
            return Ok("Put id#" + id + " : " + value);
        }

        // DELETE Values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest("ERROR: id not provided");
            return Ok("Delete id#" + id);
        }
    }
}
