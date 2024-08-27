using Microsoft.AspNetCore.Identity;
using RecyclableMaterials.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecyclableMaterials.Models
{
    public class RatingModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Please enter a rating between 1 and 5 stars.")]
        public int Stars { get; set; }

     
        public string? UserId { get; set; } 

        public IdentityUser User { get; set; }

        public int ProductId { get; set; }
        public  ProductModel Product { get; set; }
        public DateTime CreateAt { get; set; }
    }
}

