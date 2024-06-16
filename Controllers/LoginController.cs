using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Task_API.AccessLayer;
using Task_API.Models;

namespace Task_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly Savedata _saveInfo;
        private readonly IConfiguration _configuration;

        public LoginController(Savedata saveInfo, IConfiguration configuration)
        {
            _saveInfo = saveInfo;
            _configuration = configuration;
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] Login_model loginModel)
        {
            var user = _saveInfo.AuthenticateUser(loginModel);
                
            if (user == null)
                return BadRequest(new { message = "Invalid credentials" });

            var token = GenerateJwtToken(user);

            return Ok(new { token, role = user.Role });
        }

        private string GenerateJwtToken(Signup_Model user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role)
        }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
