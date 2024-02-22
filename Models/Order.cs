using System.ComponentModel.DataAnnotations;

namespace LuzmaShopAPI.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public User User { get; set; } = new User();
        public UserCart UserCart { get; set; } = new UserCart();
        public Payment Payment { get; set; } = new Payment();
        public string CreatedAt { get; set; } = string.Empty;
    }
}
