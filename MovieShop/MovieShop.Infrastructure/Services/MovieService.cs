using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieShop.Core.Models.Response;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using MovieShop.Infrastructure.Data;
using MovieShop.Infrastructure.Repositories;
using OpenQA.Selenium;

namespace MovieShop.Infrastructure.Services
{
    public class MovieService: IMovieService
    {
        private readonly IMovieRepository _repository;
        //private readonly IMapper _mapper;

        public MovieService(IMovieRepository repository)//constructor injection.
        {
            _repository = repository;
        }
        public async Task<IEnumerable<MovieResponseModel>> GetHighestGrossingMovies()
        {
            // calling Repository.
            // MovieRepository class.

            // step1: get the highest revenue data from repository
            // step2: create a list model just for the selected data to demo
            // step3: assign the selected data into each model.
            // step4: return the list of model

            var movies = await _repository.GetHighestRevenueMovies();
            // Map our Movie Entity to MovieResponseModel. // we only need the data that we want.
            var movieResponseModel = new List<MovieResponseModel>();
            foreach (var movie in movies)
            {
                movieResponseModel.Add(new MovieResponseModel
                    {
                        Id = movie.Id,
                        PosterUrl = movie.PosterUrl,
                        Title = movie.Title
                    });
            }

            return movieResponseModel;
        }

        public Task<PagedResultSet<MovieResponseModel>> GetMoviesByPagination(int pageSize = 20, int page = 0, string title = "")
        {
            throw new NotImplementedException();
        }

        public Task<PagedResultSet<MovieResponseModel>> GetAllMoviePurchasesByPagination(int pageSize = 20, int page = 0)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedList<MovieResponseModel>> GetAllPurchasesByMovieId(int movieId)
        {
            throw new NotImplementedException();
        }

       

        public Task<IEnumerable<ReviewMovieResponseModel>> GetReviewsForMovie(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetMoviesCount(string title = "")
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MovieResponseModel>> GetTopRatedMovies()
        {
            throw new NotImplementedException();
        }


        public Task<IEnumerable<MovieResponseModel>> GetMoviesByGenre(int genreId)
        {
            throw new NotImplementedException();
        }

        public Task<MovieDetailsResponseModel> CreateMovie(MovieCreateRequest movieCreateRequest)
        {
            throw new NotImplementedException();
        }

        public Task<MovieDetailsResponseModel> UpdateMovie(MovieCreateRequest movieCreateRequest)
        {
            throw new NotImplementedException();
        }



         public async Task<MovieDetailsResponseModel> GetMovieAsync(int id)
        {
            var movie = await _repository.GetByIdAsync(id);
            var model = new MovieDetailsResponseModel()
            {
                Id = movie.Id,
                Title = movie.Title,
                PosterUrl = movie.PosterUrl,
                BackdropUrl = movie.BackdropUrl,
                Overview = movie.Overview,
                Tagline = movie.Tagline,
                Budget = movie.Budget,
                Revenue = movie.Revenue,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl,
                ReleaseDate = movie.ReleaseDate,
                RunTime = movie.RunTime,
                Price = movie.Price

            };
            
            return model;
        }

         Task<IEnumerable<ReviewMovieResponseModel>> IMovieService.GetReviewsForMovie(int id)
        {
            throw new NotImplementedException();
        }

         Task<MovieDetailsResponseModel> IMovieService.CreateMovie(MovieCreateRequest movieCreateRequest)
        {
            throw new NotImplementedException();
        }

         Task<MovieDetailsResponseModel> IMovieService.UpdateMovie(MovieCreateRequest movieCreateRequest)
        {
            throw new NotImplementedException();
        }
    }
}
