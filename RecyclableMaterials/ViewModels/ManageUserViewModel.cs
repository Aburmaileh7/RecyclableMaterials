using System.ComponentModel.DataAnnotations;

namespace RecyclableMaterials.ViewModels
{
    public class ManageUserViewModel
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
