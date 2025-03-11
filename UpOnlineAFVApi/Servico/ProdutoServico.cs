using UpOnlineAFVApi.DTOs;
using UpOnlineAFVApi.Filtros;
using UpOnlineAFVApi.Models;
using UpOnlineAFVApi.Repositorio;
using UpOnlineAFVApi.Utils;

namespace UpOnlineAFVApi.Servico
{
    public class ProdutoServico : IProdutoServico
    {

        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly ICategoriaRepositorio _categoriaRepositorio;
        private readonly ITokenServico _tokenServico;
        private readonly int[] _tamanhosPaginasPermitidos = [5, 10, 15];

        public ProdutoServico(IProdutoRepositorio produtoRepositorio, ICategoriaRepositorio categoriaRepositorio, ITokenServico tokenServico)
        {
            _produtoRepositorio = produtoRepositorio;
            _categoriaRepositorio = categoriaRepositorio;
            _tokenServico = tokenServico;
        }

        // buscar produto pelo id
        public async Task<Resposta<ProdutoDTO>> BuscarProdutoPeloId(String token, int idProduto)
        {

            try
            {
                TokenDTO tokenDTO = await _tokenServico.ValidarTokenUsuario(token);

                Produto produto = await _produtoRepositorio.BuscarProdutoPeloId(idProduto);

                if (produto is null)
                {

                    return new Resposta<ProdutoDTO>()
                    {
                        Msg = "Não existe um produto cadastrado com o id informado!",
                        Conteudo = null,
                        Ok = false
                    };
                }

                ProdutoDTO produtoDTO = new ProdutoDTO();
                produtoDTO.ProdutoId = produto.ProdutoId;
                produtoDTO.UrlFotoProduto = produto.UrlFotoProduto;
                produtoDTO.Nome = produto.Nome;
                produtoDTO.PrecoCompra = produto.PrecoCompra;
                produtoDTO.PrecoVenda = produto.PrecoVenda;
                produtoDTO.DataCadastro = produto.DataCadastro;
                produtoDTO.Ativo = produto.Ativo;
                produtoDTO.CategoriaId = produto.CategoriaId;
                produtoDTO.Descricao = produto.Descricao;
                produtoDTO.Estoque = produto.Estoque;

                CategoriaDTO categoriaDTO = new CategoriaDTO();
                categoriaDTO.CategoriaId = produto.Categoria.CategoriaId;
                categoriaDTO.Nome = produto.Categoria.Nome;
                categoriaDTO.Status = produto.Categoria.Ativo;

                produtoDTO.CategoriaDTO = categoriaDTO;

                return new Resposta<ProdutoDTO>()
                {
                    Msg = "Produto encontrado com sucesso!",
                    Conteudo = produtoDTO,
                    Ok = true
                };
            }
            catch (TokenInvalidoException e)
            {

                return new Resposta<ProdutoDTO>()
                {
                    Msg = e.Message,
                    Conteudo = null,
                    Ok = false
                };
            }
            catch (Exception e)
            {

                return new Resposta<ProdutoDTO>()
                {
                    Msg = "Erro ao tentar-se buscar o produto pelo id!",
                    Conteudo = null,
                    Ok = false
                };
            }

        }

        private Boolean ValidarElementosPorPaginaConsulta(int elementosPorPagina)
        {

            return _tamanhosPaginasPermitidos.Contains(elementosPorPagina);
        }

