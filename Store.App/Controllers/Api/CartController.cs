using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.App.Controllers.Api.Models;
using Store.App.Controllers.Api.Models.Requests;
using Store.App.Core;
using Store.App.Identity;

namespace Store.App.Controllers.Api
{
    [Authorize(Policy = nameof(CartOwnerOrAdmin))]
    [Route("api/v1/[controller]/{cartId}")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        private readonly IMapper mapper;

        public CartController(IMapper mapper, ICartService cartService)
        {
            this.mapper = mapper;
            this.cartService = cartService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CartResource), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCart(int cartId)
        {
            var cart = await cartService.FindCart(cartId);

            if (cart == null) return NotFound();

            return Ok(mapper.Map<CartResource>(cart));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddItems(int cartId, [FromBody] AddUpdateCartItemsRequest request)
        {
            var cart = await cartService.FindCart(cartId);

            if (cart == null) return NotFound();

            foreach (var item in request.Items) await cartService.AddItemToCart(cart.Id, item.ProductId, item.Amount);

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateItems(int cartId, [FromBody] AddUpdateCartItemsRequest request)
        {
            var cart = await cartService.FindCart(cartId);

            if (cart == null) return NotFound();

            var requestIds = request.Items.Select(i => i.ProductId).ToArray();
            var cartIds = cart.CartItems.Select(i => i.ProductId).ToArray();

            if (!requestIds.All(ci => cartIds.Contains(ci))) return BadRequest("All items should be already in cart");

            foreach (var item in request.Items) await cartService.ChangeItemCount(cart.Id, item.ProductId, item.Amount);

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ClearCart(int cartId)
        {
            await cartService.ClearCart(cartId);
            return NoContent();
        }
    }
}
