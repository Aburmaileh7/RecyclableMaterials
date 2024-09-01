using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using RecyclableMaterials.Data;
using RecyclableMaterials.Models;
using System.Security.Claims;

namespace RecyclableMaterials.Controllers
{
    public class RatingController : Controller
    {
        private readonly RDBContext _dbContext;
        private readonly UserManager<AppUserModel> _userManager;
        public RatingController(RDBContext dbContext, UserManager<AppUserModel> userManager)
        {

            _dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpPost]
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

            return Json(new { success = true, message = "THANK YOU" });
        }

        



    }
}
