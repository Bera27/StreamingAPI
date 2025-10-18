namespace StreamingAPI.Models
{
    public class Conteudo
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public string FileUrl { get; set; }

        public int CriadorId { get; set; }
        public Usuario Criador { get; set; }
        public List<ItemPlaylist> Items { get; set; } = new();
    }
}
