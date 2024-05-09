using ECommerceApp.Model;
using ECommerceAPI.ECommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace ECommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpGet]
        public async Task<ActionResult<List<Users>>> GetUser()
        {
            try
            {
                var users = await _userService.GetUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while getting users");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var user = await _userService.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password);

                if (user != null)
                {
                    var token = GenerateJwtToken(user);
                    return Ok(new { Id = user.Id, Token = token });
                }
                else
                {
                    return BadRequest("Invalid email or password");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while logging in");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Users>> AddUser(Users user)
        {
            try
            {
                var result = await _userService.AddUser(user);
                if (result > 0)
                {
                    return Ok("User added successfully");
                }
                else
                {
                    return BadRequest("Failed to add user");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while adding user");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            try
            {
                var result = await _userService.DeleteUser(userId);
                if (result)
                {
                    return Ok("User deleted successfully");
                }
                else
                {
                    return BadRequest("User with this id does not exist");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while deleting user");
                return StatusCode(500, "Internal server error");

            }
        }

        private string GenerateJwtToken(Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
