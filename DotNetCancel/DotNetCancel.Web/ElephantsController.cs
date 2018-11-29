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
    public class ElephantsController : ControllerBase
    {
        private readonly DotNetCancelWebContext _context;

        public ElephantsController(DotNetCancelWebContext context)
        {
            _context = context;
        }

        // GET: api/Elephants
        [HttpGet]
        public IEnumerable<Elephant> GetElephant()
        {
            return _context.Elephant;
        }

        // GET: api/Elephants/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetElephant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var elephant = await _context.Elephant.FindAsync(id);

            if (elephant == null)
            {
                return NotFound();
            }

            return Ok(elephant);
        }

        // PUT: api/Elephants/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutElephant([FromRoute] int id, [FromBody] Elephant elephant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != elephant.Id)
            {
                return BadRequest();
            }

            _context.Entry(elephant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElephantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Elephants
        [HttpPost]
        public async Task<IActionResult> PostElephant([FromBody] Elephant elephant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Elephant.Add(elephant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetElephant", new { id = elephant.Id }, elephant);
        }

        // DELETE: api/Elephants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElephant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var elephant = await _context.Elephant.FindAsync(id);
            if (elephant == null)
            {
                return NotFound();
            }

            _context.Elephant.Remove(elephant);
            await _context.SaveChangesAsync();

            return Ok(elephant);
        }

        private bool ElephantExists(int id)
        {
            return _context.Elephant.Any(e => e.Id == id);
        }
    }
}