using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RecyclableMaterials.Areas.Dashboard.Models
{

    [Table("Products", Schema = "dbo")]
    public class ProductModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Product Id")]
        public int Id { get; set; }



        [Display(Name = "Product Name")]
        [Column(TypeName = "nvarchar(20)")]
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Product Discription")]
        [Column(TypeName = "nvarchar(200)")]
        public string Discription { get; set; }

        public decimal? Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string? ImagePath { get; set; }

        [Required]
        [Display(Name = "Product Location")]
        [Column(TypeName = "nvarchar(200)")]
        public string Location { get; set; }

        [Required]
        public int CategoryID { get; set; }


        public virtual CategoryModel Category { get; set; }

        //public int OwnerId { get; set; }
        //public User Owner { get; set; }

    }
}