        // buscar produtos
        public async Task<Resposta<List<ProdutoDTO>>> BuscarProdutos(String token, int paginaAtual, int elementosPorPagina)
        {

            try
            {
                TokenDTO tokenDTO = await _tokenServico.ValidarTokenUsuario(token);

                if (paginaAtual <= 0)
                {
                    paginaAtual = 1;
                }

                // validar elementos por página
                if (!ValidarElementosPorPaginaConsulta(elementosPorPagina))
                {
                    elementosPorPagina = _tamanhosPaginasPermitidos[ 0 ];
                }

                List<Produto> produtos = await _produtoRepositorio.BuscarProdutos(paginaAtual, elementosPorPagina);

                if (produtos.Count == 0)
                {

                    return new Resposta<List<ProdutoDTO>>("Não existem produtos cadastrados!", true, new List<ProdutoDTO>());
                }

                List<ProdutoDTO> produtosDTO = new List<ProdutoDTO>();

                produtos.ForEach(p =>
                {
                    ProdutoDTO produtoDTO = new ProdutoDTO();
                    produtoDTO.ProdutoId = p.ProdutoId;
                    produtoDTO.Nome = p.Nome;
                    produtoDTO.UrlFotoProduto = p.UrlFotoProduto;
                    produtoDTO.DataCadastro = p.DataCadastro;
                    produtoDTO.PrecoCompra = p.PrecoCompra;
                    produtoDTO.PrecoVenda = p.PrecoVenda;
                    produtoDTO.Estoque = p.Estoque;
                    produtoDTO.Ativo = p.Ativo;
                    produtoDTO.Descricao = p.Descricao;
                    produtoDTO.CategoriaId = p.CategoriaId;

                    CategoriaDTO categoriaDTO = new CategoriaDTO();
                    categoriaDTO.CategoriaId = p.CategoriaId;
                    categoriaDTO.Nome = p.Categoria.Nome;
                    categoriaDTO.Status = p.Categoria.Ativo;

                    produtoDTO.CategoriaDTO = categoriaDTO;

                    produtosDTO.Add(produtoDTO);
                });

                return new Resposta<List<ProdutoDTO>>("Produtos encontrados com sucesso!", true, produtosDTO);
            }
            catch (TokenInvalidoException e)
            {

                return new Resposta<List<ProdutoDTO>>(e.Message, false, null);
            }
            catch (Exception e)
            {

                return new Resposta<List<ProdutoDTO>>("Erro ao tentar-se consultar os produtos!", false, null);
            }

        }

        // cadastrar produto
        public async Task<Resposta<ProdutoDTO>> CadastrarProduto(String token, ProdutoDTO produtoDTOCadastrar)
        {

            try
            {
                TokenDTO tokenDTO = await _tokenServico.ValidarTokenUsuario(token);

                // validar preço de compra
                if (produtoDTOCadastrar.PrecoCompra <= 0)
                {

                    return new Resposta<ProdutoDTO>("Preço de compra inválido!", false, null);
                }

                // validar preço de venda
                if (produtoDTOCadastrar.PrecoVenda <= 0)
                {

                    return new Resposta<ProdutoDTO>("Preço de venda inválido!", false, null);
                }

                if (produtoDTOCadastrar.PrecoCompra >= produtoDTOCadastrar.PrecoVenda)
                {

                    return new Resposta<ProdutoDTO>("O preço de compra não pode ser maior ou igual ao preço de venda!", false, null);
                }

                // validar estoque
                if (produtoDTOCadastrar.Estoque <= 0)
                {

                    return new Resposta<ProdutoDTO>("Estoque inválido!", false, null);
                }

                // validar categoria do produto
                Categoria categoriaProduto = await _categoriaRepositorio.BuscarCategoriaPeloId(produtoDTOCadastrar.CategoriaId);

                if (categoriaProduto is null)
                {

                    return new Resposta<ProdutoDTO>("Categoria não encontrada!", false, null);
                }

                if (!categoriaProduto.Ativo)
                {

                    return new Resposta<ProdutoDTO>("Categoria inativa!", false, null);
                }

                // validar se já existe outro produto cadastrado com o mesmo nome
                Produto produtoMesmoNome = await _produtoRepositorio.BuscarProdutoPeloNome(produtoDTOCadastrar.Nome.Trim());

                if (produtoMesmoNome is not null)
                {

                    return new Resposta<ProdutoDTO>("Já existe outro produto cadastrado com o mesmo nome!", false, null);
                }

                Produto produto = new Produto()
                {
                    Nome = produtoDTOCadastrar.Nome,
                    Ativo = produtoDTOCadastrar.Ativo,
                    DataCadastro = new DateTime(),
                    Descricao = produtoDTOCadastrar.Descricao,
                    CategoriaId = produtoDTOCadastrar.CategoriaId,
                    Estoque = produtoDTOCadastrar.Estoque,
                    PrecoCompra = produtoDTOCadastrar.PrecoCompra,
                    PrecoVenda = produtoDTOCadastrar.PrecoVenda,
                    UrlFotoProduto = produtoDTOCadastrar.UrlFotoProduto
                };

                await _produtoRepositorio.CadastrarProduto(produto);

                produtoDTOCadastrar.ProdutoId = produto.ProdutoId;

                if (produtoDTOCadastrar.CategoriaDTO is null)
                {
                    CategoriaDTO categoriaDTO = new CategoriaDTO();
                    categoriaDTO.CategoriaId = categoriaProduto.CategoriaId;
                    categoriaDTO.Status = categoriaProduto.Ativo;
                    categoriaDTO.Nome = categoriaProduto.Nome;

                    produtoDTOCadastrar.CategoriaDTO = categoriaDTO;
                }

                return new Resposta<ProdutoDTO>()
                {
                    Msg = "Produto cadastrado com sucesso!",
                    Conteudo = produtoDTOCadastrar,
                    Ok = true
                };
            }
            catch (TokenInvalidoException e)
            {

                return new Resposta<ProdutoDTO>(e.Message, false, null);
            }
            catch (Exception e)
            {

                return new Resposta<ProdutoDTO>("Erro ao tentar-se cadastrar o produto!", false, null);
            }

        }

