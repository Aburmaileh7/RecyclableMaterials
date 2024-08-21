using Microsoft.EntityFrameworkCore;
using RecyclableMaterials.Data;
using RecyclableMaterials.Models;

public class NotificationService
{
    private readonly RDBContext _dbcontext;

    public NotificationService(RDBContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    // إنشاء إشعار جديد
    public async Task CreateNotification(int userId, string message)
    {
        var notification = new NotificationModel
        {
            UserId = userId,
            Message = message,
            IsRead = false, // تعيين الإشعار كغير مقروء
            CreatedAt = DateTime.Now
        };

        _dbcontext.Add(notification);
        await _dbcontext.SaveChangesAsync();
    }

    // استرجاع الإشعارات غير المقروءة للمستخدم
    public async Task<List<NotificationModel>> GetUserNotifications(int userId)
    {
        return await _dbcontext.Notifications
            .Where(n => n.UserId == userId && !n.IsRead)
            .OrderByDescending(n => n.CreatedAt).ToListAsync();

    }

    // تعيين الإشعار كمقروء
    public async Task MarkAsRead(int notificationId)
    {
        var notification = await _dbcontext.Notifications.FindAsync(notificationId);
        if (notification != null)
        {
            notification.IsRead = true;
            await _dbcontext.SaveChangesAsync();
        }
    }
}
