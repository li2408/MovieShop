using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Infrastructure.Services;
using Moq;
using MovieShop.Core.Models.Response;

namespace MovieShop.UnitTests
{
    [TestClass]
    public class MovieServiceUnitTest//gonna test all the methods in MovieService class
    {
        private MovieService _sut;
        private List<Movie> _movies;
        private Mock<IMovieRepository> _mockMovieRepository;//will create a class and implement this interface for us.

        [TestInitialize]
        //[OneTimeSetup] in nUnit
        public void OneTimeSetup()
        {
            _movies = new List<Movie>() // our fake repository for testing.
            {
                new Movie {Id = 1, Title = "Avengers: Infinity War", Budget = 1200000},
                new Movie {Id = 2, Title = "Avatar", Budget = 1200000},
                new Movie {Id = 3, Title = "Star Wars: The Force Awakens", Budget = 1200000},
                new Movie {Id = 4, Title = "Titanic", Budget = 1200000},
                new Movie {Id = 5, Title = "Inception", Budget = 1200000},
                new Movie {Id = 6, Title = "Avengers: Age of Ultron", Budget = 1200000},
                new Movie {Id = 7, Title = "Interstellar", Budget = 1200000},
                new Movie {Id = 8, Title = "Fight Club", Budget = 1200000},
                new Movie {Id = 9, Title = "The Lord of the Rings: The Fellowship of the Ring", Budget = 1200000},
                new Movie {Id = 10, Title = "The Dark Knight", Budget = 1200000},
                new Movie {Id = 11, Title = "The Hunger Games", Budget = 1200000},
                new Movie {Id = 12, Title = "Django Unchained", Budget = 1200000},
                new Movie {Id = 13, Title = "The Lord of the Rings: The Return of the King", Budget = 1200000},
                new Movie {Id = 14, Title = "Harry Potter and the Philosopher's Stone", Budget = 1200000},
                new Movie {Id = 15, Title = "Iron Man", Budget = 1200000},
                new Movie {Id = 16, Title = "Furious 7", Budget = 1200000}
            };

        }

        [ClassInitialize]
        public void SetUp() // in here we setup the mockRepository to use when someone calls the methods
        {
            _mockMovieRepository = new Mock<IMovieRepository>();

            //now we need to setup the methods for this mock repository.
            //we are telling the mockRepository that: whenever someone calls this GetHighestRevenueMovies() method,
            //please go ahead and return this list of movies.
            _mockMovieRepository.Setup(m => m.GetHighestRevenueMovies()).ReturnsAsync(_movies);//we have all methods from IMovieRepository
            
            //injecting a class that implements the interface IMovieRepository
            _sut = new MovieService(_mockMovieRepository.Object);//Arrange
        }

        [TestMethod]
        public async Task TestListOfHighestGrossingMoviesFromFakeData()//should be concrete so other people understand easily
        {
            //need to call GetHighestGrossingMovies() from MovieService
            //SUT system under Test MovieService -> GetHighestGrossingMovies()

            //Arrange:
            //_sut = new MovieService(new MockMovieRepository());

            //Act:calling the actual method we are testing.
            var movies = await _sut.GetHighestGrossingMovies();

            //check the actual output with expected data.
            //AAA
            //Arrange, Act and Assert. These are 3 things you need to follow in Unit Test

            //Assert: Check the expected value with the actual value, and compare them. We can do multiple values
            Assert.IsNotNull(movies);
            Assert.IsInstanceOfType(movies,typeof(IEnumerable<MovieResponseModel>));
            Assert.AreEqual(16,movies.Count());

        }
    }

    public class MockMovieRepository: IMovieRepository
    {
        public Task<Movie> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> ListAsync(Expression<Func<Movie, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync(Expression<Func<Movie, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetExistsAsync(Expression<Func<Movie, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> AddAsync(Movie entity)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> UpdateAsync(Movie entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Movie entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> ListAllWithIncludesAsync(Expression<Func<Movie, bool>> @where, params Expression<Func<Movie, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetTopRatedMovies()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> GetHighestRevenueMovies()
        {
            //this method will get the fake data.
            var _movies = new List<Movie>
            {
                new Movie {Id = 1, Title = "Avengers: Infinity War", Budget = 1200000},
                new Movie {Id = 2, Title = "Avatar", Budget = 1200000},
                new Movie {Id = 3, Title = "Star Wars: The Force Awakens", Budget = 1200000},
                new Movie {Id = 4, Title = "Titanic", Budget = 1200000},
                new Movie {Id = 5, Title = "Inception", Budget = 1200000},
                new Movie {Id = 6, Title = "Avengers: Age of Ultron", Budget = 1200000},
                new Movie {Id = 7, Title = "Interstellar", Budget = 1200000},
                new Movie {Id = 8, Title = "Fight Club", Budget = 1200000},
                new Movie {Id = 9, Title = "The Lord of the Rings: The Fellowship of the Ring", Budget = 1200000},
                new Movie {Id = 10, Title = "The Dark Knight", Budget = 1200000},
                new Movie {Id = 11, Title = "The Hunger Games", Budget = 1200000},
                new Movie {Id = 12, Title = "Django Unchained", Budget = 1200000},
                new Movie {Id = 13, Title = "The Lord of the Rings: The Return of the King", Budget = 1200000},
                new Movie {Id = 14, Title = "Harry Potter and the Philosopher's Stone", Budget = 1200000},
                new Movie {Id = 15, Title = "Iron Man", Budget = 1200000},
                new Movie {Id = 16, Title = "Furious 7", Budget = 1200000}
            };
            return _movies;
        }
    }

}
