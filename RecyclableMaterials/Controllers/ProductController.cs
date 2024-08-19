using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecyclableMaterials.Data;
using RecyclableMaterials.Models;
using System;

namespace RecyclableMaterials.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private  IWebHostEnvironment _webHostEnvironment;
        private  RDBContext _dbContext;

        public ProductController(RDBContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }





        // GET: ProductController
        [HttpGet]
        public ActionResult Index()
        {
            var models = _dbContext.products.Include(x => x.Category).OrderBy(x => x.Name).ToList();


            return View(models);
           
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var ProductCategoryModel = (from pro in _dbContext.products
                                        join cat in _dbContext.Categories on pro.CategoryID equals cat.id
                                        select new ProductModel
                                        {

                                            Id = pro.Id,
                                            Name = pro.Name,
                                            Location = pro.Location,
                                            Quantity = pro.Quantity,
                                            Discription = pro.Discription,
                                            ImagePath = pro.ImagePath,
                                            Price = pro.Price,
                                            CategoryID = pro.CategoryID,
                                            Category = cat
                                        }).FirstOrDefault(x => x.Id == id);
            return View(ProductCategoryModel);
        }

        // GET: ProductController/Create
        public IActionResult Create()
        {
            ViewBag.CatList = _dbContext.Categories.ToList();

            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        public ActionResult Create(ProductModel model, IFormFile image)
        {
            try
            {
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
            var productmodel = _dbContext.products.FirstOrDefault(x => x.Id == id);
            return View("Create", productmodel);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ProductModel model, IFormFile Image)
        {
            try
            {
                if (model != null)
                {
                    string oldImagePath = string.Empty;
                    string oldFilePath = string.Empty;
                    var product = _dbContext.products.FirstOrDefault(x => x.Id == id);

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
                    product.Price = model.Price;
                    product.Location = model.Location;
                    product.Quantity = model.Quantity;
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
            var model = _dbContext.products.Where(x => x.Id == id).FirstOrDefault();
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

