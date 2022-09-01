using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MovieRepository : IMovieRepository
{
    public readonly MovieShopDbContext _movieShopDbContext;

    public MovieRepository(MovieShopDbContext dbContext)
    {
        _movieShopDbContext = dbContext;
    }
    public Movie GetById(int id)
    {
        // select * from movie where id = 1 join genre, cast, moviegenre, moviecast
        var movieDetails = _movieShopDbContext.Movies
            .Include(m => m.GenresOfMovie).ThenInclude(m => m.Genre)
            .Include(m => m.CastsOfMovie).ThenInclude(m => m.Cast)
            .Include(m => m.Trailers)
            .FirstOrDefault(m => m.Id == id);
        return movieDetails;
    }
    public List<Movie> GetTop30GrossingMovies()
    {
        // we need to go to database and get 30 top movies from movies table

        var movies = new List<Movie>
        {
            new Movie { Id=1, Title="Inception", PosterUrl="https://image.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg" },
            new Movie { Id=2, Title="Interstellar", PosterUrl="https://image.tmdb.org/t/p/w342//gEU2QniE6E77NI6lCU6MxlNBvIx.jpg" },
            new Movie { Id=3, Title="The Dark Knight", PosterUrl="https://image.tmdb.org/t/p/w342//qJ2tW6WMUDux911r6m7haRef0WH.jpg" },
            new Movie { Id=4, Title="Deadpool", PosterUrl="https://image.tmdb.org/t/p/w342//yGSxMiF0cYuAiyuve5DA6bnWEOI.jpg" },
            new Movie { Id=5, Title="The Avengers", PosterUrl="https://image.tmdb.org/t/p/w342//RYMX2wcKCBAr24UyPD7xwmjaTn.jpg" },
            new Movie { Id=6, Title="Avatar", PosterUrl="https://image.tmdb.org/t/p/w342//6EiRUJpuoeQPghrs3YNktfnqOVh.jpg" },
            new Movie { Id=7, Title="Guardians of the Galaxy", PosterUrl="https://image.tmdb.org/t/p/w342//r7vmZjiyZw9rpJMQJdXpjgiCOk9.jpg" },
            new Movie { Id=8, Title="Fight Club", PosterUrl="https://image.tmdb.org/t/p/w342//8kNruSfhk5IoE4eZOc4UpvDn6tq.jpg" },
            new Movie { Id=9, Title="Avengers: Infinity War", PosterUrl="https://image.tmdb.org/t/p/w342//7WsyChQLEftFiDOVTGkv3hFpyyt.jpg" },
            new Movie { Id=10, Title="Pulp Fiction", PosterUrl="https://image.tmdb.org/t/p/w342//plnlrtBUULT0rh3Xsjmpubiso3L.jpg" },
            new Movie { Id=11, Title="Django Unchained", PosterUrl="https://image.tmdb.org/t/p/w342//7oWY8VDWW7thTzWh3OKYRkWUlD5.jpg" },
            new Movie { Id=12, Title="Iron Man", PosterUrl="https://image.tmdb.org/t/p/w342//78lPtwv72eTNqFW9COBYI0dWDJa.jpg" }

        };
        return movies;
    }
}