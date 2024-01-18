using CreditScore.Models;
using CreditScore.Models.Entities;
using CreditScore.Models.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CreditScore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController: ControllerBase
    {
        readonly IBuisiness Ibusiness;
        private IConfiguration configuration;

        public LoginController(IBuisiness business, IConfiguration configuration)
        {
            Ibusiness = business;
            this.configuration = configuration;
        }

        [Authorize]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await Ibusiness.GetAllUsers();
            return response.Count > 0 ? Ok(response) : NotFound();
        }

        [Authorize]
        [HttpGet("ValidateUser/{email}/{password}")]
        public IActionResult GetUser(string email, string password)
        {
            var response = Ibusiness.AuthenticateUser(email, password);
            return Ok(response);
        }

        [HttpPost("SaveUsers")]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            var res = Ibusiness.AuthenticateUser(user.Email, user.PasswordHash);
            if (!res) {
                var response = await Ibusiness.SaveUserDetails(user);
                if (response != null) {
                    if(response)
                        return Ok(response);
                    else
                        return NotFound();
                }
                else
                    return NotFound();
            }
            else
                return NotFound();
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLogin user)
        {
            IActionResult result = Unauthorized();
            var response = Ibusiness.AuthenticateUser(user.email, user.password);
            if (response) {
                var Token = GenerateToken();
                result = Ok(new { token = Token });
            }
            return result;
        }

        private string GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer:configuration["Jwt:Issuer"],
              audience:configuration["Jwt:ValidAudience"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
