using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpOnlineAFVApi.DTOs;
using UpOnlineAFVApi.Servico;

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

    }
}
