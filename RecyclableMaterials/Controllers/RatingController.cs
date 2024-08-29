using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecyclableMaterials.Data;
using RecyclableMaterials.Models;

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

            if (stars < 1 || stars > 5)
            {
                return BadRequest("Rating value must be between 1 and 5");
            }

            // إنشاء تقييم جديد
            var rating = new RatingModel
            {
                ProductId = productId,
                Stars = stars,
                CreateAt = DateTime.Now,
                UserId = userId
            };

            // إضافة التقييم إلى قاعدة البيانات
            _dbContext.Ratings.Add(rating);

            // الحصول على المنتج المرتبط
            var product = await _dbContext.products.Include(p => p.user) // إذا كان لديك علاقة بين المنتج والمستخدم
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product != null && product.UserId != userId)
            {
                var notification = new NotificationModel
                {
                    UserId = product.UserId,                    
                    Message = $"New Rating  added to Material '{product.Name}' by {User.Identity.Name}",
                    CreatedAt = DateTime.Now,
                    IsRead = false,
                    Type = "Rating",
                    IconUrl = "/images/rating-icon.png" // رابط الشعار
                };

                _dbContext.Notifications.Add(notification);
            }

            // حفظ التغييرات في قاعدة البيانات
            await _dbContext.SaveChangesAsync();

            // إعادة التوجيه إلى تفاصيل المنتج
            return RedirectToAction("Details", "Product", new { id = productId });
        }
    }
}
