using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.App.Controllers.Api.Models;
using Store.App.Controllers.Api.Models.Requests;
using Store.App.Core;

namespace Store.App.Controllers.Api
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartsController : Controller
    {
        private readonly ICartService cartService;
        private readonly IMapper mapper;

        public CartsController(IMapper mapper, ICartService cartService)
        {
            this.mapper = mapper;
            this.cartService = cartService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CartResource), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCart()
        {
            var id = HttpContext.GetCartId();
            var cart = await cartService.FindCart(id);

            if (cart == null) return NotFound();

            return Ok(mapper.Map<CartResource>(cart));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddItems([FromBody] AddUpdateCartItemsRequest request)
        {
            var id = HttpContext.GetCartId();
            var cart = await cartService.FindCart(id);

            if (cart == null) return NotFound();

            foreach (var item in request.Items) await cartService.AddItemToCart(id, item.ProductId, item.Amount);

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateItems([FromBody] AddUpdateCartItemsRequest request)
        {
            var id = HttpContext.GetCartId();
            var cart = await cartService.FindCart(id);

            if (cart == null) return NotFound();

            var requestIds = request.Items.Select(i => i.ProductId).ToArray();
            var cartIds = cart.CartItems.Select(i => i.ProductId).ToArray();

            if (!requestIds.All(ci => cartIds.Contains(ci)))
            {
                return BadRequest("All items should be already in cart");
            }

            foreach (var item in request.Items)
            {
                await cartService.ChangeItemCount(id, item.ProductId, item.Amount);
            }

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ClearCart()
        {
            var id = HttpContext.GetCartId();
            var cart = await cartService.FindCart(id);

            if (cart == null) return NotFound();

            await cartService.ClearCart(id);
            return NoContent();
        }
    }
}
