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
    public class AdminController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public AdminController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        //create a movie
        [HttpPost]
        [Route("movie")]
        public async Task<IActionResult> CreateMovie(MovieCreateRequest movieCreateRequest)
        {
            var createdMovies = await _movieService.CreateMovie(movieCreateRequest);
            return Ok();
        }

        //update a movie:
        [HttpPut]
        [Route("movie")]
        public async Task<IActionResult> UpdateMovie(MovieCreateRequest movieCreateRequest)
        {
            var updatedMovie = await _movieService.UpdateMovie(movieCreateRequest);
            return Ok();
        }

        //list of all the purchases... order by datetime
        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> MoviePurchased([FromQuery] int pageSize = 30, [FromQuery] int page = 1)
        {
            var purchasedMovies = await _movieService.GetMoviesByPagination(pageSize,page);
            return Ok();
        }

        /*
        //top movies that have been purchased:
        [HttpGet]
        [Route("top")]
        public async Task<IActionResult> TopMovies()
        {
            var topMovies = _
        
        */
    }
}
