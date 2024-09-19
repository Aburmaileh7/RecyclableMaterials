using Microsoft.AspNetCore.Identity;
using RecyclableMaterials.Data;
using RecyclableMaterials.Models;

namespace RecyclableMaterials.Services
{
    public class NotificationService : INotificationService
    {
        private readonly UserManager<AppUserModel> _userManager;
        private readonly RDBContext _dbContext;

        public NotificationService(UserManager<AppUserModel> userManager, RDBContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public void SendNotificationComment(string userId, string message)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            if (user != null)
            {
                // تخزين الإشعار في قاعدة البيانات
                var notification = new Notifications
                {
                    UserId = userId,
                    User=user,
                    Message = message,
                    IsRead = false,
                    Type= "Comment",
                    DateCreated = DateTime.UtcNow
                };

                _dbContext.Notifications.Add(notification);
                _dbContext.SaveChanges();

                // إرسال الإشعار عبر البريد الإلكتروني كمثال آخر
                Console.WriteLine($"Notification sent to {user.Email}: {message}");
            }
        }

        public void SendNotificationRating(string userId, string message)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            if (user != null)
            {
                // تخزين الإشعار في قاعدة البيانات
                var notification = new Notifications
                {
                    UserId = userId,
                    User = user,
                    Message = message,
                    IsRead = false,
                    Type = "Rating",
                    DateCreated = DateTime.UtcNow
                };

                _dbContext.Notifications.Add(notification);
                _dbContext.SaveChanges();

                // إرسال الإشعار عبر البريد الإلكتروني كمثال آخر
                Console.WriteLine($"Notification sent to {user.Email}: {message}");
            }
        }
        
        public void SendNotificationRequest(string userId, string message)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            if (user != null)
            {
                // تخزين الإشعار في قاعدة البيانات
                var notification = new Notifications
                {
                    UserId = userId,
                    User = user,
                    Message = message,
                    IsRead = false,
                    Type = "Request",
                    DateCreated = DateTime.UtcNow
                };

                _dbContext.Notifications.Add(notification);
                _dbContext.SaveChanges();

                // إرسال الإشعار عبر البريد الإلكتروني كمثال آخر
                Console.WriteLine($"Notification sent to {user.Email}: {message}");
            }
        }
    }

}
