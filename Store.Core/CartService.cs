using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.App.Core;
using Store.DAL;
using Store.DAL.Models;

namespace Store.Core
{
    public class CartService : ICartService
    {
        private readonly StoreContext context;

        public CartService(StoreContext context)
        {
            this.context = context;
        }

        public async Task<Cart> FindCart(int id)
        {
            var cart = await GetCart(id);
            return cart;
        }

        public async Task<Cart> FindCartByUserId(string userId)
        {
            var user = await context.Users.FindAsync(userId);
            var cart = await GetCart(user.CartId);
            return cart;
        }

        public async Task AddItemToCart(int id, int productId, int count)
        {
            if (count <= 0) throw new ArgumentException(nameof(count));

            var cart = await GetCart(id);
            if (cart.CartItems.Any(ci => ci.ProductId == productId))
                cart.CartItems.Single(ci => ci.ProductId == productId).Count += count;
            else
                cart.CartItems.Add(new CartItem {ProductId = productId, Count = count});
            context.Update(cart);
            await context.SaveChangesAsync();
        }

        private async Task<Cart> GetCart(int id)
        {
            var cart = await context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (cart != null && cart.CartItems.Any())
            {
                cart.Total = cart.CartItems.Select(c => c.Product.Price * c.Count).Sum();
            }

            return cart;
        }

        public async Task ChangeItemCount(int id, int productId, int newCount)
        {
            var cart = await GetCart(id);
            if (cart.CartItems.Any(ci => ci.ProductId == productId))
            {
                if (newCount == 0)
                {
                    var cartItem = cart.CartItems.SingleOrDefault(ci => ci.ProductId == productId);
                    if (cartItem == null) return;
                    cart.CartItems.Remove(cartItem);
                }
                else
                {
                    cart.CartItems.Single(ci => ci.ProductId == productId).Count = newCount;
                }
            }
            else
                throw new ArgumentException(nameof(productId));
            context.Update(cart);
            await context.SaveChangesAsync();
        }

        public async Task RemoveItemFromCart(int id, int productId)
        {
            var cart = await GetCart(id);
            var cartItem = cart.CartItems.SingleOrDefault(ci => ci.ProductId == productId);
            if (cartItem == null) return;
            cart.CartItems.Remove(cartItem);
            context.Update(cart);
            await context.SaveChangesAsync();
        }

        public async Task ClearCart(int id)
        {
            var cart = await GetCart(id);
            cart.CartItems.Clear();
            context.Update(cart);
            await context.SaveChangesAsync();
        }

        public async Task<int> CreateCart()
        {
            var newCart = await context.Carts.AddAsync(new Cart());
            await context.SaveChangesAsync();
            return newCart.Entity.Id;
        }
    }
}
