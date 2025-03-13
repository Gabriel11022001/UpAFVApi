using Microsoft.EntityFrameworkCore;
using UpOnlineAFVApi.Models;

namespace UpOnlineAFVApi.Contexto
{
    public class ApiDbContexto: DbContext
    {

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Notificacao> Notificacoes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<NivelAcessoUsuario> NiveisAcessoUsuarios { get; set; }
        public DbSet<PermissaoNivelAcessoUsuario> PermissoesNiveisAcessosUsuarios { get; set; }
        public DbSet<TokenUsuario> TokensUsuarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        public ApiDbContexto(DbContextOptions<ApiDbContexto> contexto): base(contexto) { }

    }
}
