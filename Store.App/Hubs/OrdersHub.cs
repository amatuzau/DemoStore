using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Store.DAL.Models;

namespace Store.App.Hubs
{
    public interface IOrdersClient
    {
        Task GetNewOrder(Order order);
    }

    public class OrdersHub : Hub<IOrdersClient>
    {
        public async Task SendMessage(Order order)
        {
            await Clients.All.GetNewOrder(order);
        }
    }
}
