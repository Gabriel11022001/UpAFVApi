using UpOnlineAFVApi.DTOs;
using UpOnlineAFVApi.Models;
using UpOnlineAFVApi.Repositorio;
using UpOnlineAFVApi.Utils;

namespace UpOnlineAFVApi.Servico
{
    public class UsuarioServico : IUsuarioServico
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ITokenServico _tokenServico;
        private readonly INivelAcessoRepositorio _nivelAcessoRepositorio;
       
        public UsuarioServico(IUsuarioRepositorio usuarioRepositorio, ITokenServico tokenServico, INivelAcessoRepositorio nivelAcessoRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _tokenServico = tokenServico;
            _nivelAcessoRepositorio = nivelAcessoRepositorio;
        }

        public async Task<Resposta<List<UsuarioDTO>>> BuscarUsuarios(String token, int paginaAtual, int elementosPorPagina)
        {
            throw new NotImplementedException();
        }

        // cadastrar usuário
        public async Task<Resposta<UsuarioDTO>> CadastrarUsuario(String token, UsuarioDTO usuarioDTOCadastrar)
        {

            try
            {
                // validar o token do usuário
                TokenDTO tokenDTO = await _tokenServico.ValidarTokenUsuario(token);

                // validar e-mail
                if (!ValidaEmail.ValidarEmail(usuarioDTOCadastrar.Email))
                {

                    return new Resposta<UsuarioDTO>("E-mail inválido", false, null);
                }

                // validar se já existe outro usuário cadastrado com o mesmo nome
                Usuario usuarioMesmoNome = await _usuarioRepositorio.BuscarUsuarioPeloNome(usuarioDTOCadastrar.Nome);

                if (usuarioMesmoNome is not null)
                {

                    return new Resposta<UsuarioDTO>("Já existe outro usuário cadastrado com o mesmo nome!", false, null);
                }

                // validar se já existe outro usuário cadastrado com o mesmo e-mail
                Usuario usuarioMesmoEmail = await _usuarioRepositorio.BuscarUsuarioPeloEmail(usuarioDTOCadastrar.Email);

                if (usuarioMesmoEmail is not null)
                {

                    return new Resposta<UsuarioDTO>("Já existe outro usuário cadastrado com o mesmo e-mail!", false, null);
                }

                // validar se existe o nivel de acesso selecionado para o usuário
                NivelAcessoUsuario nivelAcessoUsuario = await _nivelAcessoRepositorio.BuscarNivelAcessoPeloId(usuarioDTOCadastrar.NivelAcessoId);

                if (nivelAcessoUsuario is null)
                {

                    return new Resposta<UsuarioDTO>("Nivel de acesso não encontrado!", false, null);
                }

                Usuario usuario = new Usuario();
                usuario.Nome = usuarioDTOCadastrar.Nome;
                usuario.Email = usuarioDTOCadastrar.Email;
                usuario.Telefone = usuarioDTOCadastrar.Telefone;
                usuario.Ativo = usuarioDTOCadastrar.Status;
                usuario.DataCadastro = new DateTime();
                usuario.NivelAcessoUsuarioId = usuarioDTOCadastrar.NivelAcessoId;
                usuario.NivelAcessoUsuario = nivelAcessoUsuario;
                usuario.Senha = CriptografaSenha.CriptografarSenha(usuarioDTOCadastrar.Senha);

                await _usuarioRepositorio.CadastrarUsuario(usuario);

                usuarioDTOCadastrar.UsuarioId = usuario.UsuarioId;

                return new Resposta<UsuarioDTO>("Usuário cadastrado com sucesso!", true, usuarioDTOCadastrar);
            }
            catch (TokenInvalidoException e)
            {

                return new Resposta<UsuarioDTO>(e.Message, false, null);
            }
            catch (Exception e)
            {

                return new Resposta<UsuarioDTO>("Erro ao tentar-se cadastrar o usuário!", false, null);
            }

        }

    }
}
