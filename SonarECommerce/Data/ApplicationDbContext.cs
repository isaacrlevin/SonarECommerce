using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SonarECommerce.Data.Models;

namespace SonarECommerce.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ShoppingCart>()
                .HasOne(sc => sc.User)
                .WithMany()
                .HasForeignKey(sc => sc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.ShoppingCart)
                .WithMany(sc => sc.CartItems)
                .HasForeignKey(ci => ci.ShoppingCartId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure indexes for better performance
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.CategoryId);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.IsActive);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.IsFeatured);

            modelBuilder.Entity<ShoppingCart>()
                .HasIndex(sc => sc.UserId)
                .IsUnique();

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderNumber)
                .IsUnique();

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Categories
            var categories = new[]
            {
                new Category { Id = 1, Name = "Processors", Description = "CPUs and processors for desktop and server systems", ImageUrl = "/images/categories/processors.png" },
                new Category { Id = 2, Name = "Graphics Cards", Description = "GPUs for gaming and professional workloads", ImageUrl = "/images/categories/graphics-cards.png" },
                new Category { Id = 3, Name = "Memory", Description = "RAM modules for system memory", ImageUrl = "/images/categories/memory.png" },
                new Category { Id = 4, Name = "Storage", Description = "SSDs, HDDs, and storage solutions", ImageUrl = "/images/categories/storage.png" },
                new Category { Id = 5, Name = "Motherboards", Description = "Motherboards for various form factors", ImageUrl = "/images/categories/motherboards.png" },
                new Category { Id = 6, Name = "Power Supplies", Description = "PSUs and power management", ImageUrl = "/images/categories/power-supplies.png" },
                new Category { Id = 7, Name = "Cooling", Description = "CPU coolers and case fans", ImageUrl = "/images/categories/cooling.png" },
                new Category { Id = 8, Name = "Cases", Description = "PC cases and enclosures", ImageUrl = "/images/categories/cases.png" }
            };

            modelBuilder.Entity<Category>().HasData(categories);

            // Seed Products
            var products = new[]
            {
                // Processors
                new Product { Id = 1, Name = "AMD Ryzen 9 7950X", Description = "16-core, 32-thread processor with 5.7 GHz boost clock", Price = 699.99m, CategoryId = 1, Brand = "AMD", Model = "7950X", StockQuantity = 25, ImageUrl = "/images/products/ryzen-9-7950x.png", IsFeatured = true },
                new Product { Id = 2, Name = "Intel Core i9-13900K", Description = "24-core (8P+16E) processor with 5.8 GHz boost clock", Price = 589.99m, CategoryId = 1, Brand = "Intel", Model = "i9-13900K", StockQuantity = 30, ImageUrl = "/images/products/i9-13900k.png", IsFeatured = true },
                new Product { Id = 3, Name = "AMD Ryzen 7 7700X", Description = "8-core, 16-thread processor with 5.4 GHz boost clock", Price = 399.99m, CategoryId = 1, Brand = "AMD", Model = "7700X", StockQuantity = 40, ImageUrl = "/images/products/ryzen-7-7700x.png" },
                
                // Graphics Cards
                new Product { Id = 4, Name = "NVIDIA RTX 4090", Description = "24GB GDDR6X flagship gaming graphics card", Price = 1599.99m, CategoryId = 2, Brand = "NVIDIA", Model = "RTX 4090", StockQuantity = 15, ImageUrl = "/images/products/rtx-4090.png", IsFeatured = true },
                new Product { Id = 5, Name = "AMD RX 7900 XTX", Description = "24GB GDDR6 high-performance gaming graphics card", Price = 999.99m, CategoryId = 2, Brand = "AMD", Model = "RX 7900 XTX", StockQuantity = 20, ImageUrl = "/images/products/rx-7900-xtx.png", IsFeatured = true },
                new Product { Id = 6, Name = "NVIDIA RTX 4070", Description = "12GB GDDR6X mid-range gaming graphics card", Price = 599.99m, CategoryId = 2, Brand = "NVIDIA", Model = "RTX 4070", StockQuantity = 35, ImageUrl = "/images/products/rtx-4070.png" },
                
                // Memory
                new Product { Id = 7, Name = "Corsair Dominator Platinum RGB 32GB", Description = "DDR5-5600 (2x16GB) high-performance memory kit", Price = 329.99m, CategoryId = 3, Brand = "Corsair", Model = "Dominator Platinum", StockQuantity = 50, ImageUrl = "/images/products/corsair-dominator-32gb.png" },
                new Product { Id = 8, Name = "G.SKILL Trident Z5 RGB 16GB", Description = "DDR5-6000 (2x8GB) gaming memory kit", Price = 179.99m, CategoryId = 3, Brand = "G.SKILL", Model = "Trident Z5", StockQuantity = 60, ImageUrl = "/images/products/gskill-trident-z5-16gb.png" },
                
                // Storage
                new Product { Id = 9, Name = "Samsung 980 PRO 2TB", Description = "PCIe 4.0 NVMe SSD with 7,000 MB/s read speed", Price = 199.99m, CategoryId = 4, Brand = "Samsung", Model = "980 PRO", StockQuantity = 45, ImageUrl = "/images/products/samsung-980-pro-2tb.png", IsFeatured = true },
                new Product { Id = 10, Name = "WD Black SN850X 1TB", Description = "PCIe 4.0 NVMe SSD optimized for gaming", Price = 129.99m, CategoryId = 4, Brand = "Western Digital", Model = "SN850X", StockQuantity = 55, ImageUrl = "/images/products/wd-black-sn850x-1tb.png" },
                
                // Motherboards
                new Product { Id = 11, Name = "ASUS ROG Crosshair X670E Hero", Description = "Premium AMD X670E motherboard with WiFi 6E", Price = 699.99m, CategoryId = 5, Brand = "ASUS", Model = "ROG Crosshair X670E Hero", StockQuantity = 20, ImageUrl = "/images/products/asus-crosshair-x670e.png" },
                new Product { Id = 12, Name = "MSI MPG Z690 Carbon WiFi", Description = "Intel Z690 motherboard with RGB lighting", Price = 399.99m, CategoryId = 5, Brand = "MSI", Model = "MPG Z690 Carbon", StockQuantity = 25, ImageUrl = "/images/products/msi-z690-carbon.png" },
                
                // Power Supplies
                new Product { Id = 13, Name = "Corsair RM1000x", Description = "1000W 80+ Gold fully modular power supply", Price = 199.99m, CategoryId = 6, Brand = "Corsair", Model = "RM1000x", StockQuantity = 30, ImageUrl = "/images/products/corsair-rm1000x.png" },
                new Product { Id = 14, Name = "EVGA SuperNOVA 850 G6", Description = "850W 80+ Gold fully modular power supply", Price = 149.99m, CategoryId = 6, Brand = "EVGA", Model = "SuperNOVA 850 G6", StockQuantity = 40, ImageUrl = "/images/products/evga-supernova-850-g6.png" },
                
                // Cooling
                new Product { Id = 15, Name = "Noctua NH-D15", Description = "Premium dual-tower CPU cooler with quiet operation", Price = 99.99m, CategoryId = 7, Brand = "Noctua", Model = "NH-D15", StockQuantity = 35, ImageUrl = "/images/products/noctua-nh-d15.png" },
                new Product { Id = 16, Name = "Corsair H150i Elite Capellix", Description = "360mm RGB liquid CPU cooler", Price = 189.99m, CategoryId = 7, Brand = "Corsair", Model = "H150i Elite", StockQuantity = 25, ImageUrl = "/images/products/corsair-h150i.png" },
                
                // Cases
                new Product { Id = 17, Name = "Fractal Design Meshify 2", Description = "Mid-tower case with excellent airflow", Price = 139.99m, CategoryId = 8, Brand = "Fractal Design", Model = "Meshify 2", StockQuantity = 30, ImageUrl = "/images/products/fractal-meshify-2.png" },
                new Product { Id = 18, Name = "NZXT H7 Flow", Description = "Mid-tower RGB gaming case", Price = 129.99m, CategoryId = 8, Brand = "NZXT", Model = "H7 Flow", StockQuantity = 25, ImageUrl = "/images/products/nzxt-h7-flow.png" }
            };

            modelBuilder.Entity<Product>().HasData(products);
        }
    }
}
