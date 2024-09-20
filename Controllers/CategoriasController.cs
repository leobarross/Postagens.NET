using Microsoft.AspNetCore.Mvc;

namespace Postagens.NET.Controllers
{
    public class CategoriasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
