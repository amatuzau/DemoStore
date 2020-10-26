using System;
using System.Collections.Generic;
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

        public async Task AddItemToCart(int id, Product item, int count)
        {
            if (count <= 0) throw new ArgumentException(nameof(count));

            var cart = await GetCart(id);
            if (cart.CartItems.Any(ci => ci.ProductId == item.Id))
                cart.CartItems.Single(ci => ci.ProductId == item.Id).Count += count;
            else
                cart.CartItems.Add(new CartItem {ProductId = item.Id, Count = count});
            context.Update(cart);
            await context.SaveChangesAsync();
        }

        private async Task<Cart> GetCart(int id)
        {
            var cart = await context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .SingleOrDefaultAsync(c => c.Id == id);
            return cart;
        }

        public async Task ChangeItemCount(int id, Product item, int newCount)
        {
            var cart = await GetCart(id);
            if (cart.CartItems.Any(ci => ci.ProductId == item.Id))
                cart.CartItems.Single(ci => ci.ProductId == item.Id).Count = newCount;
            else
                throw new ArgumentException(nameof(item));
            context.Update(cart);
            await context.SaveChangesAsync();
        }

        public async Task RemoveItemFromCart(int id, Product item)
        {
            var cart = await GetCart(id);
            var cartItem = cart.CartItems.SingleOrDefault(ci => ci.ProductId == item.Id);
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