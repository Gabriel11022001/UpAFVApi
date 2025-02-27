using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UpOnlineAFVApi.Migrations
{
    /// <inheritdoc />
    public partial class MigrationAdicionarTabelasUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NiveisAcessoUsuarios",
                columns: table => new
                {
                    NivelAcessoUsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NiveisAcessoUsuarios", x => x.NivelAcessoUsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "PermissoesNiveisAcessosUsuarios",
                columns: table => new
                {
                    PermissaoNivelAcessoUsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissoesNiveisAcessosUsuarios", x => x.PermissaoNivelAcessoUsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    NivelAcessoUsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK_Usuarios_NiveisAcessoUsuarios_NivelAcessoUsuarioId",
                        column: x => x.NivelAcessoUsuarioId,
                        principalTable: "NiveisAcessoUsuarios",
                        principalColumn: "NivelAcessoUsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NivelAcessoUsuarioPermissaoNivelAcessoUsuario",
                columns: table => new
                {
                    NiveisAcessoNivelAcessoUsuarioId = table.Column<int>(type: "int", nullable: false),
                    PermissoesPermissaoNivelAcessoUsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NivelAcessoUsuarioPermissaoNivelAcessoUsuario", x => new { x.NiveisAcessoNivelAcessoUsuarioId, x.PermissoesPermissaoNivelAcessoUsuarioId });
                    table.ForeignKey(
                        name: "FK_NivelAcessoUsuarioPermissaoNivelAcessoUsuario_NiveisAcessoUsuarios_NiveisAcessoNivelAcessoUsuarioId",
                        column: x => x.NiveisAcessoNivelAcessoUsuarioId,
                        principalTable: "NiveisAcessoUsuarios",
                        principalColumn: "NivelAcessoUsuarioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NivelAcessoUsuarioPermissaoNivelAcessoUsuario_PermissoesNiveisAcessosUsuarios_PermissoesPermissaoNivelAcessoUsuarioId",
                        column: x => x.PermissoesPermissaoNivelAcessoUsuarioId,
                        principalTable: "PermissoesNiveisAcessosUsuarios",
                        principalColumn: "PermissaoNivelAcessoUsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NivelAcessoUsuarioPermissaoNivelAcessoUsuario_PermissoesPermissaoNivelAcessoUsuarioId",
                table: "NivelAcessoUsuarioPermissaoNivelAcessoUsuario",
                column: "PermissoesPermissaoNivelAcessoUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_NivelAcessoUsuarioId",
                table: "Usuarios",
                column: "NivelAcessoUsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NivelAcessoUsuarioPermissaoNivelAcessoUsuario");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "PermissoesNiveisAcessosUsuarios");

            migrationBuilder.DropTable(
                name: "NiveisAcessoUsuarios");
        }
    }
}
