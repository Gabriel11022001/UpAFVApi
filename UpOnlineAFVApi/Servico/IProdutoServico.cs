using UpOnlineAFVApi.DTOs;
using UpOnlineAFVApi.Filtros;

namespace UpOnlineAFVApi.Servico
{
    public interface IProdutoServico
    {

        Task<Resposta<ProdutoDTO>> CadastrarProduto(String token, ProdutoDTO produtoDTOCadastrar);

        Task<Resposta<ProdutoDTO>> EditarProduto(String token, ProdutoDTO produtoDTOEditar);

        Task<Resposta<List<ProdutoDTO>>> BuscarProdutos(String token, int paginaAtual, int elementosPorPagina);

        Task<Resposta<ProdutoDTO>> BuscarProdutoPeloId(String token, int idProduto);

        Task<Resposta<Boolean>> DeletarProdutoProduto(String token, int idProdutoDeletar);

        Task<Resposta<List<ProdutoDTO>>> FiltrarProdutos(FiltroProdutos filtroProdutos);

        Task<Resposta<ProdutoDTO>> AlterarStatusProdutos(int idProdutoAlterarStatus, Boolean novoStatus);

    }
}
