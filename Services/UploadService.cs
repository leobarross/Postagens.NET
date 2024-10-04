namespace Postagens.NET.Services
{
    public class UploadService
    {
        private readonly string _pastaImagens;

        public UploadService()
        {
            // Define o caminho para a pasta de imagens
            _pastaImagens = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagens");

            // Cria a pasta se não existir
            if (!Directory.Exists(_pastaImagens))
            {
                Directory.CreateDirectory(_pastaImagens);
            }
        }

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Arquivo não pode ser nulo ou vazio.");

            // Gera um nome único para o arquivo
            var nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var caminhoArquivo = Path.Combine(_pastaImagens, nomeArquivo);

            using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return "/imagens/" + nomeArquivo; // Retorna o caminho relativo
        }
    }
}
