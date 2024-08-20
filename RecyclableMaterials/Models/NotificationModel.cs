using RecyclableMaterials.ViewModels;

namespace RecyclableMaterials.Models
{
    public class NotificationModel
    {
       
            public int Id { get; set; }
            public int UserId { get; set; }
            public UserViewModel User { get; set; }
            public string Message { get; set; }
            public bool IsRead { get; set; }
            public DateTime CreatedAt { get; set; }
        

    }
}
