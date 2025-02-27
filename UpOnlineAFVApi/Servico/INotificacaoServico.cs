using UpOnlineAFVApi.DTOs;

namespace UpOnlineAFVApi.Servico
{
    public interface INotificacaoServico
    {

        Task<Resposta<NotificacaoDTO>> CadastrarNotificacao(NotificacaoDTO notificacaoDTO);

        Task<Resposta<List<NotificacaoDTO>>> BuscarNotificacoes(int paginaAtual, int elementosPorPagina);

        Task<Resposta<NotificacaoDTO>> EditarNotificacao(NotificacaoDTO notificacaoDTO);

        Task<Resposta<NotificacaoDTO>> BuscarNotificacaoPeloId(int idNotificacao);

        Task<Resposta<NotificacaoDTO>> AlterarStatusNotificacao(int idNotificao, bool novoStatus);

    }
}
