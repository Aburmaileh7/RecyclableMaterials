using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecyclableMaterials.Data;
using RecyclableMaterials.Models;

namespace RecyclableMaterials.Controllers
{
    public class NotificationsController : Controller
    {
        private  RDBContext _dbContext;
        private  UserManager<AppUserModel> _userManager;

        public NotificationsController(RDBContext dbContext, UserManager<AppUserModel> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUnreadNotiCount()
        {
            var userId = _userManager.GetUserId(User);
            var unreadCount = await _dbContext.Notifications
                .CountAsync(n => n.UserId == userId && !n.IsRead);

            return Json(new { count = unreadCount });
        }


        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            var userId = _userManager.GetUserId(User);
            var notifications = await _dbContext.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            return PartialView("_NotificationsList", notifications);
        }



    }
}
