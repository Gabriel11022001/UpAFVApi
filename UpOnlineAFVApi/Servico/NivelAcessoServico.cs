using UpOnlineAFVApi.DTOs;
using UpOnlineAFVApi.Models;
using UpOnlineAFVApi.Repositorio;

namespace UpOnlineAFVApi.Servico
{
    public class NivelAcessoServico : INivelAcessoServico
    {

        private readonly INivelAcessoRepositorio _nivelAcessoRepositorio;
        private readonly IPermissaoRepositorio _permissaoRepositorio;

        public NivelAcessoServico(INivelAcessoRepositorio nivelAcessoRepositorio, IPermissaoRepositorio permissaoRepositorio)
        {
            _nivelAcessoRepositorio = nivelAcessoRepositorio;
            _permissaoRepositorio = permissaoRepositorio;
        }

        // cadastrar multiplos niveis de acesso
        public async Task<Resposta<List<NivelAcessoUsuarioDTO>>> CadastrarMultiplosNiveisAcessos(List<NivelAcessoUsuarioDTO> niveisAcessoDTOSCadastrar)
        {
            await _nivelAcessoRepositorio.IniciarTransacao();

            try
            {
                bool todosNiveisAcessoCorretos = true;
                String nivelAcessoErrado = "";
                List<NivelAcessoUsuario> niveisAcesso = new List<NivelAcessoUsuario>();
               
                foreach (NivelAcessoUsuarioDTO nivelAcessoUsuarioDTO in niveisAcessoDTOSCadastrar)
                {
                    // validar se as permissões existem
                    List<PermissaoNivelAcessoUsuarioDTO> permissoesNiveisAcessoDTO = nivelAcessoUsuarioDTO.PermissaoNivelAcessoUsuarioDTOS;

                    bool permissoesOk = true;
                    int idPermissaoIncorreta = 0;

                    List<PermissaoNivelAcessoUsuario> permissoes = new List<PermissaoNivelAcessoUsuario>();

                    foreach (PermissaoNivelAcessoUsuarioDTO permissaoNivelAcessoUsuarioDTO in permissoesNiveisAcessoDTO)
                    {
                        PermissaoNivelAcessoUsuario permissaoNivelAcessoUsuario = await _permissaoRepositorio.BuscarPermissaoPeloId(permissaoNivelAcessoUsuarioDTO.PermissaoNivelAcessoUsuarioId);
                        
                        if (permissaoNivelAcessoUsuario is null)
                        {
                            permissoesOk = false;
                            idPermissaoIncorreta = permissaoNivelAcessoUsuarioDTO.PermissaoNivelAcessoUsuarioId;
                        }
                        else
                        {
                            permissoes.Add(permissaoNivelAcessoUsuario);
                        }

                    }

                    if (!permissoesOk)
                    {
                        todosNiveisAcessoCorretos = false;
                        nivelAcessoErrado = nivelAcessoUsuarioDTO.Nome;
                    }
                    else
                    {
                        NivelAcessoUsuario nivelAcessoUsuario = new NivelAcessoUsuario();
                        nivelAcessoUsuario.Nome = nivelAcessoUsuarioDTO.Nome;
                        nivelAcessoUsuario.Ativo = nivelAcessoUsuarioDTO.Ativo;
                        nivelAcessoUsuario.Permissoes = permissoes;

                        niveisAcesso.Add(nivelAcessoUsuario);
                    }
                   
                }

                if (!todosNiveisAcessoCorretos)
                {

                    return new Resposta<List<NivelAcessoUsuarioDTO>>("Nível de acesso " + nivelAcessoErrado + " incorreto!", false, null);
                }

                foreach (NivelAcessoUsuario nivelAcesso in niveisAcesso)
                {
                    await _nivelAcessoRepositorio.CadastrarNivelAcesso(nivelAcesso);
                }

                await _nivelAcessoRepositorio.CommitarTransacao();

                List<NivelAcessoUsuarioDTO> niveisAcessoDTORetornar = new List<NivelAcessoUsuarioDTO>();

                foreach (NivelAcessoUsuario nivelAcessoUsuario in niveisAcesso)
                {
                    NivelAcessoUsuarioDTO nivelAcessoUsuarioDTO = new NivelAcessoUsuarioDTO();
                    nivelAcessoUsuarioDTO.NivelAcessoId = nivelAcessoUsuario.NivelAcessoUsuarioId;
                    nivelAcessoUsuarioDTO.Nome = nivelAcessoUsuario.Nome;
                    nivelAcessoUsuarioDTO.Ativo = nivelAcessoUsuario.Ativo;

                    niveisAcessoDTORetornar.Add(nivelAcessoUsuarioDTO);
                }

                return new Resposta<List<NivelAcessoUsuarioDTO>>("Níveis de acesso cadastrados com sucesso!", true, niveisAcessoDTORetornar);
            }
            catch (Exception e)
            {
                await _nivelAcessoRepositorio.RollbackTransacao();

                return new Resposta<List<NivelAcessoUsuarioDTO>>("Erro ao tentar-se cadastrar multiplos niveis de acesso!", false, null); 
            }

        }

