using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RecyclableMaterials.Areas.Dashboard.Models
{

    [Table("Category", Schema = "dbo")]
    public class CategoryModel
    {

        [Key]
        [Display(Name = "Category ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        [StringLength(20)]
        [Column(TypeName = "nvarchar(20)")]
        public string Name { get; set; }
    }
}
