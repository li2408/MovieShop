using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieShop.Core.Models.Response;

namespace MovieShop.Core.ServiceInterfaces
{
    public interface IMovieService
    {
        public Task<PagedResultSet<MovieResponseModel>> GetMoviesByPagination(int pageSize = 20, int page = 0, string title = "");
        public Task<PagedResultSet<MovieResponseModel>> GetAllMoviePurchasesByPagination(int pageSize = 20, int page = 0);
        public Task<PaginatedList<MovieResponseModel>> GetAllPurchasesByMovieId(int movieId);
        public Task<MovieDetailsResponseModel> GetMovieAsync(int id);
        public Task<IEnumerable<ReviewMovieResponseModel>> GetReviewsForMovie(int id);
        public Task<int> GetMoviesCount(string title = "");
        public Task<IEnumerable<MovieResponseModel>> GetTopRatedMovies();
        public Task<IEnumerable<MovieResponseModel>> GetHighestGrossingMovies();
        public Task<IEnumerable<MovieResponseModel>> GetMoviesByGenre(int genreId);
        public Task<MovieDetailsResponseModel> CreateMovie(MovieCreateRequest movieCreateRequest);
        public Task<MovieDetailsResponseModel> UpdateMovie(MovieCreateRequest movieCreateRequest);

    }

    public class MovieCreateRequest
    {
    }

    public class PaginatedList<T>
    {
    }

    public class PagedResultSet<T>
    {
    }
}
