﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.App.Core;
using Store.App.Models;

namespace Store.App.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private const string CartCookieName = "DemoStore.CartId";
        private readonly ICartService cartService;
        private readonly IProductsService productsService;

        public CartController(ICartService cartService, IProductsService productsService)
        {
            this.cartService = cartService;
            this.productsService = productsService;
        }

        public async Task<IActionResult> Index()
        {
            var cartId = await GetSetCartId();
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
            var product = await productsService.GetProductById(id);
            await cartService.AddItemToCart(await GetSetCartId(), product, count);
            return RedirectToAction("Index", "Store");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await productsService.GetProductById(id);
            await cartService.RemoveItemFromCart(await GetSetCartId(), product);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeCount(int id, int count)
        {
            var product = await productsService.GetProductById(id);
            var cartId = await GetSetCartId();
            if (count > 0)
                await cartService.ChangeItemCount(cartId, product, count);
            else
                await cartService.RemoveItemFromCart(cartId, product);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            await cartService.ClearCart(await GetSetCartId());
            return RedirectToAction("Index", "Store");
        }

        private async Task<int> GetSetCartId()
        {
            int id;

            if (HttpContext.Request.Cookies.ContainsKey(CartCookieName))
                id = int.Parse(HttpContext.Request.Cookies[CartCookieName]);
            else
                id = await cartService.CreateCart();

            HttpContext.Response.Cookies.Append(CartCookieName, id.ToString());
            return id;
        }
    }
}