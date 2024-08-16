using System.ComponentModel.DataAnnotations;

namespace RecyclableMaterials.ViewModels
{
    public class LoginViewModel
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
