using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Postagens.NET.Models;
using Postagens.NET.Models.ViewModels;
using Postagens.NET.Services;
using Postagens.NET.Services.Exceptions;
using System.Diagnostics;

namespace Pospubliens.NET.Controllers
{
    public class PublicacoesController : Controller
    {
        private readonly PublicacaoServices _service;

        public PublicacoesController(PublicacaoServices service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var publicacoes = await _service.ListarPublicacoesAsync();
            return View(publicacoes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastro(Publicacao publi)
        {
            if(publi.Id > 0)
            {
                await _service.UpdateAsync(publi);
                return RedirectToAction("Index");
            }
            else
            {
                await _service.InsertAsync(publi);
                return RedirectToAction("Index");
            }           
        }

        public async Task<IActionResult> BuscarPublicacao(int id)
        {
            var publi = await _service.BuscarPorIdAsync(id);
            if (publi == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Publicação não encontrada." });
            }
            return Ok(publi);
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(int? id)
        {
            try
            {
                // Verifica se o ID foi fornecido
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "ID não foi fornecido." });
                }

                // Busca a publicação pelo ID
                var publi = await _service.BuscarPorIdAsync(id.Value);
                if (publi == null)
                {
                    // Redireciona para a página de erro se a publicação não for encontrada
                    return RedirectToAction(nameof(Error), new { message = "Publicação não encontrada." });
                }

                // Exclui a publicação
                await _service.DeleteAsync(publi.Id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                // Trata exceções de integridade e redireciona para a página de erro
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }


        public IActionResult Error(string Message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = Message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
