using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UpOnlineAFVApi.Migrations
{
    /// <inheritdoc />
    public partial class MigrationAlterarColunasTabelaClientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientePessoaFisicaId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "ClientePessoaJuridicaId",
                table: "Clientes");

            migrationBuilder.AddColumn<string>(
                name: "TipoPessoa",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoPessoa",
                table: "Clientes");

            migrationBuilder.AddColumn<int>(
                name: "ClientePessoaFisicaId",
                table: "Clientes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientePessoaJuridicaId",
                table: "Clientes",
                type: "int",
                nullable: true);
        }
    }
}
