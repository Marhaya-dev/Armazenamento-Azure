using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoA3.Models;
using ProjetoA3.UseCases;

namespace ProjetoA3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ArquivosController : Controller
    {

        [HttpPost]
        public ActionResult<Arquivo> Post([FromForm] IFormFile file, [FromServices] UpdateArquivoCase UpdateArquivoCase)
        {
            return UpdateArquivoCase.Execute(file);
        }

        
        [HttpDelete("{id}")]
        public ActionResult<Arquivo> Delete(int id, [FromServices] DeleteArquivoCase deleteArquivoCase)
        {
            deleteArquivoCase.Execute(id);
            return new NoContentResult();
        }

        [HttpGet()]
        public ActionResult<Arquivo> Get([FromServices] ListArquivoCase ListArquivoCase)
        {
            return new OkObjectResult(ListArquivoCase.Execute());
        }
    }
}
