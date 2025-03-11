using Microsoft.EntityFrameworkCore;
using UpOnlineAFVApi.Contexto;
using UpOnlineAFVApi.Models;

namespace UpOnlineAFVApi.Repositorio
{
    public class UsuarioRepositorio: RepositorioBase, IUsuarioRepositorio
    {

        public UsuarioRepositorio(ApiDbContexto contexto): base(contexto) { }

        // alterar o nivel de acesso do usuário
        public async Task AlterarNivelAcessoUsuario(int idUsuarioAlterarNivelAcesso, NivelAcessoUsuario novoNivelAcesso)
        {
            Usuario usuario = await Contexto.Usuarios.FindAsync(idUsuarioAlterarNivelAcesso);

            if (usuario is not null)
            {
                usuario.NivelAcessoUsuario = novoNivelAcesso;
                usuario.NivelAcessoUsuarioId = novoNivelAcesso.NivelAcessoUsuarioId;

                Contexto.Usuarios.Entry(usuario).State = EntityState.Modified;
                await Contexto.SaveChangesAsync();
            }

        }

        // alterar status do usuário
        public async Task AlterarStatusUsuario(int idUsuario, bool novoStatus)
        {
            Usuario usuario = await Contexto.Usuarios.FindAsync(idUsuario);

            if (usuario is not null)
            {
                usuario.Ativo = novoStatus;
                Contexto.Usuarios.Entry(usuario).State = EntityState.Modified;
                await Contexto.SaveChangesAsync();
            }

        }

        // buscar usuário pelo e-mail
        public async Task<Usuario> BuscarUsuarioPeloEmail(string email)
        {

            return await Contexto.Usuarios.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        // buscar usuário pelo e-mail e senha
        public async Task<Usuario> BuscarUsuarioPeloEmailSenha(string email, string senha)
        {

            return await Contexto.Usuarios
                .Include(u => u.NivelAcessoUsuario)
                .FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Senha.Equals(senha));
        }

        // buscar usuário pelo nome
        public async Task<Usuario> BuscarUsuarioPeloNome(string nome)
        {

            return await Contexto.Usuarios.FirstOrDefaultAsync(u => u.Nome.Equals(nome));
        }

        // cadastrar usuário
        public async Task CadastrarUsuario(Usuario usuarioCadastrar)
        {
            await Contexto.Usuarios.AddAsync(usuarioCadastrar);
            await Contexto.SaveChangesAsync();
        }

        // deletar usuário
        public async Task DeletarUsuario(Usuario usuarioDeletar)
        {
            Contexto.Usuarios.Entry(usuarioDeletar).State = EntityState.Deleted;
            await Contexto.SaveChangesAsync();
        }

        // editar usuário
        public async Task EditarUsuario(Usuario usuarioEditar)
        {
            Contexto.Usuarios.Entry(usuarioEditar).State = EntityState.Modified;
            await Contexto.SaveChangesAsync();
        }

    }
}
