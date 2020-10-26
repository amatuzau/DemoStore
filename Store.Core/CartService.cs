using Store.DAL;
using Store.DAL.Models;
using System;

namespace Store.Core
{
    public class CartService : ICartService
    {
        private readonly IRepository<Cart> repository;

        public CartService(IRepository<Cart> repository)
        {
            this.repository = repository;
        }

        public Cart FindCart(int id)
        {
            return GetCart(id);
        }

        public void AddItemToCart(int id, Product item, int count)
        {
            if (count <= 0) throw new ArgumentException(nameof(count));

            var cart = GetCart(id);
            var stringId = item.Id.ToString();
            if (cart.CartItems.ContainsKey(stringId))
            {
                cart.CartItems[stringId] += count;
            }
            else
            {
                cart.CartItems.Add(stringId, count);
            }
            repository.SaveChangesAsync().Wait();
        }

        public void ChangeItemCount(int id, Product item, int newCount)
        {
            var cart = GetCart(id);
            var stringId = item.Id.ToString();
            if (cart.CartItems.ContainsKey(stringId))
            {
                cart.CartItems[stringId] = newCount;
            } else
            {
                throw new ArgumentException(nameof(item));
            }
            repository.SaveChangesAsync().Wait();
        }

        public void RemoveItemFromCart(int id, Product item)
        {
            var cart = GetCart(id);
            cart.CartItems.Remove(item.Id.ToString());
            repository.SaveChangesAsync().Wait();
        }

        public void ClearCart(int id)
        {
            var cart = GetCart(id);
            cart.CartItems.Clear();
            repository.SaveChangesAsync().Wait();
        }

        public int CreateCart()
        {
            var id = repository.Add(new Cart());
            repository.SaveChangesAsync().Wait();
            return id;
        }

        private Cart GetCart(int id)
        {
            var cart = repository.Get(id);
            if (cart == null)
            {
                cart = new Cart { Id = id };
                repository.Add(cart);
            }
            return cart;
        }
    }
}
