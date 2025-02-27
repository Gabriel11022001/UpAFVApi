using Microsoft.EntityFrameworkCore;
using UpOnlineAFVApi.Contexto;
using UpOnlineAFVApi.Models;

namespace UpOnlineAFVApi.Repositorio
{
    public class TokenRepositorio: RepositorioBase, ITokenRepositorio
    {

        public TokenRepositorio(ApiDbContexto contexto): base(contexto) { }

        // buscar token
        public async Task<TokenUsuario> BuscarToken(string token)
        {

            return await Contexto.TokensUsuarios
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(t => t.Token.Equals(token));
        }

        // cadastrar token
        public async Task CadastrarToken(TokenUsuario tokenUsuarioCadastrar)
        {
            await Contexto.TokensUsuarios.AddAsync(tokenUsuarioCadastrar);
            await Contexto.SaveChangesAsync();
        }

        // deletar o token
        public async Task DeletarToken(int idTokenDeletar)
        {
            TokenUsuario token = await Contexto.TokensUsuarios.FindAsync(idTokenDeletar);

            if (token is not null)
            {
                Contexto.TokensUsuarios.Entry(token).State = EntityState.Deleted;
                await Contexto.SaveChangesAsync();
            }

        }

    }
}
