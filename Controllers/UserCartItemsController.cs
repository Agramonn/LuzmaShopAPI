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
    public class UserCartItemsController : ControllerBase
    {
        private readonly LuzmaShopAPIContext _context;

        public UserCartItemsController(LuzmaShopAPIContext context)
        {
            _context = context;
        }

        // GET: api/UserCartItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCartItem>>> GetUserCartItem()
        {
            return await _context.UserCartItem.ToListAsync();
        }

        // GET: api/UserCartItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserCartItem>> GetUserCartItem(int id)
        {
            var userCartItem = await _context.UserCartItem.FindAsync(id);

            if (userCartItem == null)
            {
                return NotFound();
            }

            return userCartItem;
        }

        // PUT: api/UserCartItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserCartItem(int id, UserCartItem userCartItem)
        {
            if (id != userCartItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(userCartItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserCartItemExists(id))
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

        // POST: api/UserCartItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserCartItem>> PostUserCartItem(UserCartItem userCartItem)
        {
            _context.UserCartItem.Add(userCartItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserCartItem", new { id = userCartItem.Id }, userCartItem);
        }

        // DELETE: api/UserCartItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserCartItem(int id)
        {
            var userCartItem = await _context.UserCartItem.FindAsync(id);
            if (userCartItem == null)
            {
                return NotFound();
            }

            _context.UserCartItem.Remove(userCartItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserCartItemExists(int id)
        {
            return _context.UserCartItem.Any(e => e.Id == id);
        }
    }
}
