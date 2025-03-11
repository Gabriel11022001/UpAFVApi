using UpOnlineAFVApi.DTOs;

namespace UpOnlineAFVApi.Servico
{
    public interface ITokenServico
    {

        Task<TokenDTO> ValidarTokenUsuario(String token);

    }
}
