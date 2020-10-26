using Store.DAL.Models;
using System;

namespace Store.Core
{
    public interface ICartService
    {
        void AddItemToCart(int id, Product item, int count);
        void ChangeItemCount(int id, Product item, int newCount);
        void ClearCart(int id);
        Cart FindCart(int id);
        void RemoveItemFromCart(int id, Product item);
        int CreateCart();
    }
}