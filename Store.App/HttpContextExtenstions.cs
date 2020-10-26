using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.App
{
    public static class HttpContextExtenstions
    {
        public static int GetCartId(this HttpContext context)
        {
            return int.Parse(context.Request.Cookies[Constants.CartCookieName]);
        }
    }
}
