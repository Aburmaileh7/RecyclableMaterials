using System.ComponentModel.DataAnnotations;

namespace RecyclableMaterials.ViewModels
{
    public class RoleFormViewModel
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [Display(Name ="Role Name"),StringLength(100)]
        public string Name { get; set; }
    }
}
