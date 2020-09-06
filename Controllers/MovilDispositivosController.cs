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
    public class MovilDispositivosController : ControllerBase
    {
        private readonly Context _context;

        public MovilDispositivosController(Context context)
        {
            _context = context;
        }

        // GET: api/MovilDispositivos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovilDispositivo>>> GetMovilDispositivos()
        {
            return await _context.MovilDispositivos.ToListAsync();
        }

        // GET: api/MovilDispositivos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovilDispositivo>> GetMovilDispositivo(int id)
        {
            var movilDispositivo = await _context.MovilDispositivos.FindAsync(id);

            if (movilDispositivo == null)
            {
                return NotFound();
            }

            return movilDispositivo;
        }

        // PUT: api/MovilDispositivos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovilDispositivo(int id, MovilDispositivo movilDispositivo)
        {
            if (id != movilDispositivo.Id)
            {
                return BadRequest();
            }

            _context.Entry(movilDispositivo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovilDispositivoExists(id))
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

        // POST: api/MovilDispositivos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MovilDispositivo>> PostMovilDispositivo(MovilDispositivo movilDispositivo)
        {
            _context.MovilDispositivos.Add(movilDispositivo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovilDispositivo", new { id = movilDispositivo.Id }, movilDispositivo);
        }

        // DELETE: api/MovilDispositivos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MovilDispositivo>> DeleteMovilDispositivo(int id)
        {
            var movilDispositivo = await _context.MovilDispositivos.FindAsync(id);
            if (movilDispositivo == null)
            {
                return NotFound();
            }

            _context.MovilDispositivos.Remove(movilDispositivo);
            await _context.SaveChangesAsync();

            return movilDispositivo;
        }

        private bool MovilDispositivoExists(int id)
        {
            return _context.MovilDispositivos.Any(e => e.Id == id);
        }
    }
}
