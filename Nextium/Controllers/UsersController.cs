using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nextium.DataLayer;
using Nextium.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Twilio.TwiML.Messaging;


namespace Nextium.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly NextiumContext _context;

        public UsersController(NextiumContext context)
        {
            _context = context;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] UserSignupDto signupDto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == signupDto.Email || u.Username == signupDto.Username))
            {
                return Ok(new { Message = "Email or Username already exists." });
            }

            var user = new User
            {
                FirstName = signupDto.FirstName,
                LastName = signupDto.LastName,
                Email = signupDto.Email,
                Username = signupDto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(signupDto.Password),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            User user=new User();
            try
            {
                 user = await _context.Users.SingleOrDefaultAsync(u => u.Username == loginDto.Username);
            }
            catch (Exception ex)
            {
                string error = ex.InnerException.Message;
            }
            if (user == null)
            {
                user = await _context.Users.SingleOrDefaultAsync(u => u.Email == loginDto.Username);
            }

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                return Ok(new { Message = "Invalid username or password." });
            }

            return Ok(new { Message = "Login successful", UserName = user.Username });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] string EmailOrUserName)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == EmailOrUserName || u.Username == EmailOrUserName);

            if (user == null)
            {
                return Ok(new { Message = "Email or UserName not found." });
            }

            return Ok(new { Message = "Please Reset", Email = user.Email });
        }

        // Update password
        [HttpPut("{Email}")]
        public async Task<IActionResult> UpdatePassword(string Email, string password)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Email == Email);
                if (user != null)
                {
                    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return Ok(new { Message = "Email Not Found" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Message = ex.Message });
            }

            return Ok(new { Message = "Password Updated Successfully" });
        }
    }
}
