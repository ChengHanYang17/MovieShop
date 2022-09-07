using System.Security.Claims;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginModel model)
    {
        var userSuccess = await _accountService.ValidateUser(model);
        if (userSuccess != null && userSuccess.Id > 0)
        {
            // after successful auth
            // create claims(userid, email, firstname, lastname)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, userSuccess.Email),
                new Claim(ClaimTypes.NameIdentifier, userSuccess.Id.ToString()),
                new Claim(ClaimTypes.Surname, userSuccess.LastName),
                new Claim(ClaimTypes.GivenName, userSuccess.FirstName),
                new Claim("language", "english")
            };
            
            // create identity object
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        
            // create cookie with some expiration time
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
            
            // Password matches, redirect to home page
            return LocalRedirect("~/");
        }
       
        
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Login");
    }

    public IActionResult Register()
    {
        // showing empty register page view
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserRegisterModel model)  //make this async but not line 13
                                                                        //because this method is doing I/O bound operation, line 13 just showing empty view
    {
        var userId = await _accountService.RegisterUser(model);

        if (userId>0)
        {
            // redirect to login page
            return RedirectToAction("Login");
        }

        return View();
    }
}