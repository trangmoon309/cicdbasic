using DevopsAssignment.Database;
using DevopsAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DevopsAssignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _imagePath = Path.Combine(Directory.GetCurrentDirectory(), "images");
        private readonly ILogger<HomeController> _logger;
        private MyDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, MyDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var products = _dbContext.Products.ToList();
            if (products.Count() < 20) {
                products.Add(new Product
                {
                    Id = 1,
                    Name = "dog",
                    ImageUrl = "images/dog.png"
                });
            }
            return View(products);
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
