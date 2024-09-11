namespace RecyclableMaterials.Services
{
    public interface INotificationService
    {
        void SendNotification(string userId, string message);
    }

}