        // deletar produto
        public async Task<Resposta<bool>> DeletarProdutoProduto(String token, int idProdutoDeletar)
        {

            try
            {
                TokenDTO tokenDTO = await _tokenServico.ValidarTokenUsuario(token);

                Produto produtoDeletar = await _produtoRepositorio.BuscarProdutoPeloId(idProdutoDeletar);

                if (produtoDeletar is null)
                {

                    return new Resposta<bool>("Produto não encontrado!", false, false);
                }

                // validar se existem vendas vinculadas ao produto em questão

                await _produtoRepositorio.DeletarProduto(produtoDeletar);

                return new Resposta<bool>("Produto deletado com sucesso!", true, true);
            }
            catch (TokenInvalidoException e)
            {

                return new Resposta<bool>(e.Message, false, false);
            }
            catch (Exception e)
            {

                return new Resposta<bool>("Erro ao tentar-se deletar o produto!", false, false);
            }

        }

        public async Task<Resposta<ProdutoDTO>> EditarProduto(String token, ProdutoDTO produtoDTOEditar)
        {
            throw new NotImplementedException();
        }

        // filtrar produtos
        public async Task<Resposta<List<ProdutoDTO>>> FiltrarProdutos(FiltroProdutos filtroProdutos)
        {

            try
            {
                String filtroErro = ValidarFiltroProdutos(filtroProdutos);
                
                if (filtroErro != "")
                {

                    return new Resposta<List<ProdutoDTO>>(filtroErro, false, null);
                }

                List<Produto> produtos = await _produtoRepositorio.FiltrarProdutos(filtroProdutos);

                if (produtos.Count == 0)
                {

                    return new Resposta<List<ProdutoDTO>>("Nenhum produto encontrados!", true, new List<ProdutoDTO>());
                }

                List<ProdutoDTO> produtosDTO = new List<ProdutoDTO>();

                produtos.ForEach(p =>
                {
                    ProdutoDTO produtoDTO = new ProdutoDTO();
                    produtoDTO.ProdutoId = p.ProdutoId;
                    produtoDTO.UrlFotoProduto = p.UrlFotoProduto;
                    produtoDTO.Nome = p.Nome;
                    produtoDTO.Descricao = p.Descricao;
                    produtoDTO.PrecoVenda = p.PrecoVenda;
                    produtoDTO.PrecoCompra = p.PrecoCompra;
                    produtoDTO.DataCadastro = p.DataCadastro;
                    produtoDTO.Ativo = p.Ativo;
                    produtoDTO.Estoque = p.Estoque;
                    produtoDTO.CategoriaId = p.CategoriaId;

                    CategoriaDTO categoriaDTO = new CategoriaDTO();
                    categoriaDTO.CategoriaId = p.CategoriaId;
                    categoriaDTO.Nome = p.Categoria.Nome;
                    categoriaDTO.Status = p.Categoria.Ativo;

                    produtoDTO.CategoriaDTO = categoriaDTO;

                    produtosDTO.Add(produtoDTO);
                });

                return new Resposta<List<ProdutoDTO>>("Produtos encontrados com sucesso!", true, produtosDTO);
            }
            catch (TokenInvalidoException e)
            {

                return new Resposta<List<ProdutoDTO>>(e.Message, false, null);
            }
            catch (Exception e)
            {

                return new Resposta<List<ProdutoDTO>>("Erro ao tentar-se filtrar os produtos: " + e.Message + " " + e.StackTrace, false, null);
            }

        }

