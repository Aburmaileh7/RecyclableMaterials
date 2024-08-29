using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecyclableMaterials.Data;
using RecyclableMaterials.Models;

namespace RecyclableMaterials.Controllers
{
    public class CommentController : Controller
    {
        private readonly RDBContext _dbContext;
        private readonly UserManager<AppUserModel> _userManager;

        public CommentController(RDBContext dBContext, UserManager<AppUserModel> userManager)
        {
           _dbContext=dBContext;
           _userManager=userManager;
        }

       

        [HttpPost]
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

      
            var product = await _dbContext.products
                .Include(p => p.user)
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product != null && product.UserId != userId)
            {
                var notification = new NotificationModel
                {
                    UserId = product.UserId,
                    Message = $"A new comment has been added to your Material '{product.Name}' by {User.Identity.Name}",
                    CreatedAt = DateTime.Now,
                    IsRead = false,
                    Type = "Comment",
                    IconUrl = "/images/comment-icon.png" // رابط الشعار
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
