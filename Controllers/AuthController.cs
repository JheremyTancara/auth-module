using Api.DTOs;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly UserRepository _userRepository;

        public AuthController(JwtService jwtService, UserRepository userRepository)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginDto)
        {
            var user = await _userRepository.Authenticate(loginDto.Username, loginDto.Password); // Cambiar a await
            if (user == null)
            {
                return Unauthorized();
            }

            var token = _jwtService.GenerateToken(user);
            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerUserDto)
        {
            var user = await _userRepository.Register(registerUserDto); // Cambiar a await
            if (user == null)
            {
                return BadRequest("User registration failed.");
            }

            return Ok(user);
        }
    }
}
