using Microsoft.AspNetCore.Mvc;
using Postagens.NET.Services;

namespace Postagens.NET.Controllers
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
    }
}
