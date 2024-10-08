using Microsoft.EntityFrameworkCore;
using Postagens.NET.Data;
using Postagens.NET.Models;
using Postagens.NET.Services.Exceptions;

namespace Postagens.NET.Services
{
    public class PublicacaoServices
    {
        private readonly PostagensDbContext _context;

        public PublicacaoServices(PostagensDbContext context)
        {
            _context = context;
        }

        public async Task<List<Publicacao>> ListarPublicacoesAsync()
        {
            return await _context.Publicacoes
                .Include(pub => pub.Categoria)
                .Include(pub => pub.PublicacaoTags)
                    .ThenInclude(pt => pt.Tag)
                .ToListAsync();
        }

        public async Task<Publicacao?> BuscarPorIdAsync(int id)
        {
            return await _context.Publicacoes.FindAsync(id);
        }

        public async Task InsertAsync(Publicacao publi, List<int> tagIds)
        {
            if (await _context.Publicacoes.AnyAsync(p => p.Titulo == publi.Titulo))
            {
                throw new Exception("Publicação com esse título já existe.");
            }

            publi.DataPublicacao = DateTime.Now; // Atribui a data atual
            _context.Publicacoes.Add(publi);

            // Verifica se tagIds não está vazio e adiciona à tabela de junção
            if (tagIds != null && tagIds.Count > 0)
            {
                foreach (var tagId in tagIds)
                {
                    publi.PublicacaoTags.Add(new PublicacaoTag { Publicacao = publi, TagId = tagId });
                }
            }

            await _context.SaveChangesAsync(); // Salva todas as alterações no contexto
        }


        public async Task UpdateAsync(Publicacao publi)
        {
            if (!await _context.Publicacoes.AnyAsync(p => p.Id == publi.Id))
            {
                throw new Exception("Id não encontrado");
            }

            if (await _context.Publicacoes.AnyAsync(p => p.Titulo == publi.Titulo))
            {
                throw new Exception("Publicação com esse título já existe.");
            }

            _context.Update(publi);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            try
            {
                var publicacao = await _context.Publicacoes.FindAsync(id);
                if (publicacao == null)
                {
                    
                    throw new Exception("Publicação não encontrada para o ID fornecido.");
                }
                _context.Publicacoes.Remove(publicacao);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Não é possível Excluir a publicação porque existe categoria vinculada.");
            }
         
        }
    }
}
