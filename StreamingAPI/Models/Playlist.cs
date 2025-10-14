namespace StreamingAPI.Models
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public List<ItemPlaylist> ItemPlaylists { get; set; } = new();
    }
}
