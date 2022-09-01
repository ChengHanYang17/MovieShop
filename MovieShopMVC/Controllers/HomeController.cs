using System.Diagnostics;
using ApplicationCore.Contracts.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;

namespace MovieShopMVC.Controllers;

// Controller
// Controller will communicate with Services(Business logic),
// and Services will communicate with Repositories,
// and Repositories will communicate with Database using EF Core or Dapper or both
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMovieService _movieService;

    public HomeController(ILogger<HomeController> logger, IMovieService movieService)
    {
        _logger = logger;
        _movieService = movieService;
    }

    // Action methods
    [HttpGet]
    public IActionResult Index()
    {
        // go to database and get the data
        // we want to make tightly coupled code to be loosely coupled code 
        
        var movies = _movieService.GetTop30GrossingMovies();
        
        // 3 ways we can send data from controller/ action method to views: ViewBag, ViewData, Strongly typed Models
        return View(movies);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}