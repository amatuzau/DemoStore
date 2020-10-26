using System;
using System.Linq;
using DemoStore.Models;
using Microsoft.AspNetCore.Mvc;
using Store.Core;

namespace DemoStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        private readonly IProductsService productsService;
        private const string CartCookieName = "DemoStore.CartId";

        public CartController(ICartService cartService, IProductsService productsService)
        {
            this.cartService = cartService;
            this.productsService = productsService;
        }

        public IActionResult Index()
        {
            var cart = cartService.FindCart(GetSetCartId());
            var items = cart.CartItems.Select(i => new CartViewModel {
                ProductId = int.Parse(i.Key),
                ProductName = productsService.GetProductById(int.Parse(i.Key)).Name,
                Count = i.Value });
            return View(items);
        }

        public IActionResult Add(int id, int count)
        {
            var product = productsService.GetProductById(id);
            cartService.AddItemToCart(GetSetCartId(), product, count);
            return RedirectToAction("Index", "Store");
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            var product = productsService.GetProductById(id);
            cartService.RemoveItemFromCart(GetSetCartId(), product);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeCount(int id, int count)
        {
            var product = productsService.GetProductById(id);
            cartService.ChangeItemCount(GetSetCartId(), product, count);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ClearCart()
        {
            cartService.ClearCart(GetSetCartId());
            return RedirectToAction("Index", "Store");
        }
        private int GetSetCartId()
        {
            if (HttpContext.Request.Cookies.ContainsKey(CartCookieName))
            {
                return int.Parse(HttpContext.Request.Cookies[CartCookieName]);
            } else
            {
                var id = cartService.CreateCart();
                HttpContext.Response.Cookies.Append(CartCookieName, id.ToString());
                return id;
            }
        }
    }
}
