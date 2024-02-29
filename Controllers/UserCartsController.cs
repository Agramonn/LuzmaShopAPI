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
    public class UserCartsController : ControllerBase
    {
        private readonly LuzmaShopAPIContext _context;

        public UserCartsController(LuzmaShopAPIContext context)
        {
            _context = context;
        }
        // GET: api/UserCarts
        [HttpGet("GetAllPreviousCartsOfUser/{UserId}")]
        public async Task<ActionResult<IEnumerable<UserCart>>> GetAllPreviousCartsOfUser(int UserId)
        {
            return await _context.UserCart.Where(uc => uc.Ordered == true && uc.User.Id == UserId).Include(uc => uc.User).Include(uc => uc.CartItems).ThenInclude(ci => ci.Product).ThenInclude(pi=>pi.ProductCategory).ToListAsync();
        }
        // GET: api/UserCarts
        [HttpGet("GetActiveCartOfUser/{UserId}")]
        public async Task<ActionResult<UserCart>> GetActiveCartOfUser(int UserId)
        {
            var usercart = new UserCart();
            var ActiveCarts = await _context.Cart.Where(c => c.User.Id == UserId && c.Ordered == false).ToListAsync();

            if(ActiveCarts == null || ActiveCarts.Count == 0)
            {
                return Ok(usercart);
            }

            var cartid = await _context.Cart.Where(c => c.User.Id == UserId && !c.Ordered).Select(c => c.Id).FirstOrDefaultAsync();

            var cartitemids = await _context.CartItem.Include(c => c.Product).Where(c => c.Cart.Id == cartid).ToListAsync();

            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == UserId);

            foreach (var item in cartitemids)
            {
                var productitem = _context.Product.Include(p => p.ProductCategory).Include(p => p.Offer).Where(p => p.Id == item.Product.Id).FirstOrDefault();

                if (productitem != null)
                {
                    UserCartItem cartitem = new()
                    {
                        Id = item.Id,
                        Product = productitem,
                    };
                    usercart.CartItems.Add(cartitem);
                } 
            }

           
            if(user != null)
            {
                usercart.Id = cartid;
                usercart.User = user;
                usercart.Ordered = false;
                usercart.OrderedOn = "";
            }
            
            return Ok(usercart);

        }

        // GET: api/UserCarts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCart>>> GetUserCart()
        {
            return await _context.UserCart.ToListAsync();
        }

        // GET: api/UserCarts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserCart>> GetUserCart(int id)
        {
            var userCart = await _context.UserCart.FindAsync(id);

            if (userCart == null)
            {
                return NotFound();
            }

            return userCart;
        }

        // PUT: api/UserCarts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserCart(int id, UserCart userCart)
        {
            if (id != userCart.Id)
            {
                return BadRequest();
            }

            _context.Entry(userCart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserCartExists(id))
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

        // POST: api/UserCarts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserCart>> PostUserCart(UserCart userCart)
        {
            _context.UserCart.Add(userCart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserCart", new { id = userCart.Id }, userCart);
        }

        // DELETE: api/UserCarts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserCart(int id)
        {
            var userCart = await _context.UserCart.FindAsync(id);
            if (userCart == null)
            {
                return NotFound();
            }

            _context.UserCart.Remove(userCart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserCartExists(int id)
        {
            return _context.UserCart.Any(e => e.Id == id);
        }
    }
}
