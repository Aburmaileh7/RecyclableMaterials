using System.ComponentModel.DataAnnotations;

namespace RecyclableMaterials.ViewModels
{
    public class UserViewModel
    {
        [Key]
        public string Id { get; set; }

        public string UserNmae { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
