using System.Collections.Generic;
using System.Threading.Tasks;
using ApiMysqlDocker.Data;
using ApiMysqlDocker.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiMysqlDocker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public ProductsController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
			return await _context.Products.ToListAsync();
        }

        [HttpGet("{id:int}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
        {
			var product = await _context.Products.FindAsync(id);

			if (product is null)
            {
				return NotFound();
            }

			return product;
        }

		[HttpPut("{id:int}")]
		public async Task<ActionResult<Product>> ChangeProduct(int id, Product product)
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
				if (!ProductExists(id).Result)
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

		private async Task<bool> ProductExists(int id)
        {
			return await _context.Products.AnyAsync(e => e.Id == id);
        }

		[HttpPost]
		public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
			_context.Products.Add(product);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        [HttpDelete("{id:int}")]
		public async Task<ActionResult> DeleteProduct(int id)
        {
			var product = await _context.Products.FindAsync(id);
			if (product is null)
            {
				return NotFound();
            }

			_context.Products.Remove(product);
			await _context.SaveChangesAsync();

			return NoContent();
        }
	}
}

