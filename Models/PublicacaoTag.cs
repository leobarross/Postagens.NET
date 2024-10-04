namespace Postagens.NET.Models
{
    public class PublicacaoTag
    {
        public int PublicacaoId { get; set; }
        public Publicacao? Publicacao { get; set; }

        public int TagId { get; set; }
        public Tag? Tag { get; set; }
    }
}
