using AMI_project.Dtos;
using AMI_project.Models;

namespace AMI_project.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserViewDto>> GetUsersAsync();
        Task<UserViewDto> GetUserByIdAsync(int userId);
        Task<UserViewDto> CreateUserAsync(UserCreateDto userDto);
        Task<UserViewDto> UpdateUserAsync(int userId, UserUpdateDto userDto);
        Task<bool> DeleteUserAsync(int userId);
        Task<bool> UserExistsAsync(string username, string email);
    }
}
