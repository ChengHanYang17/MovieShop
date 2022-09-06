using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers;

public class MoviesController : Controller
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
     public async Task<IActionResult> Details(int id) {
    
         // go to database and get the movie information by
         // movie id and send the data (Model) to the view
         // ADO.NET 
         // Dapper Stackoverflow -> Micro ORM
         // Entity Framework Core => Full ORM
    
         // Select * from Movies where id =12;
         // Code is Maintenable, Reusable, Readable, extensible, testable
         // layers => Layered architecture
         // Onion, Clean 
         // go to movie service -> movie repository and get movie details from Movies table
         
         // ASP.NET, we are making an I/O bound operation which is the outside database call, file call, stream, network
         // Thread is waiting for I/O bound operation to finish 
         var movieDetails = await _movieService.GetMovieDetails(id);
         return View(movieDetails);
     }
}