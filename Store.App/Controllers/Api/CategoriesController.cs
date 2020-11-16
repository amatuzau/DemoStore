﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.App.Filters;
using Store.DAL;
using Store.DAL.Models;

namespace Store.App.Controllers.Api
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [FormatFilter]
    public class CategoriesController : ControllerBase
    {
        private readonly StoreContext context;

        public CategoriesController(StoreContext context)
        {
            this.context = context;
        }

        /// <summary>
        ///     Get all categories
        /// </summary>
        /// <returns>List of categories</returns>
        [HttpGet]
        [ServiceFilter(typeof(CacheFilterAttribute))]
        [ProducesResponseType(typeof(ICollection<Category>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await context.Categories.ToListAsync());
        }

        // GET: api/Categories/5
        [HttpGet("{id}.{format?}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await context.Categories.FindAsync(id);

            if (category == null) return NotFound();

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id) return BadRequest();

            context.Entry(category).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            context.Categories.Add(category);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new {id = category.Id}, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            var category = await context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return category;
        }

        private bool CategoryExists(int id)
        {
            return context.Categories.Any(e => e.Id == id);
        }
    }
}
