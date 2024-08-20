using RecyclableMaterials.ViewModels;

namespace RecyclableMaterials.Models
{
    public class RatingModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public virtual ProductModel Product { get; set; }

        public int UserId  { get; set; }
        public virtual UserViewModel User { get; set; }

        public int Stars { get; set; }

        public DateTime CreateAt { get; set; }
    }
}
