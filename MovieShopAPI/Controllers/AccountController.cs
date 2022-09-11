using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            var user = await _accountService.RegisterUser(model);
            return Ok(user);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            var user = await _accountService.ValidateUser(model);

            if (user != null)
            {
                // create token
                var jwtToken = CreateJwtToken(user);
                return Ok(new { token = jwtToken });
            }
            // token, JWT(Jason Web Token)
            // client will send email/password to API, POST
            // API will validate the email/password and if valid then API will create the JWT token and send to client
            // It's client's responsibility to store the token somewhere
            // Angular, React(localstorage or sessionstorage in browser)
            // iOS/Android device
            // When client needs some secure information or needs to perform any operation that
            // requires users to be authenticated then client needs to send the token to the API in the Http Header
            // Once the API receives the token from client it will validate the JWT token and
            // if valid it will send the data back to the client
            // If JWT token is invalid or expired then APi will send 401 Unauthorized
            throw new UnauthorizedAccessException("Please check email and password");
            return Unauthorized(new {errorMessage ="Please check email and password"});
        }

        private string CreateJwtToken(UserLoginSuccessModel user)
        {
            // create the claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim("Country", "USA"),
                new Claim("Language", "English")
            };

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);
            
            // specify a secret key
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["secretKey"]));
            
            // specify the algorithm
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            
            // specify the expiration of the token
            var tokenExpiration = DateTime.UtcNow.AddHours(2);
            
            // create and object with all the above information so create the token
            var tokenDetails = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = tokenExpiration,
                SigningCredentials = credentials,
                Issuer = "MovieShop, Inc",
                Audience = "MovieShop Clients"
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var encodedJwt = tokenHandler.CreateToken(tokenDetails);
            return tokenHandler.WriteToken(encodedJwt);
        }
    }
}
