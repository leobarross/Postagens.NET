using Microsoft.AspNetCore.Mvc;
using Postagens.NET.Models;
using Postagens.NET.Services;

namespace Postagens.NET.Controllers
{
    public class TagsController : Controller
    {
        private readonly TagService _service;

        public TagsController(TagService tagService)
        {
            _service = tagService;
        }

        public async Task<IActionResult> Index()
        {
            var tags = await _service.BuscarTagsAsync();
            return View(tags);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Cadastro(Tag tag)
        {
            if (tag.Id > 0)
            {
                await _service.UpdateAsync(tag);
                return RedirectToAction("Index");
            }
            else
            {
                await _service.InsertAsync(tag);
                return RedirectToAction("Index");
            }
            
        }

        [HttpGet]
        public async Task <IActionResult> BuscarPorId(int id)
        {
            var tag =  await _service.BuscarPorIdAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            return Ok(tag);
        }
        
        [HttpPost]
        public async Task <IActionResult> Excluir(Tag tag)
        {
            if(tag.Id > 0)
            {
             await _service.DeleteAsync(tag.Id);
             return RedirectToAction(nameof(Index));
            }
            else
            {
                throw new Exception();
            }

        }
    }
}
