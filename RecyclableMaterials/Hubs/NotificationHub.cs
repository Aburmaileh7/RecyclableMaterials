using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class NotificationHub : Hub
{
    public async Task SendNotification(string userId, string message)
    {
        // إرسال الإشعار إلى مستخدم محدد
        await Clients.User(userId).SendAsync("ReceiveNotification", message);
    }
}
