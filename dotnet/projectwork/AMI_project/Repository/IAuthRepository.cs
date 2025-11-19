using AMI_project.Dtos;
using AMI_project.Models;

namespace AMI_project.Repository
{
    public interface IAuthRepository
    {
        Task<User> RegisterAsync(RegisterRequestDto request);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
        //Task<AuthResponseDto> ConsumerLoginAsync(ConsumerLoginRequestDto request);
    }
}