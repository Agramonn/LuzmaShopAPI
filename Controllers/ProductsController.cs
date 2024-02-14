using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LuzmaShopAPI.Data;
using LuzmaShopAPI.Models;
using Microsoft.Data.SqlClient;

namespace LuzmaShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly LuzmaShopAPIContext _context;

        public ProductsController(LuzmaShopAPIContext context)
        {
            _context = context;
        }

        //GET: api/Products/GetProductsCategory
        [HttpGet("GetProductsCategory")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string category, string subcategory, int count)
        {
            IQueryable<Product> query = _context.Product;

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.ProductCategory.Category == category);
            }

            if (!string.IsNullOrEmpty(subcategory))
            {
                query = query.Where(p => p.ProductCategory.SubCategory == subcategory);
            }

            if (count > 0)
            {
                query = query.OrderBy(p => Guid.NewGuid()).Take(count);
            }

            return await query
                .Include(p => p.ProductCategory)
                .Include(p => p.Offer)
                .ToListAsync();
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            var products = await _context.Product
                                        .Include(p => p.ProductCategory)
                                        .Include(p => p.Offer)
                                        .ToListAsync();

            return Ok(products);
        }


        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Product
                .Include(p => p.ProductCategory)
                .Include(p => p.Offer)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }


        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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

            return Ok(await _context.Product.ToListAsync());
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the category already exists
            var existingCategory = _context.ProductCategory
                                            .FirstOrDefault(pc =>
                                                pc.Id == product.ProductCategory.Id);

            // Check if the offer already exists
            var existingOffer = _context.Offer
                                        .FirstOrDefault(o =>
                                            o.Id == product.Offer.Id);

            // If either category or offer doesn't exist, return BadRequest
            if (existingCategory == null || existingOffer == null)
            {
                return BadRequest("Invalid category or offer.");
            }

            // Set the ProductCategoryId and OfferId in the product
            product.ProductCategory = existingCategory;
            product.Offer = existingOffer;

            // Add the product to the context and save changes
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return Ok(await _context.Product.ToListAsync());
        }




        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(await _context.Product.ToListAsync());
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
