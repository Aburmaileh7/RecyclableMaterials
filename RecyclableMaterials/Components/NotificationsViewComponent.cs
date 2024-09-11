using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity;
using System.Linq;
using System.Threading.Tasks;
using RecyclableMaterials.Models;
using RecyclableMaterials.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using RecyclableMaterials.Services;


namespace RecyclableMaterials.ViewComponents
{
    public class NotificationsViewComponent : ViewComponent
    {
        private UserManager<AppUserModel> _userManager;
        private readonly RDBContext _dbContext;

        public NotificationsViewComponent(RDBContext dbContext, UserManager<AppUserModel> userManager)

        {
            this._dbContext = dbContext;
            _userManager = userManager;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userId = user.Id;
            var notifications = await _dbContext.Notifications.Where(n => n.UserId == userId && !n.IsRead)
                .OrderByDescending(n => n.DateCreated).ToListAsync();

            return View(notifications);
        }
    }
}
