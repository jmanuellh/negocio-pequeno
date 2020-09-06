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
    public class InternetRentasController : ControllerBase
    {
        private readonly Context _context;

        public InternetRentasController(Context context)
        {
            _context = context;
        }

        // GET: api/InternetRentas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InternetRenta>>> GetInternetRentas()
        {
            // return await _context.InternetRentas.ToListAsync();
            return await _context.InternetRentas.Join(
                _context.MovilDispositivos,
                internetRenta => internetRenta.MovilDispositivo.Id,
                movilDispositivo => movilDispositivo.Id,
                (internetRenta, movilDispositivo) => new InternetRenta {
                    Id = internetRenta.Id,
                    FechaFin = internetRenta.FechaFin,
                    Dinero = internetRenta.Dinero,
                    MovilDispositivo = movilDispositivo
                }
            ).ToListAsync();
        }

        // GET: api/InternetRentas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InternetRenta>> GetInternetRenta(int id)
        {
            var internetRenta = await _context.InternetRentas.FindAsync(id);

            if (internetRenta == null)
            {
                return NotFound();
            }

            return internetRenta;
        }

        // PUT: api/InternetRentas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInternetRenta(int id, InternetRenta internetRenta)
        {
            if (id != internetRenta.Id)
            {
                return BadRequest();
            }

            _context.Entry(internetRenta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InternetRentaExists(id))
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

        // POST: api/InternetRentas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<InternetRenta>> PostInternetRenta(InternetRenta internetRenta)
        {
            _context.InternetRentas.Add(internetRenta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInternetRenta", new { id = internetRenta.Id }, internetRenta);
        }

        // DELETE: api/InternetRentas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<InternetRenta>> DeleteInternetRenta(int id)
        {
            var internetRenta = await _context.InternetRentas.FindAsync(id);
            if (internetRenta == null)
            {
                return NotFound();
            }

            _context.InternetRentas.Remove(internetRenta);
            await _context.SaveChangesAsync();

            return internetRenta;
        }

        private bool InternetRentaExists(int id)
        {
            return _context.InternetRentas.Any(e => e.Id == id);
        }
    }
}
