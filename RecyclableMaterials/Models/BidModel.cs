using RecyclableMaterials.ViewModels;

namespace RecyclableMaterials.Models
{
    public class BidModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public ProductModel product { get; set; }

        public int UserId { get; set; }
        public UserViewModel User { get; set; }

        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
