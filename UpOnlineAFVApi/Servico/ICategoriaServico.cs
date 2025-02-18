using UpOnlineAFVApi.DTOs;

namespace UpOnlineAFVApi.Servico
{
    public interface ICategoriaServico
    {

        Task<Resposta<List<CategoriaDTO>>> BuscarCategorias(int paginaAtual, int totalElementosPorPagina);

        Task<Resposta<CategoriaDTO>> SalvarCategoria(CategoriaDTO categoriaDTOSalvar);

        Task<Resposta<CategoriaDTO>> BuscarCategoriaPeloId(int idCategoriaFiltrar);

        Task<Resposta<List<CategoriaDTO>>> FiltrarCategoriasPeloNome(String nomeCategoriaFiltrar);

        Task<Resposta<List<CategoriaDTO>>> FiltrarCategoriaPeloStatus(Boolean status);

        Task<Resposta<Boolean>> DeletarCategoria(int idCategoriaDeletar);

    }
}
