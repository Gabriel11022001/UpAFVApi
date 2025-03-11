using UpOnlineAFVApi.Contexto;
using UpOnlineAFVApi.Models;

namespace UpOnlineAFVApi.Repositorio
{
    public class NivelAcessoRepositorio: RepositorioBase, INivelAcessoRepositorio
    {

        public NivelAcessoRepositorio(ApiDbContexto contexto): base(contexto)
        {

        }

        // buscar nivel de acesso pelo id
        public async Task<NivelAcessoUsuario> BuscarNivelAcessoPeloId(int id)
        {

            return await Contexto.NiveisAcessoUsuarios.FindAsync(id);
        }

        // cadastrar nivel de acesso
        public async Task CadastrarNivelAcesso(NivelAcessoUsuario nivelAcessoUsuarioCadastrar)
        {
            await Contexto.NiveisAcessoUsuarios.AddAsync(nivelAcessoUsuarioCadastrar);
            await Contexto.SaveChangesAsync();
        }

        public async Task CommitarTransacao()
        {
            await Contexto.Database.CommitTransactionAsync();
        }

        public async Task IniciarTransacao()
        {
            await Contexto.Database.BeginTransactionAsync(); 
        }

        public async Task RollbackTransacao()
        {
            await Contexto.Database.RollbackTransactionAsync();
        }

    }
}
