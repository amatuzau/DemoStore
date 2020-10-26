using System.Threading.Tasks;
using Store.DAL.Models;

namespace Store.App.Core
{
    public interface ICartService
    {
        Task AddItemToCart(int id, Product item, int count);
        Task ChangeItemCount(int id, Product item, int newCount);
        Task ClearCart(int id);
        Task<Cart> FindCart(int id);
        Task RemoveItemFromCart(int id, Product item);
        Task<int> CreateCart();
    }
}