using Microsoft.EntityFrameworkCore;
using Postagens.NET.Data;
using Postagens.NET.Models;

namespace Postagens.NET.Services
{
    public class TagService
    {
        private readonly PostagensDbContext _context;

        public TagService(PostagensDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Tag>> BuscarTagsAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag?> BuscarPorIdAsync(int id)
        {
            return await _context.Tags.FindAsync(id);
        }

        public async Task InsertAsync(Tag tag)
        {
            if (await _context.Tags.AnyAsync(t => t.Nome == tag.Nome))
            {
                throw new Exception("Tag com esse nome já existe.");
            }
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tag tag)
        {
            if (!await _context.Tags.AnyAsync(t => t.Id == tag.Id))
            {
                throw new Exception("Id não encontrado");
            }

            if (await _context.Tags.AnyAsync(t => t.Nome == tag.Nome && t.Id != tag.Id))
            {
                throw new Exception("Já existe uma tag com esse nome.");
            }

            _context.Update(tag);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Tag não encontrada.");
            }
        }
    }
}
