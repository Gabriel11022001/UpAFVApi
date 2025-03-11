using UpOnlineAFVApi.Models;

namespace UpOnlineAFVApi.Repositorio
{
    public interface IUsuarioRepositorio
    {

        Task CadastrarUsuario(Usuario usuarioCadastrar);

        Task<Usuario> BuscarUsuarioPeloEmailSenha(String email, String senha);

        Task AlterarStatusUsuario(int idUsuario, Boolean novoStatus);

        Task AlterarNivelAcessoUsuario(int idUsuarioAlterarNivelAcesso, NivelAcessoUsuario novoNivelAcesso);

        Task EditarUsuario(Usuario usuarioEditar);

        Task DeletarUsuario(Usuario usuarioDeletar);

        Task<Usuario> BuscarUsuarioPeloNome(String nome);

        Task<Usuario> BuscarUsuarioPeloEmail(String email);

    }
}
