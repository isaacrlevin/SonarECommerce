using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SonarECommerce.Data.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public virtual ShoppingCart ShoppingCart { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        
        [NotMapped]
        public decimal TotalPrice => Price * Quantity;
    }
}