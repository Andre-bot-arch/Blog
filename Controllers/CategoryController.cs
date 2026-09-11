using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    public class CategoryController : ControllerBase
    {
        [HttpGet("v1/categories")]
        public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context) 
        {
            var categories = await context.Categories.ToListAsync();
            return Ok(categories);
            
        }

        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] BlogDataContext context) 
        {
            var categorie = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if(categorie == null)
               return NotFound();

            return Ok(categorie);
            
        }

        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync([FromBody] Category model, [FromServices] BlogDataContext context) 
        {
            context.Categories.AddAsync(model);
            context.SaveChangesAsync();
            
        }
        
    }
}