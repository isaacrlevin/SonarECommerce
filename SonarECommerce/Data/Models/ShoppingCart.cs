using System.ComponentModel.DataAnnotations.Schema;

namespace SonarECommerce.Data.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        
        public string UserId { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public virtual ApplicationUser User { get; set; } = null!;
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        
        [NotMapped]
        public decimal TotalAmount => CartItems.Sum(item => item.Price * item.Quantity);
        
        [NotMapped]
        public int TotalItems => CartItems.Sum(item => item.Quantity);
    }
}