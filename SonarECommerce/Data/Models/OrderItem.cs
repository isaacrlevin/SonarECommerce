using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SonarECommerce.Data.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        [Required]
        [StringLength(200)]
        public string ProductName { get; set; } = string.Empty;
        
        // Navigation properties
        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        
        [NotMapped]
        public decimal TotalPrice => Price * Quantity;
    }
}