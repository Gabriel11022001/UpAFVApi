using UpOnlineAFVApi.Models;

namespace UpOnlineAFVApi.Repositorio
{
    public interface INotificacaoRepositorio
    {

        Task<List<Notificacao>> BuscarNotificacoes(int paginaAtual, int elementosPorPagina);

        Task CadastrarNotificacao(Notificacao notificacaoCadastrar);

        Task EditarNotificacao(Notificacao notificacaoEditar);

        Task<Notificacao> BuscarNotificacaoPeloId(int idNotificacao);

        Task DeletarNotificacao(Notificacao notificacaoDeletar);

        Task AlterarStatusNotificacao(int idNotificacao, bool novoStatus);

    }
}
