namespace StreamingAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        
        public IList<Role> Roles { get; set; }
        public List<Conteudo> Conteudos { get; set; }
        public List<Playlist> Playlists { get; set; }
    }
}
