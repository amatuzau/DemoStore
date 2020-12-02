using System.Threading.Tasks;
using Store.DAL.Models;

namespace Store.Core
{
    public interface IOrderingService
    {
        Task<int> PlaceOrder(string userId, string address, string phone);
        Task<Order> GetOrder(int orderId);
    }
}
