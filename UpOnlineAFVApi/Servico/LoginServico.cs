using UpOnlineAFVApi.DTOs;
using UpOnlineAFVApi.Models;
using UpOnlineAFVApi.Repositorio;
using UpOnlineAFVApi.Utils;

namespace UpOnlineAFVApi.Servico
{
    public class LoginServico : ILoginServico
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ITokenRepositorio _tokenRepositorio;

        public LoginServico(IUsuarioRepositorio usuarioRepositorio, ITokenRepositorio tokenRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _tokenRepositorio = tokenRepositorio;
        }

        // realizar login
        public async Task<Resposta<UsuarioDTO>> Login(string email, string senha)
        {

            try
            {
                
                // validar o e-mail
                if (email.Trim() == "")
                {

                    return new Resposta<UsuarioDTO>("Informe o e-mail", false, null);
                }

                if (!ValidaEmail.ValidarEmail(email))
                {

                    return new Resposta<UsuarioDTO>()
                    {
                        Msg = "E-mail inválido!",
                        Conteudo = null,
                        Ok = false
                    };
                }

                // validar senha
                if (senha.Trim() == "")
                {

                    return new Resposta<UsuarioDTO>("Informe a senha!", false, null);
                }

                if (senha.Trim().Length != 6)
                {

                    return new Resposta<UsuarioDTO>("Senha inválida!", false, null);
                }

                Usuario usuario = await _usuarioRepositorio.BuscarUsuarioPeloEmailSenha(email, CriptografaSenha.CriptografarSenha(senha.Trim()));

                if (usuario is null)
                {

                    return new Resposta<UsuarioDTO>("E-mail ou senha incorretos!", false, null);
                }

                if (!usuario.Ativo)
                {

                    return new Resposta<UsuarioDTO>("Perfil inativo!", false, null);
                }

                // gerar o token do usuário
                TokenUsuario tokenUsuario = new TokenUsuario()
                {
                    DataRegistro = new DateTime(),
                    Token = GeraToken.GerarToken(usuario.Nome),
                    Usuario = usuario,
                    UsuarioId = usuario.UsuarioId
                };

                await _tokenRepositorio.CadastrarToken(tokenUsuario);

                UsuarioDTO usuarioDTO = new UsuarioDTO();
                usuarioDTO.UsuarioId = usuario.UsuarioId;
                usuarioDTO.Nome = usuario.Nome;
                usuarioDTO.Telefone = usuario.Telefone;
                usuarioDTO.Email = usuario.Email;
                usuarioDTO.DataCadastro = usuario.DataCadastro;
                usuarioDTO.Status = usuario.Ativo;
                usuarioDTO.NivelAcessoId = usuario.NivelAcessoUsuarioId;
                usuarioDTO.NivelAcessoUsuarioDTO = new NivelAcessoUsuarioDTO();
                usuarioDTO.Token = tokenUsuario.Token;
                usuarioDTO.Senha = usuario.Senha;

                return new Resposta<UsuarioDTO>("Login efetuado com sucesso!", true, usuarioDTO);
            }
            catch (Exception e)
            {

                return new Resposta<UsuarioDTO>()
                {
                    Msg = "Erro ao tentar-se realizar login!",
                    Ok = false,
                    Conteudo = null
                };
            }

        }

    }
}
