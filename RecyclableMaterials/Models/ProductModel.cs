using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RecyclableMaterials.Areas.Dashboard.Models;
using Microsoft.AspNetCore.Identity;


namespace RecyclableMaterials.Models
{
    [Table("Products", Schema = "dbo")]

    public class ProductModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }

       

        [Display(Name = "Name Material")]
        [Column(TypeName = "nvarchar(20)")]
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Material Discription")]
        [Column(TypeName = "nvarchar(500)")]
        [StringLength(500)]
        public string Discription { get; set; }

        [Display(Name = "Material Price")]
        [Column(TypeName = "decimal(18, 3)")]
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
        public  CategoryModel Category { get; set; }

        public string UserId { get; set; }
        public AppUserModel user { get; set; }

        public bool IsReserved { get; set; } //الحجز
        public string ReservedByUserId { get; set; } 
        public DateTime? ReservationDate { get; set; }

        public ICollection<CommentModel> Comments { get; set; } = new List<CommentModel>();
        public List<RatingModel> Ratings { get; set; } = new List<RatingModel>();

        public ICollection<Notifications> Notifications { get; set; }
    }
}
