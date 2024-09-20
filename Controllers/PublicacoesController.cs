using Microsoft.AspNetCore.Mvc;

namespace Postagens.NET.Controllers
{
    public class PublicacoesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
