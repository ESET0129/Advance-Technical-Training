


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using CollegeApp.Models;
using CollegeApp.Data; // Add your DbContext namespace

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly CollegeContext _context; // Your DbContext

        public AuthController(IConfiguration configuration, CollegeContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel user)
        {
            try
            {
                // Validate input
                if (string.IsNullOrEmpty(user.UserId) || string.IsNullOrEmpty(user.Password))
                {
                    return BadRequest("UserId and Password are required");
                }

                // Find user in database
                var dbUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.UserId == user.UserId);

                // Check if user exists and password matches
                if (dbUser == null || dbUser.Password != user.Password)
                {
                    return Unauthorized("Invalid UserId or Password");
                }

                // Generate JWT token
                var token = GenerateJwtToken(dbUser);

                return Ok(new
                {
                    token,
                    user = new
                    {
                        userId = dbUser.UserId,
                        userName = dbUser.UserName,
                        email = dbUser.Email,
                        role = dbUser.Role
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["DurationInMinutes"])),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    public class LoginModel
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}
