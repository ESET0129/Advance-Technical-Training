using AMI_project.Dtos;
using AMI_project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AMI_project.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AmidbContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(AmidbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username);

            // 1. Check if user exists
            if (user == null)
            {
                return null; // User not found
            }

            // 2. Check if password is correct
            // ** We use BCrypt to verify the hash **
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return null; // Invalid password
            }

            // 3. Check if user is active
            if (user.IsActive != true)
            {
                return null; // User account is disabled
            }

            // 4. User is valid, generate token
            var token = GenerateAdminJwtToken(user);

            return new AuthResponseDto
            {
                Username = user.Username,
                Email = user.Email,
                Token = token
            };
        }

        public async Task<User> RegisterAsync(RegisterRequestDto request)
        {
            // 1. Check if user already exists
            if (await _context.Users.AnyAsync(u => u.Username == request.Username || u.Email == request.Email))
            {
                // We should throw a specific exception here, but for now we'll return null
                return null;
            }

            // 2. Hash the password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Username = request.Username,
                DisplayName = request.DisplayName,
                Email = request.Email,
                Phone = request.Phone,
                PasswordHash = hashedPassword,
                IsActive = true, // Default to active
                LastLogin = null
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        // --- ADD THIS NEW CONSUMER LOGIN METHOD ---
        public async Task<AuthResponseDto> ConsumerLoginAsync(ConsumerLoginRequestDto request)
        {
            // 1. Find the consumer by ID and EXACT name match
            var consumer = await _context.Consumers
                .FirstOrDefaultAsync(c => c.ConsumerId == request.ConsumerId && c.Name == request.ConsumerName);

            if (consumer == null)
            {
                return null; // Invalid credentials
            }

            // 2. Check if consumer account is active
            if (consumer.Status != "Active")
            {
                return null; // Account is inactive
            }

            // 3. User is valid, generate a consumer-specific token
            var token = GenerateConsumerJwtToken(consumer);

            return new AuthResponseDto
            {
                Username = consumer.Name,
                Email = consumer.Email,
                Token = token
            };
        }

        // --- ADD THIS NEW CONSUMER TOKEN GENERATOR ---
        private string GenerateConsumerJwtToken(Consumer consumer)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        // The "Subject" is their ConsumerID. This is the most important part.
        new Claim(JwtRegisteredClaimNames.Sub, consumer.ConsumerId.ToString()),
        new Claim(ClaimTypes.Name, consumer.Name),
        new Claim(JwtRegisteredClaimNames.Email, consumer.Email ?? ""), // Handle null email
        new Claim(ClaimTypes.Role, "Consumer"), // <-- This is the new role
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        // --- JWT Token Generation Helper ---
        private string GenerateAdminJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("UserID", user.UserId.ToString()), // Custom claim
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8), // Token is valid for 8 hours
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
