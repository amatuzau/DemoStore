using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.App.Controllers.Api.Models;
using Store.Core;
using Store.Core.Queries;
using Store.DAL;
using Store.DAL.Models;

namespace Store.App.Controllers.Api
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;
        private readonly IMapper mapper;

        public ProductsController(IProductsService productsService, IMapper mapper)
        {
            this.productsService = productsService;
            this.mapper = mapper;
        }
        
        // GET
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductsByCategory([FromQuery] ProductQuery query)
        {
            var pagedResult = await productsService.GetProducts(query);
            HttpContext.Response.Headers.Add("x-total-count", pagedResult.TotalCount.ToString());
            
            return Ok(mapper.Map<IEnumerable<ProductResource>>(pagedResult.Items));
        }
    }
}