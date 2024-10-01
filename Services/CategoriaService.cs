using Microsoft.EntityFrameworkCore;
using Postagens.NET.Data;
using Postagens.NET.Models;

namespace Postagens.NET.Services
{
    public class CategoriaService
    {
        private readonly PostagensDbContext _context;

        public CategoriaService(PostagensDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Categoria>> BuscarCategoriasAsync()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<Categoria?> BuscarPorIdAsync(int id)
        {
            return  await _context.Categorias.FindAsync(id);
        }

        public async Task InsertAsync(Categoria categoria)
        {
            if (await _context.Categorias.AnyAsync(cat => cat.Nome == categoria.Nome))
            {
                throw new Exception("Categoria com esse nome já existe.");                          
            }
           
                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();

        }

        public async Task UpdateAsync(Categoria categoria)
        {
            if (!await _context.Categorias.AnyAsync(c => c.Id == categoria.Id))
            {
                throw new Exception("Id não encontrado");
            }

            if (await _context.Categorias.AnyAsync(t => t.Nome == categoria.Nome && t.Id != categoria.Id))
            {
                throw new Exception("Já existe uma Categoria com esse nome.");
            }

            _context.Update(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Categoria não encontrada.");
            }
        }
    }
}
