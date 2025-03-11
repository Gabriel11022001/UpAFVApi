using UpOnlineAFVApi.Contexto;
using UpOnlineAFVApi.Models;

namespace UpOnlineAFVApi.Repositorio
{
    public class PermissaoRepositorio: RepositorioBase, IPermissaoRepositorio
    {

        public PermissaoRepositorio(ApiDbContexto contexto): base(contexto)
        {

        }

        // buscar permissão pelo id
        public async Task<PermissaoNivelAcessoUsuario> BuscarPermissaoPeloId(int idPermissao)
        {

            return await Contexto.PermissoesNiveisAcessosUsuarios.FindAsync(idPermissao);
        }

        // cadastrar permissão
        public async Task CadastrarPermissao(PermissaoNivelAcessoUsuario permissaoNivelAcessoUsuarioCadastrar)
        {
            await Contexto.PermissoesNiveisAcessosUsuarios.AddAsync(permissaoNivelAcessoUsuarioCadastrar);
            await Contexto.SaveChangesAsync();
        }

    }
}
