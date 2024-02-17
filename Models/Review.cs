using System.ComponentModel.DataAnnotations;

namespace LuzmaShopAPI.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public User User { get; set; } = new User();
        public Product Product { get; set; } = new Product();
        public string Value { get; set; }  = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
    }
}
