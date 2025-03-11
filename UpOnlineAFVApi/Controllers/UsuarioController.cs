using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpOnlineAFVApi.DTOs;
using UpOnlineAFVApi.Servico;

namespace UpOnlineAFVApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioServico _usuarioServico;

        public UsuarioController(IUsuarioServico usuarioServico)
        {
            _usuarioServico = usuarioServico;
        }

        // cadastrar usuário
        [ HttpPost ]  
        public async Task<IActionResult> CadastrarUsuario([ FromHeader ] String token, UsuarioDTO usuarioDTO)
        {
            Resposta<UsuarioDTO> respostaCadastrarUsuario = await _usuarioServico.CadastrarUsuario(token, usuarioDTO);

            return respostaCadastrarUsuario.Ok ? Ok(respostaCadastrarUsuario) : BadRequest(respostaCadastrarUsuario);
        }

    }
}
