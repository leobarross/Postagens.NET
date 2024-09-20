using Postagens.NET.Data;
using Postagens.NET.Models;

namespace Postagens.NET.Services
{
    public class TagService
    {
        private readonly PostagensDbContext _dbContext;

        public TagService(PostagensDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Tag> BuscarTodas()
        {
            return _dbContext.Tags.ToList();
        }

        public Tag InserirTag(Tag tag)
        {
            if (!_dbContext.Tags.Any(t => t.Nome == tag.Nome))
            {
                _dbContext.Tags.Add(tag);
                _dbContext.SaveChanges();
                return tag;
            }
            else
            {
                throw new Exception("Tag com esse nome já existe.");
            }

        }

    }
}
