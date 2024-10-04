using System.ComponentModel.DataAnnotations;

namespace Postagens.NET.Models
{
    public class Publicacao
    {
        public int Id { get; set; }
        public required string Titulo { get; set; }
        public string? Imagem { get; set; }
        public required string Conteudo { get; set; }
        public required DateTime DataPublicacao { get; set; }

        public required Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }

        public virtual ICollection<PublicacaoTag> PublicacaoTags { get; set; } = new List<PublicacaoTag>();
    }
}
