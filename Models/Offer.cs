using System.ComponentModel.DataAnnotations;

namespace LuzmaShopAPI.Models
{
    public class Offer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = "";
        public int Discount { get; set; } = 0;
    }
}
