using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using negocio_pequeño.Models;

namespace negocio_pequeño.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsolesController : ControllerBase
    {
        private readonly Context _context;

        public ConsolesController(Context context)
        {
            _context = context;
        }

        // GET: api/Consoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Console>>> GetConsoles()
        {
            return await _context.Consoles.ToListAsync();
        }

        // GET: api/Consoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Console>> GetConsole(int id)
        {
            var console = await _context.Consoles.FindAsync(id);

            if (console == null)
            {
                return NotFound();
            }

            return console;
        }

        // PUT: api/Consoles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsole(int id, Console console)
        {
            if (id != console.Id)
            {
                return BadRequest();
            }

            _context.Entry(console).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsoleExists(id))
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

        // POST: api/Consoles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Console>> PostConsole(Console console)
        {
            _context.Consoles.Add(console);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConsole", new { id = console.Id }, console);
        }

        // DELETE: api/Consoles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Console>> DeleteConsole(int id)
        {
            var console = await _context.Consoles.FindAsync(id);
            if (console == null)
            {
                return NotFound();
            }

            _context.Consoles.Remove(console);
            await _context.SaveChangesAsync();

            return console;
        }

        private bool ConsoleExists(int id)
        {
            return _context.Consoles.Any(e => e.Id == id);
        }
    }
}
