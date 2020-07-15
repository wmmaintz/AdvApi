using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities;
using AdvApi.Data;

namespace AdvApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        private readonly ApiContext _context;

        public ServersController(ApiContext context)
        {
            _context = context;
        }

        /* Server model
         * =============
         * Name
         * IsOnLine
         */

        /// <summary>
        /// GET: api/servers
        /// </summary>
        /// <returns>List of Server Objects</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Server>>> GetServers()
        {
            IList<Server> servers = await _context.Servers.ToListAsync();
            return Ok(servers.OrderBy(i => i.Name));
        }

        /// <summary>
        /// GET: api/servers/QA-App
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Server Object</returns>
        [HttpGet("{name}")]
        public async Task<ActionResult<Server>> GetServer(string name)
        {
            var server = await _context.Servers.FindAsync(name);

            if (server == null)
            {
                return NotFound();
            }

            return server;
        }

        /// <summary>
        /// PUT: api/servers/oldServerName
        /// </summary>
        /// <param name="name">OldServerName</param>
        /// <param name="server">Server model with changed values</param>
        /// <returns>Updated Server Object</returns>
        /// see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{name}")]
        public async Task<IActionResult> PutServer(string name, [FromBody] Server server)
        {
            if (
                   (server.Name == name)
                && (server.IsOnLine != true)
                && (server.IsOnLine != false)
            )
            {
                return BadRequest();
            }

            server.Name = name;
            if (name != server.Name)
            {
                return BadRequest();
            }

            _context.Entry(server).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServerExists(name))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(server);
        }

        // POST: api/servers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Server>> PostServer(Server server)
        {
            if ( server.Name == null )
            {
                return BadRequest("Server name not provided.");
            }

            _context.Servers.Add(server);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServer", new { name = server.Name }, server);
        }

        /// <summary>
        /// DELETE: api/servers/oldServerName
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Deleted Server Object</returns>
        [HttpDelete("{name}")]
        public async Task<ActionResult<Server>> DeleteServer(string name)
        {
            var server = await _context.Servers.FindAsync(name);
            if (server == null)
            {
                return NotFound();
            }

            _context.Servers.Remove(server);
            await _context.SaveChangesAsync();

            return Ok(server);
        }

        private bool ServerExists(string name)
        {
            return _context.Servers.Any(e => e.Name == name);
        }
    }
}
