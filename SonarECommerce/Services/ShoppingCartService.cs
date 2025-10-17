using SonarECommerce.Data;
using SonarECommerce.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace SonarECommerce.Services
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> GetOrCreateCartAsync(string userId);
        Task<bool> AddToCartAsync(string userId, int productId, int quantity = 1);
        Task<bool> UpdateCartItemAsync(string userId, int productId, int quantity);
        Task<bool> RemoveFromCartAsync(string userId, int productId);
        Task<bool> ClearCartAsync(string userId);
        Task<int> GetCartItemCountAsync(string userId);
        Task<decimal> GetCartTotalAsync(string userId);
    }

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public ShoppingCartService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<ShoppingCart> GetOrCreateCartAsync(string userId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var cart = await context.ShoppingCarts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                context.ShoppingCarts.Add(cart);
                await context.SaveChangesAsync();
            }

            return cart;
        }

        public async Task<bool> AddToCartAsync(string userId, int productId, int quantity = 1)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                
                var product = await context.Products.FindAsync(productId);
                if (product == null || !product.IsActive || product.StockQuantity < quantity)
                    return false;

                var cart = await context.ShoppingCarts
                    .Include(c => c.CartItems)
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                if (cart == null)
                {
                    cart = new ShoppingCart
                    {
                        UserId = userId,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    context.ShoppingCarts.Add(cart);
                    await context.SaveChangesAsync();
                }

                var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                    existingItem.Price = product.Price; // Update price in case it changed
                }
                else
                {
                    var cartItem = new CartItem
                    {
                        ShoppingCartId = cart.Id,
                        ProductId = productId,
                        Quantity = quantity,
                        Price = product.Price,
                        AddedAt = DateTime.UtcNow
                    };

                    context.CartItems.Add(cartItem);
                }

                cart.UpdatedAt = DateTime.UtcNow;
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateCartItemAsync(string userId, int productId, int quantity)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                
                var cart = await context.ShoppingCarts
                    .Include(c => c.CartItems)
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                if (cart == null)
                    return false;

                var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

                if (cartItem == null)
                    return false;

                if (quantity <= 0)
                {
                    context.CartItems.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity = quantity;
                }

                cart.UpdatedAt = DateTime.UtcNow;
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveFromCartAsync(string userId, int productId)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                
                var cart = await context.ShoppingCarts
                    .Include(c => c.CartItems)
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                if (cart == null)
                    return false;

                var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

                if (cartItem == null)
                    return false;

                context.CartItems.Remove(cartItem);
                cart.UpdatedAt = DateTime.UtcNow;
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ClearCartAsync(string userId)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                
                var cart = await context.ShoppingCarts
                    .Include(c => c.CartItems)
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                if (cart == null)
                    return false;

                context.CartItems.RemoveRange(cart.CartItems);
                cart.UpdatedAt = DateTime.UtcNow;
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> GetCartItemCountAsync(string userId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var cart = await context.ShoppingCarts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            return cart?.TotalItems ?? 0;
        }

        public async Task<decimal> GetCartTotalAsync(string userId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var cart = await context.ShoppingCarts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            return cart?.TotalAmount ?? 0;
        }
    }
}