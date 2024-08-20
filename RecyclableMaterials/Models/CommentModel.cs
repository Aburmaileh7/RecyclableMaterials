using RecyclableMaterials.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace RecyclableMaterials.Models
{
    public class CommentModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public ProductModel product { get; set; }

        public int UserId { get; set; }
        public UserViewModel user { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime CreateAt { get; set; }
    }
}
