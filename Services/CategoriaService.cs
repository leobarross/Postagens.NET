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
    }
}
