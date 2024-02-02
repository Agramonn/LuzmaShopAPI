using System.ComponentModel.DataAnnotations;

namespace LuzmaShopAPI.Models
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Category { get; set; } = "";
        public string SubCategory { get; set; } = "";
    }
}
