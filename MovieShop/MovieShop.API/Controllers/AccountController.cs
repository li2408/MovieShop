using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Threading.Tasks;
using MovieShop.Core.Models.Request;
using MovieShop.Core.ServiceInterfaces;
using MovieShop.Core.Models.Response;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace MovieShop.API.Controllers
{
    //attribute based routing.
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser(UserRegisterRequestModel userRegisterRequestModel)
        {
            //1.check if model is valid:
            if (ModelState.IsValid)
            {
                //call the User Service:
                return Ok(userRegisterRequestModel);
            }
            //else:
            return BadRequest(new { message = "Please correct the input information" });
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = _userService.GetUserDetails(id);
            if (user != null)
            {
                return Ok();
            }

            return BadRequest(new { message = "No person is found" });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> login(LoginRequestModel loginRequestModel)
        {
            //steps: take the model as input
            //and then validate the user.
            //finally tell the user that he's logged in.
            if (ModelState.IsValid)
            {
                var user = await _userService.ValidateUser(loginRequestModel.Email, loginRequestModel.Password);
                if (user == null)
                {
                    return Unauthorized();
                }

                //success, here generate the JWT:
                var token = GenerateJWT(user);
                return Ok(new { token });
            }
            return BadRequest(new { message = "Invalid email or password"});
        }

        private string GenerateJWT(UserLoginResponseModel userLoginResponseModel)
        {
            var claims = new List<Claim> {
                     new Claim (ClaimTypes.NameIdentifier, userLoginResponseModel.Id.ToString()),
                     new Claim( JwtRegisteredClaimNames.GivenName, userLoginResponseModel.FirstName ),
                     new Claim( JwtRegisteredClaimNames.FamilyName, userLoginResponseModel.LastName ),
                     new Claim( JwtRegisteredClaimNames.Email, userLoginResponseModel.Email )
            };
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);
            var PrivateKey = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(PrivateKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var expires = DateTime.UtcNow.AddHours(72);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Audience = "MovieShop Users",
                Issuer = "MovieShop",
                SigningCredentials = credentials,
                Expires = expires
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var encodedToken = tokenHandler.CreateToken(tokenDescriptor); // create the token here.

            return tokenHandler.WriteToken(encodedToken);
        }
    }
}
