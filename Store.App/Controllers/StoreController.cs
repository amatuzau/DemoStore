using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.App.Core;
using Store.App.Models;

namespace Store.App.Controllers
{
    public class StoreController : Controller
    {
        private readonly IProductsService productsService;

        public StoreController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new StoreViewModel
            {
                Categories = (await productsService.GetCategoriesWithProducts()).ToArray(),
                Products = (await productsService.GetProducts()).ToArray()
            };

            return View(model);
        }

        public async Task<IActionResult> Buy([FromRoute] int id)
        {
            var product = await productsService.GetProductById(id);
            return View("Confirm", product);
        }
    }
}