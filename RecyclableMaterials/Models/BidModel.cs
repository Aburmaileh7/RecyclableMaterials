namespace RecyclableMaterials.Models
{
    public class BidModel
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        //public Material Material { get; set; }
        public int UserId { get; set; }
        //public User User { get; set; }
        public decimal OfferAmount { get; set; }
        public DateTime DateSubmitted { get; set; }
    }
}
