using UpOnlineAFVApi.DTOs;

namespace UpOnlineAFVApi.Servico
{
    public interface IUsuarioServico
    {

        Task<Resposta<UsuarioDTO>> CadastrarUsuario(String token, UsuarioDTO usuarioDTOCadastrar);

        Task<Resposta<List<UsuarioDTO>>> BuscarUsuarios(String token, int paginaAtual, int elementosPorPagina);

    }
}
