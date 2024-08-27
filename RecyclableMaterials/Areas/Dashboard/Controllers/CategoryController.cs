using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecyclableMaterials.Areas.Dashboard.Models;
using RecyclableMaterials.Data;
using System;

namespace RecyclableMaterials.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly RDBContext _dbContext;

        public CategoryController(RDBContext dbContext)
        {
            this._dbContext = dbContext;
        }


        // GET: CategoryController
        public ActionResult Index()
        {
            var CategoryList = _dbContext.Categories.ToList();
            return View(CategoryList);
        }

       
      

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]

        public ActionResult Create(CategoryModel category)
        {
            if (category != null)
            {
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }




        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _dbContext.Categories.FirstOrDefault(x => x.id == id);
            if (model != null)
            {
                return View("Create", model);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: CategoryController/Edit/5
        [HttpPost]

        public ActionResult Edit(int id, CategoryModel category)
        {
            var model = _dbContext.Categories.FirstOrDefault(x => x.id == id);
            if (model != null)
            {
                model.Name = category.Name;

                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View("Create", category);
        }



        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CategoryModel category)
        {
            var model = _dbContext.Categories.FirstOrDefault(x => x.id == id);
            if (model != null)
            {
                _dbContext.Categories.Remove(model);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
