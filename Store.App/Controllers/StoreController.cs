using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Models;
using Microsoft.AspNetCore.Mvc;
using Store.Core;

namespace Store.Controllers
{
    public class StoreController : Controller
    {
        private readonly IProductsService productsService;

        public StoreController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public IActionResult Index()
        {
            var model = new StoreViewModel
            {
                Categories = productsService.GetCategoriesWithProducts().ToArray(),
                Products = productsService.GetProducts().ToArray()
            };

            return View(model);
        }

        public IActionResult Buy([FromRoute] int id)
        {
            var product = productsService.GetProductById(id);
            return View("Confirm", product);
        }
    }
}