using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNetCancel.Web.Models;

namespace DotNetCancel.Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaitController : ControllerBase
    {
        private readonly DotNetCancelWebContext _context;

        public WaitController(DotNetCancelWebContext context)
        {
            _context = context;
        }

        // GET: api/Waits
        [HttpGet()]
        public async Task<IActionResult> Wait()
        {
			using (var connection = _context.Database.GetDbConnection())
			{
				connection.Open();
				using (var command = connection.CreateCommand())
				{
					command.CommandText = "waitfor delay '00:00:30'";
					await command.ExecuteNonQueryAsync();
				}
			}
			return Ok();
        }
    }
}
