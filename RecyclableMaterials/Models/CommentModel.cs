using Microsoft.AspNetCore.Identity;
using RecyclableMaterials.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecyclableMaterials.Models
{
    public class CommentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }

        public int ProductId { get; set; }

        public ProductModel product { get; set; }

        public string UserId { get; set; }
        public AppUserModel user { get; set; }

       
        public string Text { get; set; }

        public DateTime CreateAt { get; set; }
    }
    /////////////////////////////////////////////////
    
}
