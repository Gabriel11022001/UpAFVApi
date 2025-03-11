using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpOnlineAFVApi.DTOs;
using UpOnlineAFVApi.Servico;

namespace UpOnlineAFVApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NivelAcessoController : ControllerBase
    {

        private readonly INivelAcessoServico _nivelAcessoServico;

        public NivelAcessoController(INivelAcessoServico nivelAcessoServico)
        {
            _nivelAcessoServico = nivelAcessoServico;
        }

        // cadastrar nivel de acesso
        [ HttpPost ]  
        public async Task<IActionResult> CadastrarNivelAcesso(NivelAcessoUsuarioDTO nivelAcessoUsuarioDTO)
        {
            Resposta<NivelAcessoUsuarioDTO> respostaCadastrarNivelAcesso = await _nivelAcessoServico.CadastrarNivelAcesso(nivelAcessoUsuarioDTO);

            return respostaCadastrarNivelAcesso.Ok ? StatusCode(201, respostaCadastrarNivelAcesso) : BadRequest(respostaCadastrarNivelAcesso);
        }

        // cadastrar multiplos niveis de acesso
        [ HttpPost("cadastrar-multiplos-niveis-acesso") ]
        public async Task<IActionResult> CadastrarMultiplosNiveisAcesso(List<NivelAcessoUsuarioDTO> niveisAcessoUsuariosDTO)
        {
            Resposta<List<NivelAcessoUsuarioDTO>> respostaCadastrarMultiplosNiveisAcesso = await _nivelAcessoServico.CadastrarMultiplosNiveisAcessos(niveisAcessoUsuariosDTO);

            return respostaCadastrarMultiplosNiveisAcesso.Ok ? StatusCode(201, respostaCadastrarMultiplosNiveisAcesso) : BadRequest(respostaCadastrarMultiplosNiveisAcesso);
        }

    }
}
