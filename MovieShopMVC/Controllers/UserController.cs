using ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Infra;

namespace MovieShopMVC.Controllers;

[Authorize]  // filter put on the top so every methods have it
public class UserController : Controller
{
    private readonly ICurrentUser _currentUser;


    public UserController(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    [HttpGet]
    //[Authorize]  // filter to check 
    public async Task<IActionResult> Purchases()
    {
        // get all the movies purchased by user
        // get user id, httpcontext.user.claims and then call the database and get the information to the view
        var userId = _currentUser.UserId;
        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> Favorites()
    {
        return View();
    }
    
    [HttpGet]  // show empty view
    public async Task<IActionResult> EditProfile()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> EditProfile(UserEditModel model)
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> BuyMovie()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> FavoriteMovie()
    {
        return View();
    }
}