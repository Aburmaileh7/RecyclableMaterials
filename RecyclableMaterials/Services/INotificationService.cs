namespace RecyclableMaterials.Services
{
    public interface INotificationService
    {
        void SendNotificationComment(string userId, string message);
        void SendNotificationRating(string userId, string message);
        void SendNotificationRequest(string userId, string message);
    }

}
