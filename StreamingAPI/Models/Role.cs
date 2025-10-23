namespace StreamingAPI.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public IList<Usuario> Usuarios { get; set; }
    }
}