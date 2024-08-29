using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RecyclableMaterials.Models
{
    public class NotificationModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationId { get; set; }

        public string UserId { get; set; }
        public AppUserModel User { get; set; }

        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
        public string Type { get; set; }
        public string IconUrl { get; set; }

    }
}
