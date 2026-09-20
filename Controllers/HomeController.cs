using Blog.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [ApiController]
    [ApiKey]
    [Route("")]

    public class HomeController : ControllerBase
    {
        [HttpGet("")]

        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet("teste")]
        public IActionResult Teste()
        {
            return Ok(new
            {
                nome = "andre",
                sobrenome = "lucas"
            });
        }

    }
}