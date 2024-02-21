using System.ComponentModel.DataAnnotations;

namespace LuzmaShopAPI.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public User User { get; set; } = new User();
        public bool Ordered { get; set; }
        public string OrderedOn { get; set; } = string.Empty;
    }
}
