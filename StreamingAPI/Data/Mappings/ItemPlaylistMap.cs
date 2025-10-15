using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingAPI.Models;

namespace StreamingAPI.Data.Mappings
{
    public class ItemPlaylistMap : IEntityTypeConfiguration<ItemPlaylist>
    {
        public void Configure(EntityTypeBuilder<ItemPlaylist> builder)
        {
            builder.ToTable("ItemPlaylist");

            builder.HasKey(x => new { x.PlaylistId, x.ConteudoId });

            builder.HasOne(x => x.Playlist)
                .WithMany(p => p.Items)
                .HasForeignKey(x => x.PlaylistId);

            builder.HasOne(x => x.Conteudo)
                .WithMany(c => c.Items)
                .HasForeignKey(x => x.ConteudoId);
        }
    }
}
