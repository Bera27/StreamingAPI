using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingAPI.Models;

namespace StreamingAPI.Data.Mappings
{
    public class ConteudoMap : IEntityTypeConfiguration<Conteudo>
    {
        public void Configure(EntityTypeBuilder<Conteudo> builder)
        {
            builder.ToTable("Conteudo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Titulo)
                .IsRequired()
                .HasColumnName("Titulo")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Tipo)
                .IsRequired()
                .HasColumnName("Tipo")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50);

            builder.Property(x => x.FileUrl)
                .IsRequired()
                .HasColumnName("FileUrl")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255);

            builder.HasOne(c => c.Criador)
                .WithMany(u => u.Conteudos)
                .HasForeignKey(c => c.CriadorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.Titulo, "IX_Conteudo_Titulo");
        }
    }
}
