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

        [Display(Name = "Profile Picture URL")]
        public IFormFile? ProfilePicture { get; set; }



        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
        public string ProfilePictureUrl { get; internal set; }
    }
}
