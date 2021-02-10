using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using negocio_pequeño.Models;
using negocio_pequeño.Models.Product;
using System.Linq.Dynamic.Core;

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
            string asd = "Nombre";
            return await _context
                .Product
                // .Select(p => new Product {
                //     Id = p.Id,
                //     Nombre = p.Nombre,
                //     PrecioVenta = p.PrecioVenta
                // })
                .OrderBy(asd)
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

        private IQueryable<Product> SearchProducts(IQueryable<Product> query, Product product)
        {
            query = Convert.ToBoolean(product.Id) ? query.Where(p => p.Id == product.Id) : query;
            query = product.Nombre != null ? query.Where(p => EF.Functions.Like(p.Nombre, $"%{product.Nombre}%")) : query;
            query = Convert.ToBoolean(product.PrecioVenta) ? query.Where(p => p.PrecioVenta == product.PrecioVenta) : query;
            return query;
        }

        private IQueryable<Product> GetPaginatedProduct(IQueryable<Product> query, Pagination options)
        {
            if (options.ItemsPerPage == -1) return query.Take(10000);
            return query.Skip((options.Page-1) * options.ItemsPerPage).Take(options.ItemsPerPage);
        }

        private IQueryable<Product> GetOrderProductQuery(IQueryable<Product> query, Pagination options)
        {
            if (options.sortDesc.Length != 0 && options.sortDesc[0] == true)
            {
                return query.OrderBy(options.sortBy[0]+" desc");
            }
            else if(options.sortDesc.Length != 0)
            {
                return query.OrderBy(options.sortBy[0]);
            }
            return query.OrderBy(p => p.Id);
        }

        [HttpPost("withOptions")]
        public async Task<ActionResult<PaginatedProduct>> GetProductWithOptions(Pagination options)
        {
            IQueryable<Product> query = _context.Product;
            // Se busca
            query = SearchProducts(query, options.Product);
            int serverItemsLength = await query.CountAsync();
            // Se ordena
            query = GetOrderProductQuery(query,options);
            // Se página
            query = GetPaginatedProduct(query, options);

            return new PaginatedProduct {
                Product = await query.ToListAsync(),
                ServerItemsLength = serverItemsLength
            };
        }
    }
}
