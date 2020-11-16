using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Store.App.Core;

namespace Store.App
{
    public class CartIdHandler : IMiddleware
    {
        private readonly ICartService cartService;

        public CartIdHandler(ICartService cartService)
        {
            this.cartService = cartService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (!context.Request.Cookies.ContainsKey(Constants.CartCookieName))
            {
                int id;
                if (context.User.Identity.IsAuthenticated)
                    id = (await cartService.FindCartByUserId(context.User.Identity.Name)).Id;
                else
                    id = await cartService.CreateCart();

                context.Response.Cookies.Append(Constants.CartCookieName, id.ToString());
            }

            await next(context);
        }
    }
}
