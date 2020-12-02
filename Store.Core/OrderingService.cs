using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Store.App.Core;
using Store.DAL;
using Store.DAL.Models;

namespace Store.Core
{
    public class OrderingService : IOrderingService
    {
        private readonly StoreContext context;
        private readonly ICartService cartService;
        private readonly IMapper mapper;

        public OrderingService(StoreContext context, ICartService cartService, IMapper mapper)
        {
            this.context = context;
            this.cartService = cartService;
            this.mapper = mapper;
        }

        public async Task<Order> GetOrder(int orderId)
        {
            return await context.Orders.Where(o => o.Id == orderId).Include(o => o.Items).SingleOrDefaultAsync();
        }

        public async Task<int> PlaceOrder(string userId, string address, string phone)
        {
            var cart = await cartService.FindCartByUserId(userId);
            if (cart == null) throw new ArgumentException(nameof(userId));

            var order = mapper.Map<Order>(cart);
            order.UserId = userId;
            order.Address = address;
            order.Phone = phone;

            var entry = await context.Orders.AddAsync(order);

            await context.SaveChangesAsync();
            await cartService.ClearCart(cart.Id);

            return entry.Entity.Id;
        }
    }
}
