using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OverKoepelendeOefening_LarsLauryssens.Data;
using OverKoepelendeOefening_LarsLauryssens.Models;

namespace OverKoepelendeOefening_LarsLauryssens.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext context;

        public CategoriesController( AppDbContext appDbContext)
        {
            context = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] int Page = 1, [FromQuery] int PageSize = 5)
        {
            var totalCategories = await context.Categories.CountAsync();

            var results = await context.Categories
                .Include(c=> c.Posts)
                .OrderBy(c=> c.Id)
                .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();


            if (!results.Any())
            {
                return NotFound();
            }

            return Ok(results);
        }

        [HttpGet("details/{id}/posts")]
        public async Task<ActionResult> GetAllPostsByCatId([FromRoute] int id)
        {
            var result = await context.Categories
                .Include(c => c.Posts)
                .FirstOrDefaultAsync(cat => cat.Id == id);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result.Posts);
        }

        [HttpPost] 
        public async Task<IActionResult> AddCategory(Category category )
        {
            if (category == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Gelieve correcte data in te vullen");
            }
            context.Categories.Add(category);   
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllCategories),new {id = category.Id}, category);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory(int id , Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Gelieve geldige data in te geven");
            }
            context.Entry(category).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            context.Categories.Remove(result);
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
