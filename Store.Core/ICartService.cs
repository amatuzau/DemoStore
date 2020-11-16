using System.Threading.Tasks;
using Store.DAL.Models;

namespace Store.App.Core
{
    public interface ICartService
    {
        Task AddItemToCart(int id, int productId, int count);
        Task ChangeItemCount(int id, int productId, int newCount);
        Task ClearCart(int id);
        Task<Cart> FindCart(int id);
        Task RemoveItemFromCart(int id, int productId);
        Task<int> CreateCart();
        Task<Cart> FindCartByUserId(string userId);
    }
}
