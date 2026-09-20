using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    public class CategoryController : ControllerBase
    {
        [HttpGet("v1/categories")]
        [Authorize]
        public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context)
        {
            try
            {
                var categories = await context.Categories.ToListAsync();
                return Ok(new ResultViewModel<List<Category>>(categories));

            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("CGA02 - falha ao lista categorias"));
            }
 
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
        
        public async Task<IActionResult> PostAsync([FromBody] EditorCategoryViewModel model, [FromServices] BlogDataContext context)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));

                var category = new Category
                {
                    Id = 0,
                    Name = model.Name,
                    Slug = model.Slug,
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
                return Created($"v1/categories/{category.Id}", new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("CTPT01 - Nao possivel incluir a categoria"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Category>("CTPT02 - Falha interna do servidor"));
            }

        }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] EditorCategoryViewModel model, [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound(new ResultViewModel<Category>("CTPUT01 - Categoria nao encontrada"));

                category.Name = model.Name;
                category.Slug = model.Slug;
                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("CTPUT01 - Nao possivel alterar a categoria"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Category>("CTPT02 - Falha interna do servidor"));
            }
        }

        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] BlogDataContext context)
        {
            try
            {

                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound(new ResultViewModel<Category>("CTDEL01 - Categoria nao encontrada"));

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("CTDEL01 - Nao possivel deletar a categoria"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Category>("CTDEL02 - Falha interna do servidor"));
            }
        }

    }
}