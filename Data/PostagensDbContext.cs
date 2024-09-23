using Microsoft.EntityFrameworkCore;
using Postagens.NET.Models;

namespace Postagens.NET.Data
{
    public class PostagensDbContext: DbContext
    {
        public PostagensDbContext(DbContextOptions<PostagensDbContext> options) : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Publicacao> Publicacoes { get; set; }
        public DbSet<Tag> Tags { get; set; }

    }
}
