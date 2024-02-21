using System.ComponentModel.DataAnnotations;

namespace LuzmaShopAPI.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        public Cart Cart { get; set; } = new Cart();
        public Product Product { get; set; } = new Product();
    }
}
