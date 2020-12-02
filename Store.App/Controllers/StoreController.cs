using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Store.App.Models;
using Store.Core;

namespace Store.App.Controllers
{
    public class StoreController : Controller
    {
        private readonly ILogger<StoreController> logger;
        private readonly IProductsService productsService;

        public StoreController(IProductsService productsService, ILogger<StoreController> logger)
        {
            this.productsService = productsService;
            this.logger = logger;
        }

        public async Task<IActionResult> Index(decimal price = 0, int page = 0, int pageSize = 10)
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
                logger.LogDebug(JsonConvert.SerializeObject(model));
            else
                return View("ProductCreateEdit", model);
            return RedirectToAction("Index");
        }
    }
}
