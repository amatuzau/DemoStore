using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.App.Core;
using Store.App.Models;
using Store.Core;

namespace Store.App.Controllers
{
    [Authorize]
    public class CartController : Controller
    {

        private readonly ICartService cartService;
        private readonly IProductsService productsService;

        public CartController(ICartService cartService, IProductsService productsService)
        {
            this.cartService = cartService;
            this.productsService = productsService;
        }

        public async Task<IActionResult> Index()
        {
            var cartId = HttpContext.GetCartId();
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
            await cartService.AddItemToCart(HttpContext.GetCartId(), id, count);
            return RedirectToAction("Index", "Store");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int productId)
        {
            await cartService.RemoveItemFromCart(HttpContext.GetCartId(), productId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeCount(int id, int count)
        {
            var cartId = HttpContext.GetCartId();
            if (count > 0)
                await cartService.ChangeItemCount(cartId, id, count);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            await cartService.ClearCart(HttpContext.GetCartId());
            return RedirectToAction("Index", "Store");
        }


    }
}
