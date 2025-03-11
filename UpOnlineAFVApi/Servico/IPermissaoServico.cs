using UpOnlineAFVApi.DTOs;

namespace UpOnlineAFVApi.Servico
{
    public interface IPermissaoServico
    {

        Task<Resposta<PermissaoNivelAcessoUsuarioDTO>> CadastrarPermissao(PermissaoNivelAcessoUsuarioDTO permissaoNivelAcessoUsuarioDTO);

    }
}
