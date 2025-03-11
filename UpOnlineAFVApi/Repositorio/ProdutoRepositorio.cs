using UpOnlineAFVApi.Contexto;
using UpOnlineAFVApi.Models;
using Microsoft.EntityFrameworkCore;
using UpOnlineAFVApi.Filtros;
using Microsoft.IdentityModel.Tokens;

namespace UpOnlineAFVApi.Repositorio
{
    public class ProdutoRepositorio: RepositorioBase, IProdutoRepositorio
    {

        public ProdutoRepositorio(ApiDbContexto contexto): base(contexto) { }

        // buscar produto pelo id
        public async Task<Produto> BuscarProdutoPeloId(int produtoId)
        {

            return await Contexto.Produtos.Include(p => p.Categoria).FirstOrDefaultAsync(p => p.ProdutoId == produtoId);
        }

        // buscar produto pelo nome
        public async Task<Produto> BuscarProdutoPeloNome(string nome)
        {

            return await Contexto.Produtos.FirstOrDefaultAsync(p => p.Nome.Equals(nome));
        }

        // buscar produtos
        public async Task<List<Produto>> BuscarProdutos(int paginaAtual, int elementosPorPagina)
        {

            return await Contexto.Produtos
                .Include(p => p.Categoria)
                .OrderBy(p => p.Nome)
                .Skip((paginaAtual - 1) * elementosPorPagina)
                .Take(elementosPorPagina)
                .ToListAsync();
        }

        // cadastrar produto
        public async Task CadastrarProduto(Produto produto)
        {
            await Contexto.Produtos.AddAsync(produto);
            await Contexto.SaveChangesAsync();
        }

        // deletar produto
        public async Task DeletarProduto(Produto produtoDeletar)
        {
            Contexto.Produtos.Entry(produtoDeletar).State = EntityState.Deleted;
            await Contexto.SaveChangesAsync();
        }

        // editar produto
        public async Task EditarProduto(Produto produto)
        {
            Contexto.Produtos.Entry(produto).State = EntityState.Modified;
            await Contexto.SaveChangesAsync();
        }

        // filtrar produtos
        public async Task<List<Produto>> FiltrarProdutos(FiltroProdutos filtroProdutos)
        {

            return await Contexto.Produtos
                .Where(p => filtroProdutos.NomeProduto.Trim() == "" ? p.Nome != "" : p.Nome.Contains(filtroProdutos.NomeProduto.Trim()))
                .Where(p => filtroProdutos.Descricao.IsNullOrEmpty() ? p.Descricao != "" || p.Descricao.Length > 0 : p.Descricao.Contains(filtroProdutos.Descricao))
                .Where(p => filtroProdutos.PrecoVendaInicial != 0 && filtroProdutos.PrecoVendaFinal != 0 ? p.PrecoVenda >= filtroProdutos.PrecoVendaInicial && p.PrecoVenda <= filtroProdutos.PrecoVendaFinal : p.PrecoVenda != null)
                .Where(p => p.Ativo == filtroProdutos.Status)
                .OrderBy(p => p.Nome)
                .Include(p => p.Categoria)
                .ToListAsync();
        }

        // alterar status do produtos
        public async Task AlterarStatusProduto(int idProdutoAlterarStatus, Boolean novoStatus)
        {
            Produto produtoAlterarStatus = await Contexto.Produtos.FindAsync(idProdutoAlterarStatus);

            if (produtoAlterarStatus is not null)
            {
                produtoAlterarStatus.Ativo = novoStatus;
                Contexto.Produtos.Entry(produtoAlterarStatus).State = EntityState.Modified;
                await Contexto.SaveChangesAsync();
            }

        }

    }
}
