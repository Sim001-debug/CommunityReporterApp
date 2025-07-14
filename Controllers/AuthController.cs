using CommunityReporterApp.Data;
using CommunityReporterApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CommunityReporterApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AppDbContext context, IPasswordHasher<AppUser> passwordHasher, IConfiguration configuration, ILogger<AuthController> logger)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var existingUser = await _context.AppUsers
                .FirstOrDefaultAsync(u => u.Username == request.UserName);

            if (existingUser != null)
                return BadRequest("Username already exists.");

            var newUser = new AppUser
            {
                Username = request.UserName,
                Role = request.Role,
            };

            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, request.Password);

            _context.AppUsers.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Username and Password are required!");

            var user = await _context.AppUsers.FirstOrDefaultAsync(u => u.Username == request.UserName);

            if (user == null)
            {
                _logger.LogWarning("Failed login attempt for username: {Username} from IP: {IP}",
                    request.UserName, HttpContext.Connection.RemoteIpAddress?.ToString());
                return Unauthorized("Invalid credentials.");
            }

            _logger.LogInformation("Successful login for user: {Username}", user.Username);

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result != PasswordVerificationResult.Success)
                return Unauthorized("Invalid credentials.");

            var token = GenerateJwtToken(user);
            return Ok(new 
            {   
                token,
                id = user.Id,
                userName = user.Username,
            });
        }

        private string GenerateJwtToken(AppUser user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role), //to be "Admin"
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //to implement public-key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //added this endpoint to debug the issue of the token.
        [HttpGet("debug-token")]
        public IActionResult DebugToken()
        {
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader))
                return BadRequest("No Authorization header");

            if (!authHeader.StartsWith("Bearer "))
                return BadRequest("Invalid Authorization header format");

            var token = authHeader.Substring("Bearer ".Length);

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(token);

                return Ok(new
                {
                    Claims = jsonToken.Claims.Select(c => new { c.Type, c.Value }),
                    Expires = jsonToken.ValidTo,
                    IsExpired = jsonToken.ValidTo < DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Invalid token: {ex.Message}");
            }
        }

        [HttpGet("debug-auth")]
        public IActionResult DebugAuth()
        {
            return Ok(new
            {
                IsAuthenticated = User.Identity.IsAuthenticated,
                UserName = User.Identity.Name,
                Roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value),
                AllClaims = User.Claims.Select(c => new { c.Type, c.Value })
            });
        }
    }
}
