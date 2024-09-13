using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecyclableMaterials.Areas.Dashboard.ViewModel;
using RecyclableMaterials.Data;
using RecyclableMaterials.Models;

namespace RecyclableMaterials.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly RDBContext _dbContext;
        private readonly UserManager<AppUserModel> _userManager;

        public HomeController(RDBContext dbContext, UserManager<AppUserModel> userManager)
        {
            this._dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            int productCount = _dbContext.products.Count();
            int myproductCount = _dbContext.products
                                               .Where(p => p.UserId == userId).Count();

            var models = _dbContext.products.Include(x => x.Category).OrderBy(x => x.Name).ToList();
            ViewBag.ProductCount = productCount;
            ViewBag.MyProductCount = myproductCount;


            var user = await _userManager.FindByIdAsync(userId);


            var viewModel = new UserProductViewModel
            {
                User = user,
                Products = models
            };
  
            return View(viewModel);
        }
    }
}
