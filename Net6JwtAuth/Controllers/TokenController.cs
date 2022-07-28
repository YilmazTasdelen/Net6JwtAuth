using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Net6JwtAuth.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Net6JwtAuth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : Controller
    {
        public IConfiguration _configuration;

        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login( LoginModel user)
        {
            if (user is null)
            {
                return BadRequest("Invalid client request");
            }
            //if (user.UserName == "johndoe" && user.Password == "def@123")
            //{
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:7276",
                    audience: "https://localhost:7276",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new AuthenticatedResponse { Token = tokenString });
            //}
           // return Unauthorized();
        }
    }

}

