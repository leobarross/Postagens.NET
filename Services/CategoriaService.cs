using Postagens.NET.Data;
using Postagens.NET.Models;

namespace Postagens.NET.Services
{
    public class CategoriaService
    {
        private readonly PostagensDbContext _dbContext;

        public CategoriaService(PostagensDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Categoria> BuscarTodas()
        {
            return _dbContext.Categorias.ToList();
        }

        public Categoria? BuscarPorId(int id)
        {

            var categoria = _dbContext.Categorias.FirstOrDefault(cat => cat.Id == id);
            if (categoria == null)
            {
                throw new Exception("Categoria não encontrada.");
            }
            return categoria;

        }

        public Categoria InserirCategoria(Categoria categoria)
        {
            if (!_dbContext.Categorias.Any(cat => cat.Nome == categoria.Nome))
            {
                _dbContext.Categorias.Add(categoria);
                _dbContext.SaveChanges();
                return categoria;
            }
            else
            {
                throw new Exception("Tag com esse nome já existe.");
            }

        }

        public void UpdateCategoria(Categoria categoria)
        {
            var categoriaExistente = _dbContext.Categorias.Find(categoria.Id);

            if (categoriaExistente == null)
            {
                throw new Exception("Tag não encontrada.");
            }


            if (_dbContext.Categorias.Any(t => t.Nome == categoria.Nome && t.Id != categoria.Id))
            {
                throw new Exception("Já existe uma tag com esse nome.");
            }

            categoriaExistente.Nome = categoria.Nome;

            _dbContext.SaveChanges();
        }
        public void DeletarCategoria(int id)
        {
            var tag = _dbContext.Categorias.Find(id);
            if (tag != null)
            {
                _dbContext.Categorias.Remove(tag);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("Tag não encontrada.");
            }
        }
    }
}
