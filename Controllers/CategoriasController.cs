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

        public IActionResult Index()
        {
            var categorias = _service.BuscarTodas();
            return View(categorias);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastro(Categoria categoria)
        {
            if (categoria.Id > 0)
            {
                _service.UpdateCategoria(categoria);
                return RedirectToAction("Index");

            }
            else
            {
                _service.InserirCategoria(categoria);
                return RedirectToAction("Index");
            }

        }

        [HttpGet]
        public IActionResult BuscarUmaCategoria(int id)
        {
            var categoria = _service.BuscarPorId(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return Ok(categoria);
        }

        [HttpPost]
        public IActionResult Excluir(Categoria categoria)
        {
            if (categoria.Id > 0)
            {
                _service.DeletarCategoria(categoria.Id);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                throw new Exception();
            }

        }
    }
}
