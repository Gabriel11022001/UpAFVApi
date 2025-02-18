using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpOnlineAFVApi.DTOs;
using UpOnlineAFVApi.Servico;

namespace UpOnlineAFVApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {

        private readonly ICategoriaServico _categoriaServico;

        public CategoriaController(ICategoriaServico categoriaServico)
        {
            _categoriaServico = categoriaServico;
        }

        // buscar categoria pelo id
        [ HttpGet("{idCategoria:int}") ]
        public async Task<IActionResult> BuscarCategoriaPeloId(int idCategoria)
        {
            Resposta<CategoriaDTO> resposta = await _categoriaServico.BuscarCategoriaPeloId(idCategoria);

            if (resposta.Ok)
            {

                return Ok(resposta);
            }

            return BadRequest(resposta);
        }

        // cadastrar categoria
        [ HttpPost ]  
        public async Task<IActionResult> CadastrarCategoria(CategoriaDTO categoriaDTO)
        {
            Resposta<CategoriaDTO> resposta = await _categoriaServico.SalvarCategoria(categoriaDTO);

            if (resposta.Ok)
            {

                return StatusCode(201, resposta);
            }

            return BadRequest(resposta);
        }

        [ HttpPut ]
        // editar categoria
        public async Task<IActionResult> EditarCategoria(CategoriaDTO categoriaDTO)
        {
            Resposta<CategoriaDTO> resposta = await _categoriaServico.SalvarCategoria(categoriaDTO);

            return resposta.Ok ? Ok(resposta) : BadRequest(resposta);
        }

    }
}
