namespace RecyclableMaterials.Models
{
    public class ReservationModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public virtual ProductModel Product { get; set; }

        public string UserId { get; set; }
        public virtual AppUserModel User { get; set; }

        // Pending, Approved, Rejected
        public string Status { get; set; }

        public DateTime ReservationDate { get; set; }

        public DateTime? ReservationEndDate { get; set; }
    }

}
