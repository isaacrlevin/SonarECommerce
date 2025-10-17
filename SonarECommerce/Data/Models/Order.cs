using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SonarECommerce.Data.Models
{
    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }
    
    public class Order
    {
        public int Id { get; set; }
        
        public string UserId { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string OrderNumber { get; set; } = string.Empty;
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        
        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        
        // Shipping Information
        [Required]
        [StringLength(100)]
        public string ShippingName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(200)]
        public string ShippingAddress { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string ShippingCity { get; set; } = string.Empty;
        
        [Required]
        [StringLength(20)]
        public string ShippingZipCode { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string ShippingCountry { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public virtual ApplicationUser User { get; set; } = null!;
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}