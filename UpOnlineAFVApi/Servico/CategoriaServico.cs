using UpOnlineAFVApi.DTOs;
using UpOnlineAFVApi.Models;
using UpOnlineAFVApi.Repositorio;

namespace UpOnlineAFVApi.Servico
{
    public class CategoriaServico : ICategoriaServico
    {

        private readonly ICategoriaRepositorio _categoriaRepositorio;

        public CategoriaServico(ICategoriaRepositorio categoriaRepositorio)
        {
            _categoriaRepositorio = categoriaRepositorio;
        }

        // alterar status da categoria
        public async Task<Resposta<CategoriaDTO>> AlterarStatusCategoria(int idCategoria, bool novoStatus)
        {

            try
            {
                Categoria categoriaAlterarStatus = await _categoriaRepositorio.BuscarCategoriaPeloId(idCategoria);

                if (categoriaAlterarStatus is null)
                {

                    return new Resposta<CategoriaDTO>("Categoria não encontrada!", false, null);
                }

                if (categoriaAlterarStatus.Ativo == novoStatus)
                {

                    return new Resposta<CategoriaDTO>("A categoria já possui esse status!", true, null);
                }

                categoriaAlterarStatus.Ativo = novoStatus;

                await _categoriaRepositorio.EditarCategoria(categoriaAlterarStatus);

                CategoriaDTO categoriaDTO = new CategoriaDTO();
                categoriaDTO.CategoriaId = categoriaAlterarStatus.CategoriaId;
                categoriaDTO.Nome = categoriaAlterarStatus.Nome;
                categoriaDTO.Status = categoriaAlterarStatus.Ativo;

                return new Resposta<CategoriaDTO>("Status da categoria alterado com sucesso!", true, categoriaDTO);
            }
            catch (Exception e)
            {

                return new Resposta<CategoriaDTO>("Erro ao tentar-se alterar o status da categoria!", false, null); 
            }

        }

        // buscar categoria pelo id
        public async Task<Resposta<CategoriaDTO>> BuscarCategoriaPeloId(int idCategoriaFiltrar)
        {

            try
            {
                Categoria categoria = await _categoriaRepositorio.BuscarCategoriaPeloId(idCategoriaFiltrar);

                if (categoria is null)
                {

                    return new Resposta<CategoriaDTO>("Categoria não encontrada!", false, null);
                }

                CategoriaDTO categoriaDTO = new CategoriaDTO();
                categoriaDTO.CategoriaId = categoria.CategoriaId;
                categoriaDTO.Nome = categoria.Nome;
                categoriaDTO.Status = categoria.Ativo;

                return new Resposta<CategoriaDTO>("Categoria encontrada com sucesso!", true, categoriaDTO);
            }
            catch (Exception e)
            {

                return new Resposta<CategoriaDTO>("Erro ao tentar-se buscar a categoria pelo id!", false, null);
            }

        }

        public async Task<Resposta<List<CategoriaDTO>>> BuscarCategorias(int paginaAtual, int totalElementosPorPagina)
        {
            throw new NotImplementedException();
        }

        public async Task<Resposta<bool>> DeletarCategoria(int idCategoriaDeletar)
        {
            throw new NotImplementedException();
        }

        public async Task<Resposta<List<CategoriaDTO>>> FiltrarCategoriaPeloStatus(bool status)
        {
            throw new NotImplementedException();
        }

        public async Task<Resposta<List<CategoriaDTO>>> FiltrarCategoriasPeloNome(string nomeCategoriaFiltrar)
        {
            throw new NotImplementedException();
        }

        // salvar categoria na base de dados
        public async Task<Resposta<CategoriaDTO>> SalvarCategoria(CategoriaDTO categoriaDTOSalvar)
        {

            try
            {

                if (categoriaDTOSalvar.CategoriaId == 0)
                {

                    // cadastrar categoria
                    return await CadastrarCategoria(categoriaDTOSalvar);
                }

                // editar categoria
                return await EditarCategoria(categoriaDTOSalvar);
            }
            catch (Exception e)
            {

                return new Resposta<CategoriaDTO>("Erro ao tentar-se salvar a categoria!", false, null);
            }

        }

        // cadastrar categoria na base de dados
        private async Task<Resposta<CategoriaDTO>> CadastrarCategoria(CategoriaDTO categoriaDTOCadastrar)
        {
            // validar se já não existe outra categoria cadastrada com o mesmo nome
            Categoria categoriaCadastradaMesmoNome = await _categoriaRepositorio.BuscarCategoriaPeloNome(categoriaDTOCadastrar.Nome);

            if (categoriaCadastradaMesmoNome is not null)
            {

                return new Resposta<CategoriaDTO>("Já existe outra categoria cadastrada com o mesmo nome!", false, null);
            }

            Categoria categoriaCadastrar = new Categoria()
            {
                Nome = categoriaDTOCadastrar.Nome.Trim(),
                Ativo = categoriaDTOCadastrar.Status
            };

            await _categoriaRepositorio.CadastrarCategoria(categoriaCadastrar);

            categoriaDTOCadastrar.CategoriaId = categoriaCadastrar.CategoriaId;

            return new Resposta<CategoriaDTO>("Categoria cadastrada com sucesso!", true, categoriaDTOCadastrar);
        }

        // editar categoria
        private async Task<Resposta<CategoriaDTO>> EditarCategoria(CategoriaDTO categoriaDTOEditar)
        {
            // validar se já existe outra categorias cadastrada com o mesmo nome
            Categoria categoriaMesmoNome = await _categoriaRepositorio.BuscarCategoriaPeloNome(categoriaDTOEditar.Nome);

            if (categoriaMesmoNome is not null && categoriaMesmoNome.CategoriaId != categoriaDTOEditar.CategoriaId)
            {

                return new Resposta<CategoriaDTO>("Já existe outra categoria cadastrada com o mesmo nome!", false, null);
            }

            Categoria categoriaEditar = await _categoriaRepositorio.BuscarCategoriaPeloId(categoriaDTOEditar.CategoriaId);

            if (categoriaEditar is null)
            {

                return new Resposta<CategoriaDTO>("Categoria não encontrada!", false, null);
            }

            categoriaEditar.Nome = categoriaDTOEditar.Nome;
            categoriaEditar.Ativo = categoriaDTOEditar.Status;

            await _categoriaRepositorio.EditarCategoria(categoriaEditar);

            return new Resposta<CategoriaDTO>("Categoria editada com sucesso!", true, categoriaDTOEditar);
        }

    }
}
