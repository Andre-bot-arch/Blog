using Blog.Data;
using Blog.Models;
using BLog.ViewModels;
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
            try
            {
                var categorie = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if(categorie == null)
                return NotFound();

                return Ok(categorie);
                
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, "CGBI01 - Falha ao listar categorias");
            }
            catch (Exception e)
            {
                return StatusCode(500, "CGBI02 - Falha interna no servidor");
            }
            
            
        }

        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync([FromBody] CreateCategoryViewModel crete, [FromServices] BlogDataContext context) 
        {
            try
            {
                
                var model = new Category
                {
                   Id = 0,
                   Name = crete.Name,   
                   Slug = crete.Slug.ToLower(),  
                };

                await context.Categories.AddAsync(model);
                await context.SaveChangesAsync();
                return Created($"v1/categories/{model.Id}", model);
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, "CPA01 - Falha ao criar categoria");
            }
            catch (Exception e)
            {
                return StatusCode(500, "CPA02 - Falha interna no servidor");
            }
          
        }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] Category model, [FromServices] BlogDataContext context) 
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if(category == null)
                    return NotFound();

                category.Name = model.Name;
                category.Slug = model.Slug;

                await context.SaveChangesAsync();
                return Ok(category);
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, "CPUTA01 - Falha ao atualizar categoria");
            }
            catch (Exception e)
            {
                return StatusCode(500, "CPUTA02 - Falha interna no servidor");
            }
           
        }

        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if(category == null)
                    return NotFound();

                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, "CDA01 - Falha ao deletar categoria");
            }
            catch (Exception e)
            {
                return StatusCode(500, "CDA02 - Falha interna no servidor");
            }
          
        }
    }
}