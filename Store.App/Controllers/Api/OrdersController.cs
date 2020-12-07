using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Store.App.Hubs;
using Store.Core;

namespace Store.App.Controllers.Api
{
    public class Order
    {
        public string Address { get; set; }
        public string Phone { get; set; }
    }

    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrderingService orderingService;
        private readonly IHubContext<OrdersHub, IOrdersClient> hubContext;

        public OrdersController(IOrderingService orderingService, IHubContext<OrdersHub, IOrdersClient> hubContext)
        {
            this.orderingService = orderingService;
            this.hubContext = hubContext;
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> Get(int orderId)
        {
            var order = await orderingService.GetOrder(orderId);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] Order order)
        {
            var id = await orderingService.PlaceOrder(User.GetSubjectId(), order.Address, order.Phone);

            var newOrder = await orderingService.GetOrder(id);

            await hubContext.Clients.All.GetNewOrder(newOrder);

            return CreatedAtAction("Get", id);
        }
    }
}
