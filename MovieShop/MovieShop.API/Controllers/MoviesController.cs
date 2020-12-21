using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController: ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        //api/movies/toprevenue

        //since the controller's name is MoviesController, so it'll match with "movies" in the url
        [HttpGet] //this is a Get method
        [Route("toprevenue")] //this is a route
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            // call our service and call the method and get the data.
            // var movies = _movieService.GetTopMovies();
            // http status code
            var movies = await _movieService.GetHighestGrossingMovies();
            if (!movies.Any())
            {
                return NotFound("no Movies Found");
            }

            return Ok(movies);
        }

        [HttpGet]
        [Route("toprated")]
        public async Task<IActionResult> GetTopRated()
        {
            var movies = _movieService.GetTopRatedMovies();
            return Ok(movies);
        }

        [HttpGet]
        [Route("genre/{genreId:int}")]
        public async Task<IActionResult> GetMovieByGenre(int genreId)
        {
            var genre = _movieService.GetMoviesByGenre(genreId);
            return Ok(genre);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetMovie")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieAsync(id);
            return Ok(movie);
        }

        [HttpGet]
        [Route("{id}/reviews")]
        public async Task<IActionResult> GetReviewsByMovie(int movieId)
        {
            var reviews = _movieService.GetReviewsForMovie(movieId);
            return Ok(reviews);
        }
    }
}
