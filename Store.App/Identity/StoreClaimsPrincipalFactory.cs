using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Store.DAL.Models;

namespace Store.App.Identity
{
    public class StoreClaimsPrincipalFactory : UserClaimsPrincipalFactory<StoreUser>
    {
        public StoreClaimsPrincipalFactory(UserManager<StoreUser> userManager,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(StoreUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("cartId", user.CartId.ToString()));
            return identity;
        }
    }
}
