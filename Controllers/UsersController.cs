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
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace LuzmaShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly LuzmaShopAPIContext _context;

        public UsersController(LuzmaShopAPIContext context)
        {
            _context = context;
        }

        
        // GET: api/Users/Login
        [HttpPost("Login")]
        public async Task<IActionResult> LogIn([FromBody] User loginUser)
        {
            // Query the database to check if a user with the provided email and password exists
            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == loginUser.Email);

            if (user != null && user.Password == loginUser.Password)
            {
                // User found, generate JWT token
                string key = "GUAP010823HSRTGDA7SuperSecureKey2024";
                string duration = "60"; // Token expiration duration in minutes
                var symmetrickey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(symmetrickey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim("firstName", user.FirstName),
                    new Claim("lastName", user.LastName),
                    new Claim("email", user.Email),
                    new Claim("address", user.Address),
                    new Claim("mobile", user.Mobile),
                    new Claim("createdAt", user.CreatedAt),
                    new Claim("updatedAt", user.UpdatedAt),
                };

                var token = new JwtSecurityToken(
                    issuer: "localhost",
                    audience: "localhost",
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(double.Parse(duration)),
                    signingCredentials: credentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(tokenString);
            }
            else {
                return BadRequest("Invalid email or password.");
            }
            
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(User user)
        {
            //Validate if the email is not already in use.
            var existingEmail = await _context.User.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingEmail != null)
            {
                return BadRequest("Email already in use.");
            }

            user.CreatedAt = DateTime.UtcNow.ToString();
            user.UpdatedAt = DateTime.UtcNow.ToString();
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User added succesfully!");
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            user.UpdatedAt = DateTime.UtcNow.ToString();
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(await _context.User.ToListAsync());
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            //Validate if the email is not already in use.
            var existingEmail = await _context.User.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingEmail != null)
            {
                return BadRequest("Email already in use.");
            }

            user.CreatedAt = DateTime.UtcNow.ToString();
            user.UpdatedAt = DateTime.UtcNow.ToString();
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User added succesfully!");
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(await _context.User.ToListAsync());
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
