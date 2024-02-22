using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LuzmaShopAPI.Data;
using LuzmaShopAPI.Models;

namespace LuzmaShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly LuzmaShopAPIContext _context;

        public CartsController(LuzmaShopAPIContext context)
        {
            _context = context;
        }

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("InsertCartItem/{User}/{Product}")]
        public async Task<ActionResult<Cart>> PostCart(int User, int Product)
        {
            var query = await _context.Cart
                .Include(q => q.User)
                .FirstOrDefaultAsync(c => c.User.Id == User && !c.Ordered);

            var product = await _context.Product
                .Include(p => p.ProductCategory)
                .Include(p => p.Offer)
                .FirstOrDefaultAsync(p => p.Id == Product);

            if (product == null)
            {
                return BadRequest("Product not found");
            }

            if (query == null)
            {
                var user = await _context.User.FirstOrDefaultAsync(u => u.Id == User);

                if (user == null)
                {
                    return BadRequest("User not found");
                }

                // Create a new Cart if it doesn't exist
                Cart cart = new Cart
                {
                    User = user,
                    Ordered = false
                };

                _context.Cart.Add(cart);
                await _context.SaveChangesAsync();

                query = await _context.Cart.OrderBy(c => c.Id).LastOrDefaultAsync();
            }

            // Use the existing query result instead of querying again
            if (query == null || query.Ordered)
            {
                return BadRequest("Cart not found or already ordered");
            }

            var cartItem = new CartItem
            {
                Cart = query,
                Product = product
            };

            _context.CartItem.Add(cartItem);
            await _context.SaveChangesAsync();

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Inserted");
            }
            catch (DbUpdateException ex)
            {
                // Log or handle the exception appropriately
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCart()
        {
            return await _context.Cart.ToListAsync();
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(int id)
        {
            var cart = await _context.Cart.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        // PUT: api/Carts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(int id, Cart cart)
        {
            if (id != cart.Id)
            {
                return BadRequest();
            }

            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
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

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(Cart cart)
        {
            _context.Cart.Add(cart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCart", new { id = cart.Id }, cart);
        }

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var cart = await _context.Cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.Id == id);
        }
    }
}
