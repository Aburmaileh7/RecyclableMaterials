using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecyclableMaterials.Data;
using RecyclableMaterials.Models;
using System.Diagnostics;

namespace RecyclableMaterials.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RDBContext _dbContext;

      
        public HomeController(ILogger<HomeController> logger, RDBContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var models = _dbContext.products.Include(x => x.Category)
                                                 .OrderBy(x => x.Name).ToList();


            return View(models);
        }
        
        public IActionResult Restaurants()
        {
            return View();
        }
        public IActionResult Medical()
        {
            return View();
        }
        public IActionResult Stores()
        {
            return View();
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
