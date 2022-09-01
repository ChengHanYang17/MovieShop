using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IMovieService
{
    // services will typically expose the business functionality to the UI/client/ controllers
    // this method will be called by home/index
    // Services will always return models
    List<MovieCardModel> GetTop30GrossingMovies();

    MovieDetailsModel GetMovieDetails(int movieId);
}