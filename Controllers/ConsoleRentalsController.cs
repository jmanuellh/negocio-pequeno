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
    public class ConsoleRentalsController : ControllerBase
    {
        private readonly Context _context;

        public ConsoleRentalsController(Context context)
        {
            _context = context;
        }

        // GET: api/ConsoleRentals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsoleRental>>> GetConsoleRentals()
        {
            return await _context.ConsoleRentals.ToListAsync();
        }

        // GET: api/ConsoleRentals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsoleRental>> GetConsoleRental(int id)
        {
            var consoleRental = await _context.ConsoleRentals.FindAsync(id);

            if (consoleRental == null)
            {
                return NotFound();
            }

            return consoleRental;
        }

        // PUT: api/ConsoleRentals/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsoleRental(int id, ConsoleRental consoleRental)
        {
            if (id != consoleRental.Id)
            {
                return BadRequest();
            }

            _context.Entry(consoleRental).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsoleRentalExists(id))
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

        // POST: api/ConsoleRentals
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ConsoleRental>> PostConsoleRental(ConsoleRental consoleRental)
        {
            _context.ConsoleRentals.Add(consoleRental);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConsoleRental", new { id = consoleRental.Id }, consoleRental);
        }

        // DELETE: api/ConsoleRentals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ConsoleRental>> DeleteConsoleRental(int id)
        {
            var consoleRental = await _context.ConsoleRentals.FindAsync(id);
            if (consoleRental == null)
            {
                return NotFound();
            }

            _context.ConsoleRentals.Remove(consoleRental);
            await _context.SaveChangesAsync();

            return consoleRental;
        }

        private bool ConsoleRentalExists(int id)
        {
            return _context.ConsoleRentals.Any(e => e.Id == id);
        }
    }
}
