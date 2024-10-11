using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIDEMO_.Controllers
{
    public class LoginUserController : Controller
    {

        private readonly IConfiguration configuration;

        public LoginUserController(IConfiguration config) 
        {

            configuration = config;

        
        }


        [AllowAnonymous]
        [HttpGet("RequestToken")]
        public JsonResult RequestToken()
        {
            DateTime utcNow = DateTime.UtcNow;

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
            };

            DateTime expiredDateTime = utcNow.AddMinutes(3);

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            //Key + credentials
            var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("SecretKey"));
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(key);
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            string token = jwtSecurityTokenHandler.WriteToken(new JwtSecurityToken(claims: claims, expires: expiredDateTime, notBefore: utcNow, signingCredentials: signingCredentials));

            return new JsonResult(new { token });
        }


    }
}
