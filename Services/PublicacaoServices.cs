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
                    var tag = _context.Tags.Find(tagId);
                    if (tag != null)
                    {
                        publi.PublicacaoTags.Add(new PublicacaoTag { Publicacao = publi, TagId = tagId, Tag = tag });

                    }
                }
            }

            await _context.SaveChangesAsync(); 
        }

        public async Task<Publicacao?> VerDetalhes(int id)
        {
            var publicacao = await _context.Publicacoes
            .Include(p => p.PublicacaoTags)
                .ThenInclude(pt => pt.Tag) 
            .FirstOrDefaultAsync(p => p.Id == id);

            return publicacao;
        }

        public async Task UpdateAsync(Publicacao publi, List<int> tagIds)
        {       
            var publicacaoExistente = await _context.Publicacoes
                .Include(p => p.PublicacaoTags)
                .FirstOrDefaultAsync(p => p.Id == publi.Id);

            if (publicacaoExistente == null)
            {
                throw new Exception("Id não encontrado.");
            }

            if (await _context.Publicacoes.AnyAsync(p => p.Titulo == publi.Titulo && p.Id != publi.Id))
            {
                throw new Exception("Publicação com esse título já existe.");
            }

            publicacaoExistente.Titulo = publi.Titulo;
            publicacaoExistente.Conteudo = publi.Conteudo;

            // Mantém a imagem existente se nenhuma nova imagem for fornecida
            if (!string.IsNullOrEmpty(publi.Imagem))
            {
                publicacaoExistente.Imagem = publi.Imagem;
            }

            publicacaoExistente.PublicacaoTags.Clear();

            if (tagIds != null && tagIds.Count > 0)
            {
                foreach (var tagId in tagIds)
                {
                    var tag = await _context.Tags.FindAsync(tagId);
                    if (tag != null)
                    {
                        publicacaoExistente.PublicacaoTags.Add(new PublicacaoTag { PublicacaoId = publicacaoExistente.Id, TagId = tagId, Tag = tag });
                    }
                }
            }
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
