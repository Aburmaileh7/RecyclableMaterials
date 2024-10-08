﻿using RecyclableMaterials.Areas.Dashboard.Models;
using RecyclableMaterials.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecyclableMaterials.Models;
using System;
using Microsoft.AspNetCore.Identity;
using RecyclableMaterials.ViewModels; 
using RecyclableMaterials.Services;
using Microsoft.CodeAnalysis;


namespace RecyclableMaterials.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly RDBContext _dbContext;
        private readonly UserManager<AppUserModel> _userManager;
        private readonly INotificationService _notificationService;



        public ProductController(RDBContext dbContext, IWebHostEnvironment webHostEnvironment
            , UserManager<AppUserModel> userManager, INotificationService notificationService)
        {
            this._dbContext = dbContext;
            this._webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _notificationService = notificationService;

        }




        #region BaseAction
        // GET: ProductController
        //[ActionFilter]
        public ActionResult HomeIndex()
        {

            return View();
        }


        //[ActionFilter]
        public ActionResult Index(int productId)
        {
            var models = _dbContext.products.Include(x => x.Category).Include(x=>x.user)
                                                .OrderBy(x => x.Name).ToList();


            return View(models);
        }

        //[ActionFilter]
        public ActionResult Myproduct()
        {
            var models = _dbContext.products.Include(x => x.Category)
                                                .OrderBy(x => x.Name).ToList();

            var userId = _userManager.GetUserId(User);
            int myproductCount = _dbContext.products.Where(p => p.UserId == userId).Count();
            ViewBag.MyProductCount = myproductCount;

            var reservationsCount= _dbContext.Reservations.Include(r =>r.Product)
                .Where(r => r.UserId == userId && r.Status=="Pending").Count();

            ViewBag.PendingReservations = reservationsCount;

            return View(models);
        }



        // GET: ProductController/Create
        //[ActionFilter]
        public IActionResult Create()
        {
            ViewBag.CatList = _dbContext.Categories.ToList();
            return View();
        }

        // POST: ProductController/Create


        [HttpPost]
        //[ActionFilter]
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
        //[ActionFilter]
        public ActionResult Edit(int id)
        {
            ViewBag.CatList = _dbContext.Categories;
            var productmodel = _dbContext.products.FirstOrDefault(x => x.ProductId == id);
            return View("Create", productmodel);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        //[ActionFilter]
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
        //[ActionFilter]
        public ActionResult Delete(int id)
        {
            var model = GetProduct(id);
            return View(model);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ActionFilter]
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

        //[ActionFilter]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _dbContext.products
                .Include(m => m.Category).Include(m => m.Ratings).Include(m => m.user)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            
           
          
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
        #endregion

        #region comments&Ratting
        public async Task<IActionResult> ViewComment(int id)
        {
            var product = await _dbContext.products
                .Include(p => p.Comments)
                    .ThenInclude(c => c.user)
                .FirstOrDefaultAsync(p => p.ProductId == id);



            var commentViewModels = product.Comments.Select(c => new CommentProductViewModel
            {
                Product = product,
                CommentText = c.Text,
                FirstName=c.user.FirstName,
                LastName=c.user.LastName,
                UserEmail = c.user.Email,
                ProfilePictureUrl = c.user.ProfilePictureUrl

            }).ToList();

            

            return View(commentViewModels);
        }


            //[ActionFilter]
        public async Task<IActionResult> AddComment(int productId, string text)
        {
            var userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(text))
            {
                return BadRequest("Comment cannot be empty");
            }

            var comment = new CommentModel
            {

                ProductId = productId,
                Text = text,
                CreateAt = DateTime.Now,
                UserId = userId
            };

            _dbContext.Comments.Add(comment);
            await _dbContext.SaveChangesAsync();

            // جلب صاحب المنتج لإرسال إشعار له
            var product = await _dbContext.products.Include(p => p.user).FirstOrDefaultAsync(p => p.ProductId == productId);
            if (product != null)
            {
                var ownerUserId = product.UserId;

                if (ownerUserId != userId)
                {
                    var notificationMessage = $" '{product.Name}' get new comment.";
                    _notificationService.SendNotificationComment(ownerUserId, notificationMessage);
                }
            }

            return RedirectToAction("ViewComment", "Product", new { id = productId }); 
        }



        [HttpPost]
        //[ActionFilter]
        public async Task<IActionResult> AddRating(int productId, int stars)
        {


            var userId = _userManager.GetUserId(User);

            var existingRating = await _dbContext.Ratings
                .FirstOrDefaultAsync(r => r.ProductId == productId && r.UserId == userId);

            if (existingRating != null)
            {
                existingRating.Stars = stars; // تحديث التقييم إذا كان موجودًا
            }
            else
            {
                var rating = new RatingModel
                {
                    ProductId = productId,
                    Stars = stars,
                    UserId = userId
                };
                _dbContext.Ratings.Add(rating); // إضافة تقييم جديد إذا لم يكن موجودًا
            }

            await _dbContext.SaveChangesAsync();


            // جلب صاحب المنتج لإرسال إشعار له
            var product = await _dbContext.products.Include(p => p.user).FirstOrDefaultAsync(p => p.ProductId == productId);
            if (product != null)
            {
                var ownerUserId = product.UserId;

                if (ownerUserId != userId)
                {
                    var notificationMessage = $"'{product.Name}' get new rating.";
                    _notificationService.SendNotificationRating(ownerUserId, notificationMessage);
                }
            }

            return Json(new { success = true, message = "THANK YOU" });
        }

        #endregion


        #region Request

        public ActionResult ManageRequest()
        {
            var reservations = _dbContext.Reservations
                .Include(r => r.Product).Include(r => r.User)
                .Where(r => r.Status == "Pending") 
                .OrderBy(r => r.ReservationDate) 
                .ToList();

            var userId = _userManager.GetUserId(User);
            int myproductCount = _dbContext.products.Where(p => p.UserId == userId).Count();
            ViewBag.MyProductCount = myproductCount;

            var reservationsCount = _dbContext.Reservations.Include(r => r.Product)
                .Where(r => r.UserId == userId && r.Status == "Pending").Count();

            ViewBag.PendingReservations = reservationsCount;

            return View(reservations);
        }


        [HttpPost]
        public async Task<IActionResult> RequestReservation(int productId)
        {

            var userId = _userManager.GetUserId(User);

            var reservation = new ReservationModel
            {
                ProductId = productId,
                UserId = userId,
                Status = "Pending",
                ReservationDate = DateTime.Now
            };

            _dbContext.Reservations.Add(reservation);
            await _dbContext.SaveChangesAsync();

            // جلب صاحب المنتج لإرسال إشعار له
            var product = await _dbContext.products.Include(p => p.user).FirstOrDefaultAsync(p => p.ProductId == productId);
            if (product != null)
            {
                var ownerUserId = product.UserId;

                if (ownerUserId != userId)
                {
                    var notificationMessage = $" '{product.Name}' get new Request.";
                    _notificationService.SendNotificationRequest(ownerUserId, notificationMessage);
                }
            }

            return RedirectToAction("Details", "Product", new { id = productId });
        }


        [HttpPost]
        public async Task<IActionResult> ApproveReservation(int reservationId)
        {
            var reservation = await _dbContext.Reservations
                .Include(r => r.Product).Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == reservationId);

            if (reservation == null)
            {
                return NotFound();
            }


            var ownerId = _userManager.GetUserId(User);
            if (reservation.Product.UserId != ownerId)
            {
                return Unauthorized();
            }


            reservation.Status = "Approved";
            await _dbContext.SaveChangesAsync();


            var userId = reservation.UserId;
            var productName = reservation.Product.Name;

            // إنشاء رسالة الإشعار
            var notificationMessage = $"Reservation for '{productName}' has been approved.";
            // إرسال الإشعار
            _notificationService.SendNotificationRating(userId, notificationMessage);

            return RedirectToAction("ManageRequest");
        }

        [HttpPost]
        public async Task<IActionResult> RejectReservation(int reservationId)
        {
            var reservation = await _dbContext.Reservations
                .Include(r => r.Product).Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == reservationId);

            if (reservation == null)
            {
                return NotFound();
            }


            var ownerId = _userManager.GetUserId(User);
            if (reservation.Product.UserId != ownerId)
            {
                return Unauthorized();
            }


            reservation.Status = "Rejected";
            await _dbContext.SaveChangesAsync();


            var userId = reservation.UserId;
            var productName = reservation.Product.Name;

            // إنشاء رسالة الإشعار
            var notificationMessage = $"Reservation for '{productName}' has been rejected.";

            // إرسال الإشعار
            _notificationService.SendNotificationRating(userId, notificationMessage);

            return RedirectToAction("ManageRequest");
        }




         #endregion

    }
}
