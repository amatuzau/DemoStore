using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace Store.App.Identity
{
    public class StoreProfileService : IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var cartIdClaim = context.Subject.FindFirst(Constants.CartClaimName);
            var roleClaims = context.Subject.FindAll(JwtClaimTypes.Role);

            context.IssuedClaims.Add(cartIdClaim);
            context.IssuedClaims.AddRange(roleClaims);

            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.CompletedTask;
        }
    }
}
