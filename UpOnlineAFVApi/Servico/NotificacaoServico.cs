using UpOnlineAFVApi.DTOs;
using UpOnlineAFVApi.Models;
using UpOnlineAFVApi.Repositorio;

namespace UpOnlineAFVApi.Servico
{
    public class NotificacaoServico : INotificacaoServico
    {

        private readonly INotificacaoRepositorio _notificacaoRepositorio;

        public NotificacaoServico(INotificacaoRepositorio notificacaoRepositorio)
        {
            _notificacaoRepositorio = notificacaoRepositorio;
        }

        public Task<Resposta<NotificacaoDTO>> AlterarStatusNotificacao(int idNotificao, bool novoStatus)
        {
            throw new NotImplementedException();
        }

        public Task<Resposta<NotificacaoDTO>> BuscarNotificacaoPeloId(int idNotificacao)
        {
            throw new NotImplementedException();
        }

        // consultar as notificações
        public async Task<Resposta<List<NotificacaoDTO>>> BuscarNotificacoes(int paginaAtual, int elementosPorPagina)
        {

            try
            {
                List<Notificacao> notificacoes = await _notificacaoRepositorio.BuscarNotificacoes(paginaAtual, elementosPorPagina);

                if (notificacoes.Count == 0)
                {

                    return new Resposta<List<NotificacaoDTO>>("Não existem notificações cadastradas na base de dados!", false, new List<NotificacaoDTO>());
                }

                List<NotificacaoDTO> notificacoesDTO = new List<NotificacaoDTO>();

                notificacoes.ForEach(notificacao =>
                {
                    NotificacaoDTO notificacaoDTO = new NotificacaoDTO();
                    notificacaoDTO.NotificacaoId = notificacao.NotificacaoId;
                    notificacaoDTO.Titulo = notificacao.Titulo;
                    notificacaoDTO.Mensagem = notificacao.Mensagem;
                    notificacaoDTO.Status = notificacao.Status;

                    notificacoesDTO.Add(notificacaoDTO);
                });

                return new Resposta<List<NotificacaoDTO>>("Notificações encontradas com sucesso!", true, notificacoesDTO);
            }
            catch (Exception e)
            {

                return new Resposta<List<NotificacaoDTO>>("Erro ao tentar-se consultar as notificações!", false, null);
            }

        }

        // cadastrar notificação na base de dados
        public async Task<Resposta<NotificacaoDTO>> CadastrarNotificacao(NotificacaoDTO notificacaoDTO)
        {

            try
            {
                Notificacao notificacao = new Notificacao();
                notificacao.Titulo = notificacaoDTO.Titulo.ToUpper();
                notificacao.Mensagem = notificacaoDTO.Mensagem;
                notificacao.Status = false;

                await _notificacaoRepositorio.CadastrarNotificacao(notificacao);

                notificacaoDTO.NotificacaoId = notificacao.NotificacaoId;
                notificacaoDTO.Status = false;

                return new Resposta<NotificacaoDTO>("Notificação cadastrada com sucesso!", true, notificacaoDTO);
            }
            catch (Exception e)
            {

                return new Resposta<NotificacaoDTO>("Erro ao tentar-se cadastrar a notificação!", false, null);
            }

        }

        public Task<Resposta<NotificacaoDTO>> EditarNotificacao(NotificacaoDTO notificacaoDTO)
        {
            throw new NotImplementedException();
        }

    }
}
