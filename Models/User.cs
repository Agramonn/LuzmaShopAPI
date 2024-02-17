using System.ComponentModel.DataAnnotations;

namespace LuzmaShopAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; }= string.Empty;
        [Required]
        public string Email { get; set; }=string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public string Address {  get; set; } = string.Empty;
        [Required]
        public string Mobile { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public string UpdatedAt { get; set; } = string.Empty;
    }
}
