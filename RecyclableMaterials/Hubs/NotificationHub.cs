using Microsoft.AspNetCore.SignalR;

namespace RecyclableMaterials.Hubs
{
    public class NotificationHub :Hub
    {
        public async Task SendNotification(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", message);
        }
    }
}
