using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SonarECommerce.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(2000)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? OriginalPrice { get; set; }
        
        [Required]
        public int StockQuantity { get; set; }
        
        [StringLength(100)]
        public string Brand { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string Model { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Specifications { get; set; } = string.Empty;
        
        [StringLength(300)]
        public string ImageUrl { get; set; } = string.Empty;
        
        public bool IsActive { get; set; } = true;
        public bool IsFeatured { get; set; } = false;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Foreign Keys
        public int CategoryId { get; set; }
        
        // Navigation properties
        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}