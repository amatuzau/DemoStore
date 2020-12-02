using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.App.Core;
using Store.App.Identity;
using Store.App.Models;

namespace Store.App.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var cartId = User.GetCartId();
            var cart = await cartService.FindCart(cartId);
            var items = cart.CartItems.Select(ci => new CartViewModel
            {
                ProductId = ci.ProductId,
                ProductName = ci.Product.Name,
                Count = ci.Count
            });

            return View(items);
        }

        public async Task<IActionResult> Add(int id, int count)
        {
            await cartService.AddItemToCart(User.GetCartId(), id, count);
            return RedirectToAction("Index", "Store");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int productId)
        {
            await cartService.RemoveItemFromCart(User.GetCartId(), productId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeCount(int id, int count)
        {
            var cartId = User.GetCartId();
            if (count > 0)
                await cartService.ChangeItemCount(cartId, id, count);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            await cartService.ClearCart(User.GetCartId());
            return RedirectToAction("Index", "Store");
        }
    }
}
