using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpOnlineAFVApi.DTOs;
using UpOnlineAFVApi.Servico;

namespace UpOnlineAFVApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissaoController : ControllerBase
    {

        private readonly IPermissaoServico _permissaoServico;

        public PermissaoController(IPermissaoServico permissaoServico)
        {
            _permissaoServico = permissaoServico;
        }

        // cadastrar permissão
        [ HttpPost ]
        public async Task<IActionResult> CadastrarPermissao(PermissaoNivelAcessoUsuarioDTO permissaoNivelAcessoUsuarioDTO)
        {
            Resposta<PermissaoNivelAcessoUsuarioDTO> respostaCadastrarPermissao = await _permissaoServico.CadastrarPermissao(permissaoNivelAcessoUsuarioDTO);

            return respostaCadastrarPermissao.Ok ? StatusCode(201, respostaCadastrarPermissao) : BadRequest(respostaCadastrarPermissao);
        }

    }
}
