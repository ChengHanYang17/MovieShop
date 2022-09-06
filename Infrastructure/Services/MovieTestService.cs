using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class MovieTestService : IMovieService
{
    public async Task<List<MovieCardModel>> GetTop30GrossingMovies()
    {
        throw new NotImplementedException();
    }

    public async Task<MovieDetailsModel> GetMovieDetails(int movieId)
    {
        throw new NotImplementedException();
    }
}