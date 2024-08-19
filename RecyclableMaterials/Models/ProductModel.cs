using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RecyclableMaterials.Areas.Dashboard.Models;

namespace RecyclableMaterials.Models
{
    [Table("Products", Schema = "dbo")]

    public class ProductModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Product Id")]
        public int Id { get; set; }


        [Display(Name = "Name Material")]
        [Column(TypeName = "nvarchar(20)")]
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Material Discription")]
        [Column(TypeName = "nvarchar(200)")]
        public string Discription { get; set; }

        [Display(Name = "Material Price")]
        [Column(TypeName = "nvarchar(200)")]
        public decimal? Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string? ImagePath { get; set; }

        [Required]
        [Display(Name = "Location Material")]
        [Column(TypeName = "nvarchar(200)")]
        public string Location { get; set; }

        [Required]
        public int CategoryID { get; set; }


        public virtual CategoryModel Category { get; set; }
    }
}
