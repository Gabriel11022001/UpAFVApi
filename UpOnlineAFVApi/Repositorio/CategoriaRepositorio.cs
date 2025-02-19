using Microsoft.EntityFrameworkCore;
using UpOnlineAFVApi.Contexto;
using UpOnlineAFVApi.Models;

namespace UpOnlineAFVApi.Repositorio
{
    public class CategoriaRepositorio : RepositorioBase, ICategoriaRepositorio
    {

        public CategoriaRepositorio(ApiDbContexto contexto): base(contexto) { }

        // buscar categoria pelo id
        public async Task<Categoria> BuscarCategoriaPeloId(int idCategoria)
        {

            return await Contexto.Categorias.FindAsync(idCategoria);
        }

        // buscar as categorias de forma paginada
        public async Task<List<Categoria>> BuscarCategorias(int paginaAtual, int totalCategoriasPorPagina)
        {

            return await Contexto
                .Categorias
                .OrderBy(c => c.Nome)
                .Skip((paginaAtual - 1) * totalCategoriasPorPagina)
                .Take(totalCategoriasPorPagina)
                .ToListAsync();
        }

        // cadastrar categoria
        public async Task CadastrarCategoria(Categoria categoriaCadastrar)
        {
            await Contexto.Categorias.AddAsync(categoriaCadastrar);
            await Contexto.SaveChangesAsync();
        }

        // deletar categoria
        public async Task DeletarCategoria(Categoria categoriaDeletar)
        {
            Contexto.Categorias.Entry(categoriaDeletar).State = EntityState.Deleted;
            await Contexto.SaveChangesAsync();
        }

        // editar categoria
        public async Task EditarCategoria(Categoria categoriaEditar)
        {
            Contexto.Categorias.Entry(categoriaEditar).State = EntityState.Modified;
            await Contexto.SaveChangesAsync();
        }

        public async Task<List<Categoria>> FiltrarCategoriaPeloNome(string nomeCategoriaFiltrar)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Categoria>> FiltrarCategoriaPeloStatus(bool ativo)
        {
            throw new NotImplementedException();
        }

        // buscar categoria pelo nome
        public async Task<Categoria> BuscarCategoriaPeloNome(String nomeCategoria)
        {

            return await Contexto.Categorias.FirstOrDefaultAsync(c => c.Nome.Equals(nomeCategoria.Trim()));
        }

    }
}
