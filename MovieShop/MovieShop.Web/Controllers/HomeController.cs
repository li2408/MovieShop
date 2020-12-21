using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService _movieService;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        public async Task<IActionResult> Index()//the default page when we join the web.
        {
           _logger.LogInformation("Index method called");
           var topRevenueMovies = await _movieService.GetHighestGrossingMovies();
           return View(topRevenueMovies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
