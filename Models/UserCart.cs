using System.ComponentModel.DataAnnotations;

namespace LuzmaShopAPI.Models
{
    public class UserCart
    {
        [Key]
        public int Id { get; set; }
        public User User { get; set; } = new User();
        public List<UserCartItem> CartItems { get; set; } = new();
        public bool Ordered {  get; set; }
        public string OrderedOn { get; set; } = string.Empty;
    }
}
