using DistributorStore.Base.Response;
using DistributorStore.Base.Token;
using DistributorStore.Data.ApplicationDbContext;
using DistributorStore.Data.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DistributorStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly DistributorStoreDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private string _secret;
        public LoginController(DistributorStoreDbContext dbcontext,IConfiguration configuration) { 

          _dbContext = dbcontext;
            _configuration = configuration;
            _secret = _configuration["JwtConfig:Secret"];

        }

        [HttpPost("login")]
        public IActionResult Login(User model)
        {
            
            var user = _dbContext.Users.SingleOrDefault(u => u.UserName == model.UserName && u.Password == model.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            
            //getting the user's role to authenticate
            var role = user.Role; 

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId",user.UserID.ToString()),
                    new Claim("UserName", user.UserName),
                    new Claim(ClaimTypes.Role, user.Role.ToString()), // Add the user's role as a claim
                    new Claim("Password",user.Password)

                }),
                Issuer = _configuration["JwtConfig:Issuer"],
                Audience = _configuration["JwtConfig:Audience"],
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new {Token = tokenString});
        }
    }
}

