using Api.Data;
using Api.DTOs;
using Api.Services;
using Api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly UserRepository _userRepository;
        private readonly DataContext _context;

        public AuthController(JwtService jwtService, UserRepository userRepository, DataContext context)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginDto)
        {
            var user = await _userRepository.Authenticate(loginDto.Username, loginDto.Password);
            if (user == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            var token = _jwtService.GenerateToken(user);
            return Ok(new { Token = token });
        }

        [HttpPost("validate-token")]
        [Authorize]
        public IActionResult ValidateToken()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return Unauthorized(new { message = "Could not retrieve user identity." });
            }
            
            var rToken = JwtService.validarToken(identity, _context); 

            if (!rToken.success)
            {
                return Unauthorized(new { message = rToken.message });
            }

            return Ok(new { message = "Token is valid", user = rToken.result });
        }
    }
}
