using System.Security.Claims;

namespace Store.App.Identity
{
    public static class Extensions
    {
        public static int GetCartId(this ClaimsPrincipal principal)
        {
            return int.Parse(principal.FindFirstValue(Constants.CartClaimName));
        }
    }
}
