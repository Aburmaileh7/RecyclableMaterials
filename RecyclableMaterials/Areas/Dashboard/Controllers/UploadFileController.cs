using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using RecyclableMaterials.Areas.Dashboard.Models;
using RecyclableMaterials.Data;
using RecyclableMaterials.Models;
using static NuGet.Packaging.PackagingConstants;


namespace RecyclableMaterials.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    //[Authorize(Roles = "Admin")]
    public class UploadFileController : Controller
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly RDBContext _dbContext;

        public UploadFileController(RDBContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            this._dbContext = dbContext;
            this._webHostEnvironment = webHostEnvironment;

        }



        // GET: Controller
        [HttpGet]
        public ActionResult UploadFile()
        {
            var imageList = _dbContext.Images.ToList();
            return View(imageList);
        }



        [HttpPost]
        public async Task<IActionResult> UploadFile(ImageModel model, IFormFile file)
        {
            if (model != null)
            {

                if (file == null || file.Length == 0)
                {
                    return BadRequest("No File uploaded.");
                }

                string folder = "wwwroot/uploads/images";
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                //string filePath = Path.Combine(_webHostEnvironment.WebRootPath, folder, fileName);
                var FilePath = Path.Combine(Directory.GetCurrentDirectory(), folder, fileName);

                using (var strem = new FileStream(FilePath, FileMode.Create))
                {
                    await file.CopyToAsync(strem);
                }
                model.ImagePath = Path.Combine("uploads/images", fileName); 


                _dbContext.Images.Add(model);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(UploadFile));
            }
            return View();
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 104857600;
            });
        }
        private ImageModel Getimage(int id)
        {
            var model = _dbContext.Images.Where(x => x.Id == id).FirstOrDefault();
            if (model != null)
            {
                return model;
            }
            else
            {
                return new ImageModel();
            }
        }

        public ActionResult Delete(int id)
        {
            var model = Getimage(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ImageModel image)
        {
            try
            {
                var model = Getimage(id);
                if (model != null)
                {
                    _dbContext.Images.Remove(model);

                    _dbContext.SaveChanges();
                }

                return RedirectToAction(nameof(UploadFile));
            }
            catch
            {
                return View();
            }
        }
    }
}

