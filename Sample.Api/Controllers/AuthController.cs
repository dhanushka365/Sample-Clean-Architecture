using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Sample.Application.interfaces.User;
using Sample.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtSettings _jwtSettings;

        public AuthController(IUserService userService, JwtSettings jwtSettings)
        {
            _userService = userService;
            _jwtSettings = jwtSettings;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Invalid request. Username and password are required.");
            }

            var authenticatedUser = _userService.GetUser(user.Username, user.Password);

            if (authenticatedUser == null)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey ?? throw new InvalidOperationException("JWT key is not configured."));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, authenticatedUser.Username ?? throw new InvalidOperationException("User username is null."))
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }

        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            return _userService.GetUsers();
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Invalid request. Username and password are required.");
            }

            try
            {
                _userService.RegisterUser(user);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("User registered successfully.");
        }

        [HttpPut("update")]
        [Authorize]
        public IActionResult UpdateUser([FromBody] User user)
        {
            try
            {
                _userService.UpdateUser(user);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok("User updated successfully.");
        }

        [HttpDelete("delete/{id}")]
        [Authorize]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok("User deleted successfully.");
        }
    }
}