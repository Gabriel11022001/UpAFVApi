using UpOnlineAFVApi.DTOs;

namespace UpOnlineAFVApi.Servico
{
    public interface INivelAcessoServico
    {

        Task<Resposta<NivelAcessoUsuarioDTO>> CadastrarNivelAcesso(NivelAcessoUsuarioDTO nivelAcessoUsuarioDTOCadastrar);

        Task<Resposta<List<NivelAcessoUsuarioDTO>>> CadastrarMultiplosNiveisAcessos(List<NivelAcessoUsuarioDTO> niveisAcessoDTOSCadastrar);

    }
}
