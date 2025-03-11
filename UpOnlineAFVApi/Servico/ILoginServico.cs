using UpOnlineAFVApi.DTOs;

namespace UpOnlineAFVApi.Servico
{
    public interface ILoginServico
    {

        Task<Resposta<UsuarioDTO>> Login(String email, String senha);

    }
}
