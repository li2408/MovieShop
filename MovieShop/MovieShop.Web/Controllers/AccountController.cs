using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using MovieShop.Core.Models.Request;
using MovieShop.Core.Models.Response;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService) // constructor injection.
        {
            _userService = userService;
        }

        //http://localhost/account/register GET
        [HttpGet]
        //we need to show empty register page
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpGet]
        [Authorize] //this is called filter in MVC: do some create Pre-action and Post-action logic.
        //it's gonna check using the default scheme in Startup about cookie
        public async Task<IActionResult> MyAccount()
        {
            //what does this method should be doing?
            //we can only execute this method only if the user is logged in.
            //so we need to check the cookie is present and not expired.

            return View();
        }


        //when user hits submit button,we post info to this method
        // http:localhost/account/register POST
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel model)
        {
            //step1: test if the data we receive is valid
            //step2: send the model to our service (since this is HttpPost)
            //step3: return View()

            //first thing we have to make sure the data we receive is valid:
            //only when each and every validation is true then we can proceed further
            if (ModelState.IsValid)
            {
                //we need to send the UserRegisterRequestModel to our service
                await _userService.CreateUser(model);
            }

            return View();//this View is binded to Register.cshtml, which is created just for this method.
            //Register.cshtml will take the model as the input
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        public async Task<IActionResult> Login(LoginRequestModel loginRequest, string returnUrl = null)
        {

            returnUrl ??= Url.Content("~/");
            if (!ModelState.IsValid) return View();
            var user = await _userService.ValidateUser(loginRequest.Email, loginRequest.Password);//pass the data from model to service method

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }

            //only when un/pw was success the code will execute.
            //store the claims into the cookie:
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname,  user.LastName),
                new Claim(ClaimTypes.NameIdentifier,  user.Id.ToString())
            };

            //this is for creating the cookie
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return LocalRedirect(returnUrl);//go back to the home page
        }
    }
}
