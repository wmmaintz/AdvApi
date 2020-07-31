using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities;
using AdvApi.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.SqlServer.Migrations.Internal;
using Entities.Repositories;
using static Entities.Repositories.ServerRepository;

namespace AdvApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        private readonly ApiContext _ctx;
        private readonly ILogger<ServersController> _logger;
        private readonly ServerRepository _repo;

        public ServersController(DbContextOptions<ApiContext> options, ILogger<ServersController> logger)
        {
            ApiContext ctx = new ApiContext(options);
            ServerRepository repo = new ServerRepository(ctx);
            _ctx = ctx;
            _logger = logger;
            _repo = repo;
        }

        /// <summary>
        /// GET: api/servers
        /// </summary>
        /// <returns>List of Servers</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Server>>> GetServers()
        {
            IEnumerable<Server> servers = await _repo.GetAllServersAsync();
            if (servers == null)
            {
                return NotFound();
            }
            return Ok(servers);
        }

        /// <summary>
        /// GET: api/servers/name/QA
        /// </summary>
        /// <param name="name"></param>
        /// <returns>servers</returns>
        [HttpGet("name/{name}")]
        public async Task<ActionResult<Server>> GetServersWithNameLike(string name)
        {
            if (name == null)
            {
                return BadRequest("ERROR: A customer name was not provided");
            }
            IEnumerable<Server> servers = await _repo.GetServersByNameAsync(name);
            if (servers == null)
            {
                return NotFound("A server similar to " + name + " was not found");
            }
            return Ok(servers);
        }

        /// <summary>
        /// GET: api/servers/QA-App
        /// </summary>
        /// <param name="name"></param>
        /// <returns>server</returns>
        [HttpGet("{name}")]
        public async Task<ActionResult<Server>> GetServer(string name)
        {
            if (name == null)
            {
                return BadRequest("ERROR: A customer name was not provided");
            }
            Server server = await _repo.GetServerAsync(name);
            if (server == null)
            {
                return NotFound("A server, named " + name + ", was not found");
            }
            return Ok(server);
        }

        /// <summary>
        /// PUT: api/servers
        /// </summary>
        /// <param name="name">OldServerName</param>
        /// <returns>server</returns>
        /// see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> UpdateServer([FromBody] Server chgdServer)
        {
            if (chgdServer.Name == null)
            {
                return BadRequest("ERROR: Server name is invalid");
            }

            ServerResponse sr = await _repo.UpdateServerAsync(chgdServer);

            if (sr.SrResult == null)
            {
                return Ok(sr.SrServer);
            }
            return sr.SrResult.Substring(0, 6) switch
            {
                "NullId" => BadRequest("Invalid server name"),
                "NotFou" => Problem("ERROR: Server not found"),
                _ => Problem(sr.SrResult)
            };
        }

        // POST: api/servers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Server>> PostServer(Server newServer)
        {
            if (newServer.Name == null )
            {
                return BadRequest("Server name not provided.");
            }

            ServerResponse sr = await _repo.CreateServerAsync(newServer);

            if (sr.SrResult == null)
            {
                return Ok(sr.SrServer);
                // return CreatedAtAction("GetServer", new { name = server.Name }, server);
            }
            return sr.SrResult.Substring(0, 6) switch
            {
                "NullId" => BadRequest("Invalid customer email"),
                "Exists" => Problem("ERROR: Customer existed before insertion"),
                _ => Problem(sr.SrResult)
            };
        }

        /// <summary>
        /// DELETE: api/servers/serverName
        /// </summary>
        /// <param name="name"></param>
        /// <returns>server</returns>
        [HttpDelete("{name}")]
        public async Task<ActionResult<Server>> DeleteServer(string name)
        {
            if (name == null)
            {
                return BadRequest("Server name not provided.");
            }

            ServerResponse sr = await _repo.DeleteServerAsync(name);

            if (sr.SrResult == null)
            {
                return Ok();
            }
            return sr.SrResult.Substring(0, 6) switch
            {
                "NullId" => BadRequest("Invalid customer email"),
                "NotFou" => Problem("ERROR: Server name not found"),
                _ => Problem(sr.SrResult)
            };
        }

        private bool ServerExists(string name)
        {
            return _ctx.Servers.Any(e => e.Name == name);
        }
    }
}
