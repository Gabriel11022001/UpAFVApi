using UpOnlineAFVApi.Contexto;
using UpOnlineAFVApi.Models;
using Microsoft.EntityFrameworkCore;

namespace UpOnlineAFVApi.Repositorio
{
    public class NotificacaoRepositorio : RepositorioBase, INotificacaoRepositorio
    {

        public NotificacaoRepositorio(ApiDbContexto contexto): base(contexto) {}

        // alterar o status da notificação
        public async Task AlterarStatusNotificacao(int idNotificacao, bool novoStatus)
        {
            Notificacao notificacao = await Contexto.Notificacoes.FindAsync(idNotificacao);

            if (notificacao is not null)
            {
                notificacao.Status = novoStatus;
                Contexto.Notificacoes.Entry(notificacao).State = EntityState.Modified;
                await Contexto.SaveChangesAsync();
            }

        }

        // buscar notificação pelo id
        public async Task<Notificacao> BuscarNotificacaoPeloId(int idNotificacao)
        {

            return await Contexto.Notificacoes.FindAsync(idNotificacao);
        }

        // buscar notificações
        public async Task<List<Notificacao>> BuscarNotificacoes(int paginaAtual, int elementosPorPagina)
        {

            return await Contexto.Notificacoes.ToListAsync();
        }

        // cadastrar notificação
        public async Task CadastrarNotificacao(Notificacao notificacaoCadastrar)
        {
            await Contexto.Notificacoes.AddAsync(notificacaoCadastrar);
            await Contexto.SaveChangesAsync();
        }

        // deletar notificação
        public async Task DeletarNotificacao(Notificacao notificacaoDeletar)
        {
            Contexto.Notificacoes.Entry(notificacaoDeletar).State = EntityState.Deleted;
            await Contexto.SaveChangesAsync();
        }

        // editar notificação
        public async Task EditarNotificacao(Notificacao notificacaoEditar)
        {
            Contexto.Notificacoes.Entry(notificacaoEditar).State = EntityState.Modified;
            await Contexto.SaveChangesAsync();
        }

    }
}
