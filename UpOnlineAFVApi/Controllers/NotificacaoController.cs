using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpOnlineAFVApi.DTOs;
using UpOnlineAFVApi.Servico;

namespace UpOnlineAFVApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacaoController : ControllerBase
    {

        private readonly INotificacaoServico _notificacaoServico;

        public NotificacaoController(INotificacaoServico notificacaoServico)
        {
            _notificacaoServico = notificacaoServico;
        }

        // cadastrar notificação
        [ HttpPost ]
        public async Task<IActionResult> CadastrarNotificacao(NotificacaoDTO notificacaoDTO)
        {
            Resposta<NotificacaoDTO> respostaCadastrarNotificacao = await _notificacaoServico.CadastrarNotificacao(notificacaoDTO);

            return respostaCadastrarNotificacao.Ok ? StatusCode(201, respostaCadastrarNotificacao) : BadRequest(respostaCadastrarNotificacao);
        }

        // buscar notificações
        [ HttpGet ]  
        public async Task<IActionResult> BuscarNotificacoes([ FromQuery ] int paginaAtual, int elementosPorPagina)
        {
            Resposta<List<NotificacaoDTO>> respostaConsultarNotificacoes = await _notificacaoServico.BuscarNotificacoes(paginaAtual, elementosPorPagina);

            return respostaConsultarNotificacoes.Ok ? Ok(respostaConsultarNotificacoes) : BadRequest(respostaConsultarNotificacoes);
        }

    }
}
