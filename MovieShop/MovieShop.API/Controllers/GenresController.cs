using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : Controller
    {
        private readonly IGenreService _genreService;
        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        //get all the genres:
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetGenres() {
            var genres = await _genreService.GetAllGenres();
            if (genres == null)
            {
                return NotFound("no genres Found");
            }
            return Ok(genres);
        }
    }
}
