using System.ComponentModel.DataAnnotations;

namespace RecyclableMaterials.ViewModels
{
    public class UserViewModel
    {
        [Key]
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