        // cadastrar nivel de acesso
        public async Task<Resposta<NivelAcessoUsuarioDTO>> CadastrarNivelAcesso(NivelAcessoUsuarioDTO nivelAcessoUsuarioDTOCadastrar)
        {

            try
            {
                // validar se existem as permissões informadas
                List<PermissaoNivelAcessoUsuarioDTO> permissoesDTO = nivelAcessoUsuarioDTOCadastrar.PermissaoNivelAcessoUsuarioDTOS;

                if (permissoesDTO.Count == 0)
                {

                    return new Resposta<NivelAcessoUsuarioDTO>("Informe pelo menos uma permissão para o nivel de acesso!", false, null);
                }

                bool todasPermissoesExistem = true;
                int idPermissaoNaoExiste = 0;

                List<PermissaoNivelAcessoUsuario> permissoes = new List<PermissaoNivelAcessoUsuario>();

                foreach (PermissaoNivelAcessoUsuarioDTO permissaoNivelAcessoUsuarioDTO in permissoesDTO)
                {
                    int idPermissao = permissaoNivelAcessoUsuarioDTO.PermissaoNivelAcessoUsuarioId;
                    PermissaoNivelAcessoUsuario permissao = await _permissaoRepositorio.BuscarPermissaoPeloId(idPermissao);

                    if (permissao is null)
                    {
                        todasPermissoesExistem = false;
                        idPermissaoNaoExiste = idPermissao;
                    }
                    else
                    {
                        permissoes.Add(permissao);
                    }

                }

                if (!todasPermissoesExistem)
                {

                    return new Resposta<NivelAcessoUsuarioDTO>("Não existe uma permissão cadastrada com o id " + idPermissaoNaoExiste, false, null);
                }

                NivelAcessoUsuario nivelAcesso = new NivelAcessoUsuario();
                nivelAcesso.Nome = nivelAcessoUsuarioDTOCadastrar.Nome;
                nivelAcesso.Ativo = nivelAcessoUsuarioDTOCadastrar.Ativo;
                nivelAcesso.Permissoes = permissoes;

                await _nivelAcessoRepositorio.CadastrarNivelAcesso(nivelAcesso);

                nivelAcessoUsuarioDTOCadastrar.NivelAcessoId = nivelAcesso.NivelAcessoUsuarioId;

                return new Resposta<NivelAcessoUsuarioDTO>("Nível de acesso cadastrado com sucesso!", true, nivelAcessoUsuarioDTOCadastrar);
            }
            catch (Exception e)
            {

                return new Resposta<NivelAcessoUsuarioDTO>("Erro ao tentar-se cadastrar o nivel de acesso!", false, null);
            }

        }

    }
}
