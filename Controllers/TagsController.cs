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
        
        [HttpPost]
        public IActionResult Excluir(Tag tag)
        {
            if(tag.Id > 0)
            {
            _tagService.DeletarTag(tag.Id);
             return RedirectToAction(nameof(Index));
            }
            else
            {
                throw new Exception();
            }

        }
    }
}
