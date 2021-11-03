using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SearchingOMDB.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingOMDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MovieDAL movieDAL = new MovieDAL();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MovieSearch()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MovieSearch(string Title = "")
        {
            MovieModel movieModel = new MovieModel();
            if (Title != "")
            {
                movieModel = movieDAL.ConvertJSONtoSingleTitleMovieModel(Title);
                return View(movieModel);
            }
            else
            {
                return RedirectToAction("MovieSearch");
            }
        }

        public IActionResult MovieNight()
        {

            return View();
        }

        [HttpPost]
        public IActionResult MovieNight(string title1, string title2, string title3)
        {
            if (string.IsNullOrEmpty(title1) && string.IsNullOrEmpty(title2) && string.IsNullOrEmpty(title3))
            {
                return View();
            }
            List<string> movieList = new List<string>
            {
                new string(title1),
                new string(title2),
                new string(title3)
            };
            List<MovieModel> movieResult = new List<MovieModel>();

            if (movieList != null)
            {
                foreach (string movie in movieList)
                {
                    var movieDetails = movieDAL.ConvertJSONtoSingleTitleMovieModel(movie);
                    movieResult.Add(movieDetails);
                }

                return View(movieResult);
            }
            else
            {
                return RedirectToAction("MovieNight");
            }
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
