using UpOnlineAFVApi.Models;

namespace UpOnlineAFVApi.Repositorio
{
    public interface INivelAcessoRepositorio
    {

        Task<NivelAcessoUsuario> BuscarNivelAcessoPeloId(int id);

        Task CadastrarNivelAcesso(NivelAcessoUsuario nivelAcessoUsuarioCadastrar);

        Task IniciarTransacao();

        Task CommitarTransacao();

        Task RollbackTransacao();

    }
}
