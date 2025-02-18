using UpOnlineAFVApi.Models;

namespace UpOnlineAFVApi.Repositorio
{
    public interface ICategoriaRepositorio
    {

        Task CadastrarCategoria(Categoria categoriaCadastrar);

        Task EditarCategoria(Categoria categoriaEditar);

        Task<List<Categoria>> BuscarCategorias(int paginaAtual, int totalCategoriasPorPagina);

        Task DeletarCategoria(Categoria categoriaDeletar);

        Task<Categoria> BuscarCategoriaPeloId(int idCategoria);

        Task<List<Categoria>> FiltrarCategoriaPeloNome(string nomeCategoriaFiltrar);

        Task<List<Categoria>> FiltrarCategoriaPeloStatus(bool ativo);

        Task<Categoria> BuscarCategoriaPeloNome(String nomeCategoria);

    }
}
