using UpOnlineAFVApi.Models;

namespace UpOnlineAFVApi.Repositorio
{
    public interface ITokenRepositorio
    {

        Task CadastrarToken(TokenUsuario tokenUsuarioCadastrar);

        Task<TokenUsuario> BuscarToken(String token);

        Task DeletarToken(int idTokenDeletar);

    }
}
