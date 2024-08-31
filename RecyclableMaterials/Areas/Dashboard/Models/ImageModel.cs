using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RecyclableMaterials.Areas.Dashboard.Models
{
    public class ImageModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "image Id")]
        public int Id { get; set; }


        
        [Display(Name = "Material Discription")]
        [Column(TypeName = "nvarchar(500)")]
        [StringLength(500)]
        public string? Name { get; set; }


        [Required]
        public string ImagePath { get; set; }

    }
}
