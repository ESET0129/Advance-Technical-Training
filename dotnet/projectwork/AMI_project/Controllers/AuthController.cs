using AMI_project.Dtos;
using AMI_project.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMI_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = await _authRepository.RegisterAsync(request);

            if (newUser == null)
            {
                return BadRequest("User with this username or email already exists.");
            }

            // Automatically log in the user after registration
            var loginRequest = new LoginRequestDto { Username = request.Username, Password = request.Password };
            var authResponse = await _authRepository.LoginAsync(loginRequest);

            if (authResponse == null)
            {
                return StatusCode(500, "User created but failed to log in.");
            }

            return Ok(authResponse);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authResponse = await _authRepository.LoginAsync(request);

            if (authResponse == null)
            {
                return Unauthorized("Invalid username, password, or inactive account.");
            }

            return Ok(authResponse);
        }

        [HttpPost("consumer-login")]
        public async Task<IActionResult> ConsumerLogin([FromBody] ConsumerLoginRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authResponse = await _authRepository.ConsumerLoginAsync(request);

            if (authResponse == null)
            {
                return Unauthorized(new { message = "Invalid Consumer ID or Name, or account is inactive." });
            }

            return Ok(authResponse);
        }
    }
}
