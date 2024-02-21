using System.ComponentModel.DataAnnotations;

namespace LuzmaShopAPI.Models
{
    public class UserCartItem
    {
        [Key]
        public int Id { get; set; }
        public Product Product { get; set; } = new Product();
    }
}
