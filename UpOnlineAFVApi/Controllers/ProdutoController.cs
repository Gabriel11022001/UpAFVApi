using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpOnlineAFVApi.DTOs;
using UpOnlineAFVApi.Filtros;
using UpOnlineAFVApi.Servico;

namespace UpOnlineAFVApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {

        private readonly IProdutoServico _produtoServico;

        public ProdutoController(IProdutoServico produtoServico)
        {
            _produtoServico = produtoServico;
        }

        // cadastrar produto
        [ HttpPost ]
        public async Task<IActionResult> CadastrarProduto([ FromQuery ] String token, ProdutoDTO produtoDTOCadastrar)
        {
            Resposta<ProdutoDTO> respostaCadastrarProduto = await _produtoServico.CadastrarProduto(token, produtoDTOCadastrar);
            
            if (respostaCadastrarProduto.Ok)
            {

                return StatusCode(201, respostaCadastrarProduto);
            }

            return BadRequest(respostaCadastrarProduto);
        }

        // buscar produto pelo id
        [ HttpGet("{idProduto:int}") ]
        public async Task<IActionResult> BuscarProdutoPeloId([ FromQuery ] String token, int idProduto)
        {
            Resposta<ProdutoDTO> respostaBuscarProdutoPeloId = await _produtoServico.BuscarProdutoPeloId(token, idProduto);

            return respostaBuscarProdutoPeloId.Ok ? Ok(respostaBuscarProdutoPeloId) : BadRequest(respostaBuscarProdutoPeloId);
        }

        // deletar produto
        [ HttpDelete ]
        public async Task<IActionResult> DeletarProduto([ FromQuery ] String token, int idProdutoDeletar)
        {
            Resposta<Boolean> respostaDeletarProduto = await _produtoServico.DeletarProdutoProduto(token, idProdutoDeletar);

            if (respostaDeletarProduto.Ok)
            {

                return Ok(respostaDeletarProduto);
            }

            return BadRequest(respostaDeletarProduto);
        }

        // buscar produtos
        [ HttpGet ]
        public async Task<IActionResult> BuscarProdutos([ FromQuery ] String token, int paginaAtual, int elementosPorPagina)
        {
            Resposta<List<ProdutoDTO>> respostaConsultarProdutos = await _produtoServico.BuscarProdutos(token, paginaAtual, elementosPorPagina);

            return respostaConsultarProdutos.Ok ? Ok(respostaConsultarProdutos) : BadRequest(respostaConsultarProdutos);
        }

        // filtrar produtos
        [ HttpGet("filtrar-produtos") ]
        public async Task<IActionResult> FiltrarProdutos([ FromQuery ] FiltroProdutos filtroProdutos)
        {
            Resposta<List<ProdutoDTO>> respostaFiltrarProdutos = await _produtoServico.FiltrarProdutos(filtroProdutos);

            if (respostaFiltrarProdutos.Ok)
            {

                return Ok(respostaFiltrarProdutos);
            }

            return BadRequest(respostaFiltrarProdutos);
        }

        // alterar status do produto
        [ HttpPut("alterar-status") ]
        public async Task<IActionResult> AlterarStatusProduto([ FromQuery ] int id, Boolean novoStatus)
        {
            Resposta<ProdutoDTO> respostaAlterarStatusProduto = await _produtoServico.AlterarStatusProdutos(id, novoStatus);

            return respostaAlterarStatusProduto.Ok ? Ok(respostaAlterarStatusProduto) : BadRequest(respostaAlterarStatusProduto);
        }

    }
}
