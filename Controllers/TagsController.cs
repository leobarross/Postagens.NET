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
        [ValidateAntiForgeryToken]
        public IActionResult Cadastro(Tag tag)
        {
            if (tag.Id > 0)
            {
                _tagService.UpdateTag(tag);
                return RedirectToAction("Index");

            }
            else
            {
                _tagService.InserirTag(tag);
                return RedirectToAction("Index");
            }
            
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
        [HttpGet]
        public IActionResult Editar(int id)
        {
            var tag = _tagService.BuscarPorId(id);  // Método que busca a tag pelo ID
            if (tag == null)
            {
                return NotFound();
            }

            return PartialView("Cadastro", tag); // Renderiza o modal com os dados preenchidos
        }



        [HttpDelete]
        public IActionResult Excluir(int id)
        {
            _tagService.DeletarTag(id);
            return Ok();
        }
    }
}