        private String ValidarFiltroProdutos(FiltroProdutos filtroProdutos)
        {

            if (filtroProdutos.PrecoVendaInicial > filtroProdutos.PrecoVendaFinal)
            {

                return "O preço de venda inicial não deve ser maior que o preço de venda final!";
            }

            return "";
        }

        // alterar o status do produto
        public async Task<Resposta<ProdutoDTO>> AlterarStatusProdutos(int idProdutoAlterarStatus, bool novoStatus)
        {

            try
            {
                Produto produtoAlterarStatus = await _produtoRepositorio.BuscarProdutoPeloId(idProdutoAlterarStatus);

                if (produtoAlterarStatus is null)
                {

                    return new Resposta<ProdutoDTO>("Produto não encontrado!", false, null);
                }

                if (produtoAlterarStatus.Ativo == novoStatus)
                {

                    return new Resposta<ProdutoDTO>("O produto " + produtoAlterarStatus.Nome + " já possui o status a qual você deseja alterar!", true, null);
                }

                await _produtoRepositorio.AlterarStatusProduto(idProdutoAlterarStatus, novoStatus);

                ProdutoDTO produtoDTO = new ProdutoDTO();
                produtoDTO.ProdutoId = idProdutoAlterarStatus;
                produtoDTO.Nome = produtoAlterarStatus.Nome;
                produtoDTO.Descricao = produtoAlterarStatus.Descricao;
                produtoDTO.Ativo = novoStatus;
                produtoDTO.DataCadastro = produtoAlterarStatus.DataCadastro;
                produtoDTO.CategoriaId = produtoAlterarStatus.CategoriaId;
                produtoDTO.PrecoCompra = produtoAlterarStatus.PrecoCompra;
                produtoDTO.PrecoVenda = produtoAlterarStatus.PrecoVenda;
                produtoDTO.UrlFotoProduto = produtoAlterarStatus.UrlFotoProduto;
                produtoDTO.Estoque = produtoAlterarStatus.Estoque;

                CategoriaDTO categoriaDTO = new CategoriaDTO();
                categoriaDTO.CategoriaId = produtoAlterarStatus.CategoriaId;
                categoriaDTO.Nome = produtoAlterarStatus.Categoria.Nome;
                categoriaDTO.Status = produtoAlterarStatus.Categoria.Ativo;

                produtoDTO.CategoriaDTO = categoriaDTO;

                return new Resposta<ProdutoDTO>("Status alterado com sucesso!", true, produtoDTO);
            }
            catch (TokenInvalidoException e)
            {

                return new Resposta<ProdutoDTO>(e.Message, false, null);
            }
            catch (Exception e)
            {

                return new Resposta<ProdutoDTO>("Erro ao tentar-se alterar o status do produto!", false, null);
            }

        }

    }
}
