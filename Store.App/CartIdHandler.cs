using Microsoft.AspNetCore.Http;
using Store.App.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                var id = await cartService.CreateCart();
                context.Response.Cookies.Append(Constants.CartCookieName, id.ToString());
            }

            await next(context);
        }
    }
}
