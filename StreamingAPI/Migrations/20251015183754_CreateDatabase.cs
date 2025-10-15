using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamingAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    SenhaHash = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conteudo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Tipo = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    FileUrl = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    CriadorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conteudo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conteudo_Usuario_CriadorId",
                        column: x => x.CriadorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Playlist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Playlist_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemPlaylist",
                columns: table => new
                {
                    PlaylistId = table.Column<int>(type: "int", nullable: false),
                    ConteudoId = table.Column<int>(type: "int", nullable: false),
                    Ordem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPlaylist", x => new { x.PlaylistId, x.ConteudoId });
                    table.ForeignKey(
                        name: "FK_ItemPlaylist_Conteudo_ConteudoId",
                        column: x => x.ConteudoId,
                        principalTable: "Conteudo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemPlaylist_Playlist_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conteudo_CriadorId",
                table: "Conteudo",
                column: "CriadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Conteudo_Titulo",
                table: "Conteudo",
                column: "Titulo");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPlaylist_ConteudoId",
                table: "ItemPlaylist",
                column: "ConteudoId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlist_UsuarioId",
                table: "Playlist",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Email",
                table: "Usuario",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemPlaylist");

            migrationBuilder.DropTable(
                name: "Conteudo");

            migrationBuilder.DropTable(
                name: "Playlist");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
