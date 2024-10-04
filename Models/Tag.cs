using System.ComponentModel.DataAnnotations;

namespace Postagens.NET.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [Required]
        public required string Nome { get; set; }

        // Relacionamento direto com Publicacoes
        public virtual ICollection<PublicacaoTag> PublicacaoTags { get; set; } = new List<PublicacaoTag>();
    }
}
