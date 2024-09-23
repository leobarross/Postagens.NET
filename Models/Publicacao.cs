using System.ComponentModel.DataAnnotations;

namespace Postagens.NET.Models
{
    public class Publicacao
    {
        public int Id { get; set; }
        [Required]

        public required string Titulo { get; set; }
        public string? Imagem { get; set; }
        [Required]
        public required string Conteudo { get; set; }
        [Required]
        public required DateTime DataPublicacao { get; set; }

        public required Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }

        // Relacionamento com Tags (muitos-para-muitos)
        public virtual ICollection<Tag>? Tags { get; set; } = new List<Tag>();
    }
}
