using Microsoft.EntityFrameworkCore;
using StreamingAPI.Data.Mappings;
using StreamingAPI.Models;

namespace StreamingAPI.Data
{
    public class StreamingDataContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Conteudo> Conteudos { get; set; }
        public DbSet<Playlist> Playlists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$;Encrypt=True;TrustServerCertificate=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new ConteudoMap());
            modelBuilder.ApplyConfiguration(new PlaylistMap());
            modelBuilder.ApplyConfiguration(new ItemPlaylistMap());
        }
    }
}
