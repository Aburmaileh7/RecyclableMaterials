using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using RecyclableMaterials.Data;
using RecyclableMaterials.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace RecyclableMaterials.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly RDBContext _dbContext;
        private readonly UserManager<AppUserModel> _userManager;

        public NotificationsController(RDBContext dbContext, UserManager<AppUserModel> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

       
        public  async Task< IActionResult > Index()
        {
            var userId = _userManager.GetUserId(User); 
            var notifications = await _dbContext.Notifications.Where(n => n.UserId == userId)
            .OrderByDescending(n => n.DateCreated)
            .ToListAsync();

            return View(notifications);
        }

        
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var notification = await _dbContext.Notifications.FindAsync(id);
            if (notification != null)
            {
                notification.IsRead = true;
                await _dbContext.SaveChangesAsync();
            }
            
            return RedirectToAction("Details", "Product", new { id = notification.ProductId });
        }

        public async Task<int> GetUnreadNotificationsCount()
        {
            var userId = _userManager.GetUserId(User);
            var unreadCount = await _dbContext.Notifications
                                            .Where(n => n.IsRead == false && n.UserId == userId)
                                            .CountAsync();

            return unreadCount;
        }
    }

}
