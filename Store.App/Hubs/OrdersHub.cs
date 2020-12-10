using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Store.DAL.Models;

namespace Store.App.Hubs
{
    public class LockedOrder
    {
        public string UserId { get; set; }

        public int OrderId { get; set; }
    }

    public interface IOrdersClient
    {
        Task GetNewOrder(Order order);
        Task GetLockedOrder(LockedOrder order);
    }

    public class OrdersHub : Hub<IOrdersClient>
    {
        [Authorize(Roles = "Admin")]
        public async Task LockOrder(LockedOrder order)
        {
            await Clients.Group(Constants.AdminGroup).GetLockedOrder(order);
        }

        [Authorize]
        public async Task JoinGroup(string groupName)
        {
            if (Context.User != null)
            {
                var userRoles = Context.User.FindAll("role").Select(c => c.Value);
                if (userRoles.Contains(groupName))
                    await Groups.AddToGroupAsync(Context.ConnectionId, Constants.AdminGroup);
            }
        }
    }
}
