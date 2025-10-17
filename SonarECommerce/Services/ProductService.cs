using SonarECommerce.Data;
using SonarECommerce.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace SonarECommerce.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<IEnumerable<Product>> GetFeaturedProductsAsync();
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<Product?> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm, int? categoryId = null, decimal? minPrice = null, decimal? maxPrice = null, string? brand = null);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<IEnumerable<string>> GetBrandsAsync();
        Task<IEnumerable<Product>> GetTopSellingProductsAsync(int count = 8);
    }

    public class ProductService : IProductService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public ProductService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetFeaturedProductsAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive && p.IsFeatured)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive && p.CategoryId == categoryId)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm, int? categoryId = null, decimal? minPrice = null, decimal? maxPrice = null, string? brand = null)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var query = context.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm) || 
                                        p.Description.Contains(searchTerm) || 
                                        p.Brand.Contains(searchTerm) ||
                                        p.Model.Contains(searchTerm));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            if (!string.IsNullOrWhiteSpace(brand))
            {
                query = query.Where(p => p.Brand == brand);
            }

            return await query.OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<string>> GetBrandsAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Products
                .Where(p => p.IsActive && !string.IsNullOrWhiteSpace(p.Brand))
                .Select(p => p.Brand)
                .Distinct()
                .OrderBy(b => b)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetTopSellingProductsAsync(int count = 8)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            // For now, return featured products. In a real scenario, you'd track sales data
            return await context.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive)
                .OrderByDescending(p => p.IsFeatured)
                .ThenBy(p => p.CreatedAt)
                .Take(count)
                .ToListAsync();
        }
    }
}