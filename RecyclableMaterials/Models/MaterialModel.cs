using System.ComponentModel.DataAnnotations;

namespace RecyclableMaterials.Models
{
    public class MaterialModel

    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Location { get; set; }

        public string ImageUrl { get; set; }

       
        //public int OwnerId { get; set; }
        //public User Owner { get; set; }
    }
}
