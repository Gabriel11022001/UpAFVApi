using UpOnlineAFVApi.DTOs;
using UpOnlineAFVApi.Models;
using UpOnlineAFVApi.Repositorio;

namespace UpOnlineAFVApi.Servico
{
    public class PermissaoServico : IPermissaoServico
    {

        private readonly IPermissaoRepositorio _permissaoRepositorio;

        public PermissaoServico(IPermissaoRepositorio permissaoRepositorio)
        {
            _permissaoRepositorio = permissaoRepositorio;
        }

        // cadastrar permissão
        public async Task<Resposta<PermissaoNivelAcessoUsuarioDTO>> CadastrarPermissao(PermissaoNivelAcessoUsuarioDTO permissaoNivelAcessoUsuarioDTO)
        {

            try
            {
                PermissaoNivelAcessoUsuario permissaoNivelAcessoUsuario = new PermissaoNivelAcessoUsuario();
                permissaoNivelAcessoUsuario.Nome = permissaoNivelAcessoUsuarioDTO.Nome;
                permissaoNivelAcessoUsuario.Ativo = permissaoNivelAcessoUsuarioDTO.Ativo;

                await _permissaoRepositorio.CadastrarPermissao(permissaoNivelAcessoUsuario);

                permissaoNivelAcessoUsuarioDTO.PermissaoNivelAcessoUsuarioId = permissaoNivelAcessoUsuario.PermissaoNivelAcessoUsuarioId;

                return new Resposta<PermissaoNivelAcessoUsuarioDTO>("Permissão cadastrada com sucesso!", true, permissaoNivelAcessoUsuarioDTO);
            }
            catch (Exception e)
            {

                return new Resposta<PermissaoNivelAcessoUsuarioDTO>("Erro ao tentar-se cadastrar a permissão!", false, null);
            }

        }

    }
}
