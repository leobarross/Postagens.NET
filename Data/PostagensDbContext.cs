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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeamento muitos-para-muitos entre Publicacao e Tag (apenas na Publicacao)
            modelBuilder.Entity<Publicacao>()
                .HasMany(p => p.Tags)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "PublicacaoTag",
                    j => j.HasOne<Tag>().WithMany().HasForeignKey("TagId"),
                    j => j.HasOne<Publicacao>().WithMany().HasForeignKey("PublicacaoId"));

            // Mapeamento um-para-muitos entre Publicacao e Categoria (apenas na Publicacao)
            modelBuilder.Entity<Publicacao>()
                .HasOne(p => p.Categoria)
                .WithMany()
                .HasForeignKey(p => p.CategoriaId);
        }
    }
}
