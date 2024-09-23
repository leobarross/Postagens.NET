using Microsoft.AspNetCore.Mvc;
using Postagens.NET.Models;
using Postagens.NET.Services;

namespace Postagens.NET.Controllers
{
    public class TagsController : Controller
    {
        private readonly TagService _tagService;

        public TagsController(TagService tagService)
        {
            _tagService = tagService;
        }

        public IActionResult Index()
        {
            var tags = _tagService.BuscarTodas();
            return View(tags);
        }

        public IActionResult Cadastro(int? id)
        {
            Tag? tag = null;

            if (id.HasValue)
            {
                tag = _tagService.BuscarPorId(id.Value);
                if (tag == null)
                {
                    return NotFound();
                }
            }

            return View(tag);  // Retorna a view com a tag carregada ou null
        }


        [HttpPost]
        public IActionResult Cadastro([FromBody] Tag tag)
        {
            _tagService.InserirTag(tag);
            return Ok(tag);
        }

        [HttpGet]
        public IActionResult BuscarPorId(int id)
        {
            var tag = _tagService.BuscarPorId(id);
            if (tag == null)
            {
                return NotFound();
            }
            return Ok(tag);
        }
        
        [HttpPut]
        public IActionResult Editar(int id, [FromBody] Tag tagAtualizada)
        {
            if (id != tagAtualizada.Id)
            {
                return BadRequest("O ID da tag não corresponde ao ID da URL.");
            }

            try
            {
                _tagService.UpdateTag(tagAtualizada);
                return Ok(tagAtualizada);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        public IActionResult Excluir(int id)
        {
            _tagService.DeletarTag(id);
            return Ok();
        }
    }
}
