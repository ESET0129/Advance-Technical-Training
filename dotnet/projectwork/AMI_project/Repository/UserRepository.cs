using AMI_project.Dtos;
using AMI_project.Models;
using Microsoft.EntityFrameworkCore;

namespace AMI_project.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AmidbContext _context;

        public UserRepository(AmidbContext context)
        {
            _context = context;
        }

        // Helper to map User -> UserViewDto
        private UserViewDto MapToViewDto(User user)
        {
            return new UserViewDto
            {
                UserId = user.UserId,
                Username = user.Username,
                DisplayName = user.DisplayName,
                Email = user.Email,
                Phone = user.Phone,
                LastLogin = user.LastLogin,
                IsActive = user.IsActive
            };
        }

        //public async Task<IEnumerable<UserViewDto>> GetUsersAsync()
        //{
        //    return await _context.Users
        //        .AsNoTracking()
        //        .Select(user => MapToViewDto(user)) // Project to the safe DTO
        //        .ToListAsync();
        //}
        public async Task<IEnumerable<UserViewDto>> GetUsersAsync()
        {
            // This expression can be translated directly into a SQL SELECT query
            return await _context.Users
                .AsNoTracking()
                .Select(user => new UserViewDto
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Phone = user.Phone,
                    LastLogin = user.LastLogin,
                    IsActive = user.IsActive
                })
                .ToListAsync();
        }

        public async Task<UserViewDto> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == userId);

            return user == null ? null : MapToViewDto(user);
        }

        public async Task<UserViewDto> CreateUserAsync(UserCreateDto userDto)
        {
            var user = new User
            {
                Username = userDto.Username,
                DisplayName = userDto.DisplayName,
                Email = userDto.Email,
                Phone = userDto.Phone,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                IsActive = userDto.IsActive
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return MapToViewDto(user);
        }

        public async Task<UserViewDto> UpdateUserAsync(int userId, UserUpdateDto userDto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return null;

            // Update properties
            user.DisplayName = userDto.DisplayName;
            user.Email = userDto.Email;
            user.Phone = userDto.Phone;
            user.IsActive = userDto.IsActive;

            // Only update password if a new one is provided
            if (!string.IsNullOrEmpty(userDto.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            }

            await _context.SaveChangesAsync();
            return MapToViewDto(user);
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            // Add a safety check: don't let the 'admin' user be deleted
            if (user.Username.ToLower() == "admin")
            {
                return false; // Cannot delete admin
            }

            // TODO: Check if user.Username is in `ami.Consumer` (CreatedBy/UpdatedBy)
            // If so, you may want to block deletion.
            // For now, we allow it.

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UserExistsAsync(string username, string email)
        {
            return await _context.Users
                .AnyAsync(u => u.Username == username || u.Email == email);
        }
    }
}
