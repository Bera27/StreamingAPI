namespace StreamingAPI.ViewModels
{
    public class PlaylistViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Usuario { get; set; }
        public int UsuarioId { get; set; }
        public List<string> Items { get; set; } = new();
    }
}