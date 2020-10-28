using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.DAL;
using Store.DAL.Models;

namespace Store.App.Controllers.Api.Models
{
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly StoreContext context;

        public ProductsController(StoreContext context)
        {
            this.context = context;
        }
        
        // GET
        [Route("api/v1/categories/{categoryId}/products")]
        public async Task<IEnumerable<Product>> GetProductsByCategory(int categoryId)
        {
            return await context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
        }
    }
}