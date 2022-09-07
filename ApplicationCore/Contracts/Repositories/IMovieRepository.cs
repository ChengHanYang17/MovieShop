using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repositories;

public interface IMovieRepository
{
    // here will have CRUD methods, e.g. get top 30 grossing movies from database
    Task<List<Movie>>GetTop30GrossingMovies();
    Task<Movie> GetById(int movieId);

    Task<PagedResultSet<Movie>>GetMoviesByGenrePagination(int genreId, int pageSize = 30, int page = 1);
}