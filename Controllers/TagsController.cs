using Microsoft.AspNetCore.Mvc;

namespace Postagens.NET.Controllers
{
    public class TagsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
