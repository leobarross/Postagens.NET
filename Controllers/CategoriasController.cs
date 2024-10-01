using Microsoft.AspNetCore.Mvc;
using Postagens.NET.Models;
using Postagens.NET.Services;

namespace Poscategoriaens.NET.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly CategoriaService _service;

        public CategoriasController(CategoriaService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var categorias = await _service.BuscarCategoriasAsync();
            return View(categorias);
        }

        [HttpGet]
        public async Task<JsonResult> ListarCategorias()
        {
            var categorias = await _service.BuscarCategoriasAsync();
            return Json(categorias);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastro(Categoria categoria)
        {
            if (categoria.Id > 0)
            {
                await _service.UpdateAsync(categoria);
                return RedirectToAction("Index");

            }
            else
            {
                await _service.InsertAsync(categoria);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> BuscarUmaCategoria(int id)
        {
            var categoria = await _service.BuscarPorIdAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return Ok(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(Categoria categoria)
        {
            if (categoria.Id > 0)
            {
                await _service.DeleteAsync(categoria.Id);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                throw new Exception();
            }

        }
    }
}
