
using ECommerceApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using ECommerceAPI.ECommerce.Services.Interfaces;

namespace ECommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        IConfiguration _configuration;
        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
  

        [HttpGet]
        public async Task<ActionResult<List<Users>>> GetUser()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await _userService.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password);

            if (user != null)
            {
                
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var Sectoken = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                    _configuration["Jwt:Issuer"],
                    null,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials);

                var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);


                return Ok(new { Id = user.Id, Token = token });
            }
            else
            {
                return BadRequest("Invalid email or password");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Users>> AddUser(Users user)
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

        [HttpDelete("{userId}")]
        public async Task<ActionResult> DeleteUser(int userId)
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
    }
}
