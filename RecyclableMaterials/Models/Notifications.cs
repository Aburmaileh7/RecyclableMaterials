using Microsoft.AspNetCore.Identity;

namespace RecyclableMaterials.Models
{
    public class Notifications
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual AppUserModel User { get; set; }

        public int? ProductId { get; set; }

        public ProductModel product { get; set; }
    }

}
