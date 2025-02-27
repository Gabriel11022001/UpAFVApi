using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpOnlineAFVApi.DTOs;
using UpOnlineAFVApi.Servico;

namespace UpOnlineAFVApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ILoginServico _loginServico;

        public LoginController(ILoginServico loginServico)
        {
            _loginServico = loginServico;
        }

        // realizar login
        [ HttpPost ]
        public async Task<IActionResult> Login(String email, String senha)
        {
            Resposta<UsuarioDTO> respostaLogin = await _loginServico.Login(email, senha);

            if (respostaLogin.Ok)
            {

                return Ok(respostaLogin);
            }

            return BadRequest(respostaLogin);
        }

    }
}
