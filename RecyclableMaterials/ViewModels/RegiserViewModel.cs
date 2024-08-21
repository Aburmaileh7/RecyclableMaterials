using System.ComponentModel.DataAnnotations;

namespace RecyclableMaterials.ViewModels
{
    public class RegiserViewModel
    {
   
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string UserName { get; set; }

        public string? UserType { get; set; } // Individual or Company
    }
}
