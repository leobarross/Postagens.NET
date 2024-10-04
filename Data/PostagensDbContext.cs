using Microsoft.EntityFrameworkCore;
using Postagens.NET.Models;

namespace Postagens.NET.Data
{
    public class PostagensDbContext : DbContext
    {
        public PostagensDbContext(DbContextOptions<PostagensDbContext> options) : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Publicacao> Publicacoes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PublicacaoTag> PublicacaoTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PublicacaoTag>()
                .HasKey(pt => new { pt.PublicacaoId, pt.TagId });

            modelBuilder.Entity<PublicacaoTag>()
                .HasOne(pt => pt.Publicacao)
                .WithMany(p => p.PublicacaoTags)
                .HasForeignKey(pt => pt.PublicacaoId);

            modelBuilder.Entity<PublicacaoTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.PublicacaoTags)
                .HasForeignKey(pt => pt.TagId);
        }
    }


}
