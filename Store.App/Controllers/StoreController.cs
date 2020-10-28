using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Store.App.Core;
using Store.App.Models;
using Store.DAL.Models;
using FluentValidation;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Store.App.Controllers
{
    public class StoreController : Controller
    {
        private readonly IProductsService productsService;
        private readonly ILogger<StoreController> logger;

        public StoreController(IProductsService productsService, ILogger<StoreController> logger)
        {
            this.productsService = productsService;
            this.logger = logger;
        }

        public async Task<IActionResult> Index(decimal price = 0, int page = 0, int pageSize = 10)
        {
            IEnumerable<Product> products;

            if (price > 0) {
                products = productsService.GetProductsFilteredByPrice(price).Skip(pageSize * page).Take(pageSize);
            } else
            {
                products = (await productsService.GetProducts()).Skip(pageSize * page).Take(pageSize);
            }

            var model = new StoreViewModel
            {
                Categories = (await productsService.GetCategoriesWithProducts()).ToArray(),
                Products = products.ToArray()
            };

            return View(model);
        }

        public async Task<IActionResult> Index2()
        {
            var categories = await productsService.GetCategoriesWithProducts();
            return View(categories);
        }

        public async Task<IActionResult> Buy([FromRoute] int id)
        {
            var product = await productsService.GetProductById(id);
            return View("Confirm", product);
        }

        public async Task<IActionResult> Create()
        {
            var list = new SelectList(await productsService.GetCategories(), "Id", "Name");
            ViewBag.Categories = list;
            return View("ProductCreateEdit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductCreateEditModel model)
        {
            if (ModelState.IsValid)
            {
                logger.LogDebug(JsonConvert.SerializeObject(model));
            } else
            {
                return View("ProductCreateEdit", model);
            }
            return RedirectToAction("Index");
        }
    }


}