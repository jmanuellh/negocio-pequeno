using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using negocio_pequeño.Models;
using negocio_pequeño.Models.Product;

namespace negocio_pequeño.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly Context _context;

        public ProductController(Context context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            return await _context
                .Product
                .Select(p => new Product {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    PrecioVenta = p.PrecioVenta
                })
                .ToListAsync();
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Product
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }

        [HttpPost("search")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts(Product product)
        {
            IQueryable<Product> query = _context.Product;
            query = Convert.ToBoolean(product.Id) ? query.Where(p => p.Id == product.Id) : query;
            query = product.Nombre != null ? query.Where(p => EF.Functions.Like(p.Nombre, $"%{product.Nombre}%")) : query;
            query = Convert.ToBoolean(product.PrecioVenta) ? query.Where(p => p.PrecioVenta == product.PrecioVenta) : query;
            return await query.OrderBy(p => p.Id).ToListAsync();
        }

        [HttpPost("paginated")]
        public async Task<ActionResult<IEnumerable<Product>>> GetPaginatedProduct(Pagination pagination)
        {
            var query = _context.Product.Skip((pagination.pageNumber-1) * pagination.pageSize).Take(pagination.pageSize);
            return await query.OrderBy(p => p.Id).ToListAsync();
        }
    }
}