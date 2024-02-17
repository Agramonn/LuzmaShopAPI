using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LuzmaShopAPI.Data;
using LuzmaShopAPI.Models;
using Microsoft.VisualBasic;

namespace LuzmaShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly LuzmaShopAPIContext _context;

        public ReviewsController(LuzmaShopAPIContext context)
        {
            _context = context;
        }

        [HttpPost("Post")]
        public async Task<ActionResult> InsertReview([FromBody] Review review)
        {
            var product = await _context.Product.FirstOrDefaultAsync(p => p.Id == review.Product.Id);
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == review.User.Id);

            if (product == null || user == null)
            {
                return BadRequest("Invalid user or product Id");
            }

            review.Product = product;
            review.User = user;
            review.CreatedAt = DateTime.UtcNow.ToString();
            _context.Review.Add(review);
            await _context.SaveChangesAsync();

            return Ok("Review Successfully Sent");
        }
        // GET: api/Reviews
        [HttpGet("GetProductReviews/{productId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetProductReviews(int productId)
        {
            var reviews = await _context.Review.Where(r => r.Product.Id == productId).Include(r => r.User).ToListAsync();
            return Ok(reviews);
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReview()
        {
            return await _context.Review.ToListAsync();
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _context.Review.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        // PUT: api/Reviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, Review review)
        {
            if (id != review.Id)
            {
                return BadRequest();
            }

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
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

        // POST: api/Reviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(Review review)
        {
            _context.Review.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReview", new { id = review.Id }, review);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Review.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReviewExists(int id)
        {
            return _context.Review.Any(e => e.Id == id);
        }
    }
}
