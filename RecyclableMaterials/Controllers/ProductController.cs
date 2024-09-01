using RecyclableMaterials.Areas.Dashboard.Models;
using RecyclableMaterials.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecyclableMaterials.Models;
using System;
using Microsoft.AspNetCore.Identity;
using RecyclableMaterials.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using RecyclableMaterials.Hubs;

namespace RecyclableMaterials.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly RDBContext _dbContext;
        private readonly UserManager<AppUserModel> _userManager;
        private readonly IHubContext<NotificationHub> _hubContext;


        public ProductController(RDBContext dbContext, IWebHostEnvironment webHostEnvironment, IHubContext<NotificationHub> hubContext
            , UserManager<AppUserModel> userManager)
        {
            this._dbContext = dbContext;
            this._webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _hubContext = hubContext;
        }





        // GET: ProductController
        public ActionResult HomeIndex()
        {

            return View();
        }

        public ActionResult Index(int productId)
        {
            var models = _dbContext.products.Include(x => x.Category).Include(x=>x.user)
                                                .OrderBy(x => x.Name).ToList();


            return View(models);
        }

        public ActionResult Myproduct()
        {
            var models = _dbContext.products.Include(x => x.Category)
                                                .OrderBy(x => x.Name).ToList();

            return View(models);
        }

     

        

        public async Task<IActionResult> Details(int id)
        {
            var product = await _dbContext.products
                .Include(m => m.Category)
                .Include(m => m.Comments)
                    .ThenInclude(c => c.user)
                .Include(m => m.Ratings)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

          
            var averageRating = product.Ratings.Any() ? product.Ratings.Average(r => r.Stars) : 0;

          
            var userId = _userManager.GetUserId(User);

         
            var userRating = product.Ratings.FirstOrDefault(r => r.UserId == userId)?.Stars;

          
            var viewModel = new ProductDetailsViewModel
            {
                Product = product,
                AverageRating = averageRating,
                UserRating = userRating
            };

            return View(viewModel);
        }




        [HttpPost]
        public async Task<IActionResult> AddComment(int productId, string text)
        {
            var userId = _userManager.GetUserId(User); // الحصول على معرف المستخدم الذي يقوم بالتعليق
            var product = await _dbContext.products.Include(p => p.user).FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null) return NotFound();

            var comment = new CommentModel { ProductId = productId, Text = text, UserId = userId };
            _dbContext.Comments.Add(comment);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Details", new { id = productId });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRating(int productId, int stars)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account"); 
            }

            var userId = _userManager.GetUserId(User);


            var product = await _dbContext.products.Where(p => p.ProductId == productId).FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }

        
            var rating = new RatingModel
            {
                ProductId = productId,
                Stars = stars,
                UserId = userId
            };

            _dbContext.Ratings.Add(rating);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Details", new { id = productId });
        }




        // GET: ProductController/Create
        public IActionResult Create()
        {
            ViewBag.CatList = _dbContext.Categories.ToList();
            // ViewBag.CategoryList = _dbContext.Categories;
            return View();
        }

        // POST: ProductController/Create


        [HttpPost]
        public ActionResult Create(ProductModel model, IFormFile image)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (model != null)
                {
                    if (image != null)
                    {
                        string folder = "Images/ProductsImages";
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, folder, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            image.CopyTo(stream);
                        }
                        model.ImagePath = Path.Combine(folder, fileName);

                        model.UserId = userId;
                        _dbContext.products.Add(model);
                        _dbContext.SaveChanges();
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.CatList = _dbContext.Categories;
            var productmodel = _dbContext.products.FirstOrDefault(x => x.ProductId == id);
            return View("Create", productmodel);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, ProductModel model, IFormFile Image)
        {
            try
            {
                if (model != null)
                {
                    string oldImagePath = string.Empty;
                    string oldFilePath = string.Empty;
                    var product = _dbContext.products.FirstOrDefault(x => x.ProductId == id);

                    if (Image != null)
                    {

                        string folder = "Images/ProductsImages";
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, folder, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            Image.CopyTo(stream);
                        }

                        oldImagePath = product.ImagePath;
                        oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, oldImagePath);
                        product.ImagePath = Path.Combine(folder, fileName);
                    }


                    product.Name = model.Name;
                    product.Quantity = model.Quantity;
                    product.Location = model.Location;
                    product.Price = model.Price;
                    product.CategoryID = model.CategoryID;
                    product.Discription = model.Discription;

                    _dbContext.products.Update(product);
                    var response = await _dbContext.SaveChangesAsync();

                    // delete image from folder
                    if (Convert.ToBoolean(response))
                    {
                        if (!string.IsNullOrEmpty(oldImagePath))
                        {
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }
                        }
                    }


                    return RedirectToAction(nameof(Index));

                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        private ProductModel GetProduct(int id)
        {
            var model = _dbContext.products.Where(x => x.ProductId == id).FirstOrDefault();
            if (model != null)
            {
                return model;
            }
            else
            {
                return new ProductModel();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = GetProduct(id);
            return View(model);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ProductModel product)
        {
            try
            {
                var model = GetProduct(id);
                if (model != null)
                {
                    _dbContext.products.Remove(model);

                    _dbContext.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
