using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class MovieTestService : IMovieService
{
    public List<MovieCardModel> GetTop30GrossingMovies()
    {
        throw new NotImplementedException();
    }

    public MovieDetailsModel GetMovieDetails(int movieId)
    {
        throw new NotImplementedException();
    }
}