using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpOnlineAFVApi.DTOs;
using UpOnlineAFVApi.Servico;
using UpOnlineAFVApi.Utils;

namespace UpOnlineAFVApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {

        private readonly IClienteServico _clienteServico;

        public ClienteController(IClienteServico clienteServico)
        {
            _clienteServico = clienteServico;
        }

        // cadastrar cliente
        [ HttpPost ]
        public async Task<IActionResult> CadastrarCliente(ClienteDTO clienteDTO)
        {
            Resposta<ClienteDTO> respostaCadastrarCliente = await _clienteServico.SalvarCliente(clienteDTO);

            if (respostaCadastrarCliente.Ok)
            {

                return StatusCode(201, respostaCadastrarCliente);
            }

            return BadRequest(respostaCadastrarCliente);
        }

        // consultar clientes
        [ HttpGet ]
        public async Task<IActionResult> BuscarClientes([ FromQuery ] String token, int paginaAtual, int elementosPorPagina)
        {
            Resposta<RetornoListagem<List<ClienteDTO>>> respostaConsultarClientes = await _clienteServico.BuscarClientes(
                token,
                paginaAtual,
                elementosPorPagina
            );

            return respostaConsultarClientes.Ok ? Ok(respostaConsultarClientes) : BadRequest(respostaConsultarClientes);
        }

        // buscar cliente pelo id
        [ HttpGet("buscar-cliente-pelo-id") ]
        public async Task<IActionResult> BuscarClientePeloId([ FromQuery ] String token, int idCliente)
        {
            Resposta<ClienteDTO> respostaBuscarClientePeloId = await _clienteServico.BuscarClientePeloId(token, idCliente);

            return respostaBuscarClientePeloId.Ok ? Ok(respostaBuscarClientePeloId) : BadRequest(respostaBuscarClientePeloId);
        }

        // alterar o status do cliente
        [ HttpPut("alterar-status-cliente") ]
        public async Task<IActionResult> AlterarStatusCliente([ FromQuery ] String token, int idCliente, Boolean novoStatus)
        {
            Resposta<ClienteDTO> respostaAlterarStatus = await _clienteServico.AlterarStatusCliente(token, idCliente, novoStatus);

            if (respostaAlterarStatus.Ok)
            {

                return Ok(respostaAlterarStatus);
            }

            return BadRequest(respostaAlterarStatus);
        }

    }
}
