using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IMovieService
{
    // services will typically expose the business functionality to the UI/client/ controllers
    // this method will be called by home/index
    // Services will always return models
    Task<List<MovieCardModel>> GetTop30GrossingMovies();

    Task<MovieDetailsModel> GetMovieDetails(int movieId);
}