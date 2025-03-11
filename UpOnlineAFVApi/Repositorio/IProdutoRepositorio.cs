using UpOnlineAFVApi.Filtros;
using UpOnlineAFVApi.Models;

namespace UpOnlineAFVApi.Repositorio
{
    public interface IProdutoRepositorio
    {

        Task CadastrarProduto(Produto produto);

        Task EditarProduto(Produto produto);

        Task<List<Produto>> BuscarProdutos(int paginaAtual, int elementosPorPagina);

        Task<Produto> BuscarProdutoPeloId(int produtoId);

        Task DeletarProduto(Produto produtoDeletar);

        Task<Produto> BuscarProdutoPeloNome(String nome);

        Task<List<Produto>> FiltrarProdutos(FiltroProdutos filtroProdutos);

        Task AlterarStatusProduto(int idProdutoAlterarStatus, Boolean novoStatus);

    }
}
