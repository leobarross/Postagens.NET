using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Cadastro()
        {
            return View();
        }
    }
}
