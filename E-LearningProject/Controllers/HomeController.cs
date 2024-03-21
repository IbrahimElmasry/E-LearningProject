using E_LearningProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace E_LearningProject.Controllers
{
    public class HomeController : Controller

    {
        public ApplicationDBContext _context = new ApplicationDBContext();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var query = _context.Categories;

            return View(query);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {

            return View();
        }
        public IActionResult Mosh()
        {
            return View();
        }
        public IActionResult Osama()
        {
            return View();
        }
        public IActionResult David()
        {
            return View();
        }
        public IActionResult Andre()
        {
            return View();
        }
        public IActionResult LearningPaths()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Fundementals()
        {
            return View();
        }
        public  IActionResult Front()
        {
            return View();
        }
        public IActionResult Back()
        {
            return View();
        }
        public IActionResult Mobile()
        {
            return View();
        }
        public IActionResult Game()
        {
            return View();
        }
        public IActionResult Book()
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
