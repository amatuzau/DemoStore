using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Store.App.Identity
{
    public class CartOwnerOrAdminHandler : AuthorizationHandler<CartOwnerOrAdmin>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            CartOwnerOrAdmin requirement)
        {
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if (context.Resource is DefaultHttpContext ctx)
            {
                var cartId = ctx.Request.RouteValues["cartId"]?.ToString();

                var cartClaim = context.User.FindFirstValue(Constants.CartClaimName);

                if (cartId == cartClaim)
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }

            return Task.CompletedTask;
        }
    }
}
