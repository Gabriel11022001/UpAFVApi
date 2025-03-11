using UpOnlineAFVApi.Models;

namespace UpOnlineAFVApi.Repositorio
{
    public interface IPermissaoRepositorio
    {

        Task CadastrarPermissao(PermissaoNivelAcessoUsuario permissaoNivelAcessoUsuarioCadastrar);

        Task<PermissaoNivelAcessoUsuario> BuscarPermissaoPeloId(int idPermissao);

    }
}
