using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IMovieRepository
{
    // here will have CRUD methods, e.g. get top 30 grossing movies from database
    List<Movie>GetTop30GrossingMovies();
}