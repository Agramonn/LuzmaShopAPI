using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuzmaShopAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ProductCategory ProductCategory { get; set; } = new ProductCategory();
        public Offer Offer { get; set; } = new Offer();
        public double Price { get; set; }
        public int Quantity {  get; set; }
        public string ImageName { get; set; } = string.Empty;
    }
}
