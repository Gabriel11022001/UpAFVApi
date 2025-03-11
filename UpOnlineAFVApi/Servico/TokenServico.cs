using UpOnlineAFVApi.DTOs;
using UpOnlineAFVApi.Models;
using UpOnlineAFVApi.Repositorio;
using UpOnlineAFVApi.Utils;

namespace UpOnlineAFVApi.Servico
{
    public class TokenServico : ITokenServico
    {

        private readonly ITokenRepositorio _tokenRepositorio;
        private readonly int _tempoExpirarTokenEmMinutos = 30;

        public TokenServico(ITokenRepositorio tokenRepositorio)
        {
            _tokenRepositorio = tokenRepositorio;
        }

        public async Task<TokenDTO> ValidarTokenUsuario(string token)
        {
            
            try
            {
                TokenUsuario tokenUsuario = await _tokenRepositorio.BuscarToken(token);

                if (tokenUsuario is null)
                {

                    throw new TokenInvalidoException("Token não encontrado!");
                }

                if (ValidarTokenExpirou(tokenUsuario.DataRegistro))
                {

                    throw new TokenInvalidoException("Token expirado!");
                }

                return new TokenDTO()
                {
                    Token = token,
                    DataRegistro = tokenUsuario.DataRegistro,
                    UsuarioDTO = new UsuarioDTO()
                    {
                        UsuarioId = tokenUsuario.UsuarioId,
                        Nome = tokenUsuario.Usuario.Nome,
                        Email = tokenUsuario.Usuario.Email,
                        Telefone = tokenUsuario.Usuario.Telefone,
                        DataCadastro = tokenUsuario.Usuario.DataCadastro,
                        NivelAcessoId = tokenUsuario.Usuario.NivelAcessoUsuarioId,
                        Status = tokenUsuario.Usuario.Ativo,
                        Token = token
                    },
                    DataExpiracao = ObterDataExpiracaoToken(tokenUsuario.DataRegistro)
                };
            }
            catch (TokenInvalidoException e)
            {

                throw e;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        private Boolean ValidarTokenExpirou(DateTime dataCadastroToken)
        {
            DateTime dataExpiracaoToken = ObterDataExpiracaoToken(dataCadastroToken);
            DateTime dataAtual = new DateTime();

            if (dataAtual > dataExpiracaoToken)
            {

                return true;
            }

            return false;
        }

        private DateTime ObterDataExpiracaoToken(DateTime dataCadastroToken)
        {

            return dataCadastroToken.AddMinutes(Double.Parse(_tempoExpirarTokenEmMinutos.ToString()));
        }

    }
}
