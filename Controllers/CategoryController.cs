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
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound();

            return Ok(category);

        }

        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync([FromBody] Category model, [FromServices] BlogDataContext context)
        {
            try
            {
                await context.Categories.AddAsync(model);
                await context.SaveChangesAsync();
                return Created($"v1/categories/{model.Id}", model);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "CTPT01 - Nao possivel incluir a categoria");
            }
            catch (Exception e)
            {
                return StatusCode(500, "CTPT02 - Falha interna do servidor");
            }

        }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] Category model, [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound();

                category.Name = model.Name;
                category.Slug = model.Slug;
                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return Created($"v1/categories/{model.Id}", model);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "CTPUT01 - Nao possivel alterar a categoria");
            }
            catch (Exception e)
            {
                return StatusCode(500, "CTPT02 - Falha interna do servidor");
            }
        }

        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] BlogDataContext context)
        {
            try
            {

                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound();

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return Ok(category);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "CTDEL01 - Nao possivel deletar a categoria");
            }
            catch (Exception e)
            {
                return StatusCode(500, "CTDEL02 - Falha interna do servidor");
            }
        }

    }
}