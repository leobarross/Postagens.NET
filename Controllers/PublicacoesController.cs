using Microsoft.AspNetCore.Mvc;
using Postagens.NET.Models;
using Postagens.NET.Models.ViewModels;
using Postagens.NET.Services;
using Postagens.NET.Services.Exceptions;
using System.Diagnostics;

namespace Pospubliens.NET.Controllers
{
    public class PublicacoesController : Controller
    {
        private readonly UploadService _uploadService;
        private readonly PublicacaoServices _service;

        public PublicacoesController(UploadService uploadService, PublicacaoServices service)
        {
            _uploadService = uploadService;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var publicacoes = await _service.ListarPublicacoesAsync();
            return View(publicacoes);
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastro(Publicacao publi, List<int> tagIds, IFormFile imagem)
        {
            try
            {
                if (imagem != null && imagem.Length > 0)
                {
                    publi.Imagem = await _uploadService.SaveFileAsync(imagem);
                }
                else if (publi.Id > 0) // Ao editar, mantenha a imagem existente
                {
                    var publicacaoExistente = await _service.BuscarPorIdAsync(publi.Id);
                    publi.Imagem = publicacaoExistente.Imagem; // Mantenha a imagem existente
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao salvar a imagem: " + ex.Message);
            }

            if (publi.Id > 0)
            {
                await _service.UpdateAsync(publi, tagIds);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                await _service.InsertAsync(publi, tagIds);
                return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Detalhes(int id)
        {
            var publicacao = await _service.VerDetalhes(id);

            if (publicacao == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Publicação não encontrada." });
            }

            return View(publicacao); 
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
