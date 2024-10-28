using Api.Models;
using Api.Utilities;
using Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Api.Data;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository userRepository;
        private readonly DataContext _context; 

        public UserController(UserRepository _userRepository, DataContext context)
        {
            userRepository = _userRepository;
            _context = context;
        }

        [HttpGet("profile", Name = "GetUserProfile")]
        [Authorize] 
        public async Task<ActionResult<User>> GetUserProfile()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null)
            {
                return Unauthorized(new { message = "Could not retrieve user identity." });
            }

            var rToken = JwtService.validarToken(identity, _context);
            if (!rToken.success) return Unauthorized(rToken);

            User user = rToken.result;

            var userId = user.UserID.ToString();
            var userProfile = await userRepository.GetByIdAsync(int.Parse(userId));

            if (userProfile == null)
            {
                return NotFound(new { message = "User not found." });
            }
            return Ok(userProfile);
        }

        [HttpGet(Name = "GetUsers")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            
            if (identity == null)
            {
                return Unauthorized(new { message = "Could not retrieve user identity." });
            }
            
            var rToken = JwtService.validarToken(identity, _context);
            if (!rToken.success) return Unauthorized(rToken);

            User user = rToken.result;

            if (user.Role != UserRole.Admin)
            {
                return Unauthorized(new { message = "Access denied. Admin role required." });
            }

            var users = await userRepository.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}", Name = "GetUser")]
        [Authorize]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            
            if (identity == null)
            {
                return Unauthorized(new { message = "Could not retrieve user identity." });
            }
            
            var rToken = JwtService.validarToken(identity, _context);
            if (!rToken.success) return Unauthorized(rToken);

            User user = rToken.result;

            if (user.Role != UserRole.Admin)
            {
                return Unauthorized(new { message = "Access denied. Admin role required." });
            }

            var userSearch = await userRepository.GetByIdAsync(id);

            if (userSearch == null)
            {
                return NotFound(ErrorUtilities.FieldNotFound("User", id));
            }
            return Ok(userSearch);
        }

        [HttpPost(Name = "AddUser")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] RegisterUserDTO userDTO)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            
            if (identity == null)
            {
                return Unauthorized(new { message = "Could not retrieve user identity." });
            }
            
            var rToken = JwtService.validarToken(identity, _context);
            if (!rToken.success) return Unauthorized(rToken);

            User user = rToken.result;

            if (user.Role != UserRole.Admin)
            {
                return Unauthorized(new { message = "Access denied. Admin role required." });
            }

            var newUser = await userRepository.CreateAsync(userDTO);
            return CreatedAtAction(nameof(GetById), new { id = newUser.UserID }, userDTO);
        }

        [HttpPut("{id}", Name = "EditUser")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] RegisterUserDTO userDTO)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            
            if (identity == null)
            {
                return Unauthorized(new { message = "Could not retrieve user identity." });
            }
            
            var rToken = JwtService.validarToken(identity, _context);
            if (!rToken.success) return Unauthorized(rToken);

            User user = rToken.result;

            if (user.Role != UserRole.Admin)
            {
                return Unauthorized(new { message = "Access denied. Admin role required." });
            }

            if (id <= 0)
            {
                return BadRequest(ErrorUtilities.IdPositive(id));
            }

            var userToUpdate = await userRepository.GetByIdAsync(id);

            if (userToUpdate != null)
            {
                await userRepository.Update(id, userDTO);
                return NoContent();
            }
            else
            {
                return NotFound(ErrorUtilities.FieldNotFound("User", id));
            }
        }
    }
}
