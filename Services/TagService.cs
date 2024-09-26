using Microsoft.EntityFrameworkCore;
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

        public Tag? BuscarPorId(int id)
        {

            var tag = _dbContext.Tags.FirstOrDefault(tag => tag.Id == id);
            if (tag == null)
            {
                throw new Exception("Tag não encontrada.");
            }
            return tag;

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

        public void UpdateTag(Tag tag)
        {
            var tagExistente = _dbContext.Tags.Find(tag.Id);

            if (tagExistente == null)
            {
                throw new Exception("Tag não encontrada.");
            }

            
            if (_dbContext.Tags.Any(t => t.Nome == tag.Nome && t.Id != tag.Id))
            {
                throw new Exception("Já existe uma tag com esse nome.");
            }
            
            tagExistente.Nome = tag.Nome;
           
            _dbContext.SaveChanges();
        }
        public void DeletarTag(int id)
        {
            var tag = _dbContext.Tags.Find(id);
            if (tag != null)
            {
                _dbContext.Tags.Remove(tag);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("Tag não encontrada.");
            }
        }
    }
}
