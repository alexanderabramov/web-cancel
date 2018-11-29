using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNetCancel.Web.Models;
using System.Data.SqlClient;

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

        [HttpGet("andcancel")]
        public async Task<IActionResult> WaitAndCancel()
        {
			string message = "";
			using (var connection = _context.Database.GetDbConnection())
			{
				connection.Open();
				using (var command = connection.CreateCommand())
				{
					// synthetic long-running query
					command.CommandText = "waitfor delay '00:00:30'";
					Task<int> task = command.ExecuteNonQueryAsync();

					command.Cancel();

					try
					{
						await task;
						message = "Completed";
					}
					catch (SqlException ex)
					{
						var sqlErrors = ex.Errors;
						if (!(sqlErrors.Count==2 && sqlErrors[1].Message=="Operation cancelled by user."))
						{
							throw;
						}
						message = "Cancelled";
					}
				}
			}
			return base.Ok(message);
		}

	}
}
