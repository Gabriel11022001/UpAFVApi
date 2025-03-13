using UpOnlineAFVApi.DTOs;
using UpOnlineAFVApi.Enums;
using UpOnlineAFVApi.Filtros;
using UpOnlineAFVApi.Models;
using UpOnlineAFVApi.Repositorio;
using UpOnlineAFVApi.Utils;

namespace UpOnlineAFVApi.Servico
{
    public class ClienteServico : IClienteServico
    {

        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly ITokenServico _tokenServico;
        private readonly int[] _elementosPorPaginaConsulta = [ 5, 10, 50 ];

        public ClienteServico(IClienteRepositorio clienteRepositorio, ITokenServico tokenServico)
        {
            _clienteRepositorio = clienteRepositorio;
            _tokenServico = tokenServico;
        }

        public async Task<Resposta<ClienteDTO>> AlterarStatusCliente(int idCliente, bool novoStatus)
        {
            throw new NotImplementedException();
        }

        // buscar cliente pelo id
        public async Task<Resposta<ClienteDTO>> BuscarClientePeloId(int idCliente)
        {

            throw new NotImplementedException();

            try
            {
                Cliente cliente = await _clienteRepositorio.BuscarClientePeloId(idCliente);

                if (cliente is null)
                {

                    return new Resposta<ClienteDTO>("Cliente não encontrado!", false, null);
                }

                ClienteDTO clienteDTO = new ClienteDTO();
                clienteDTO.ClienteId = cliente.ClienteId;
                clienteDTO.TipoPessoaNome = cliente.TipoPessoa;

                if (cliente.TipoPessoa == "pf")
                {
                    clienteDTO.TipoPessoa = Enums.TipoPessoa.PessoaFisica;
                }
                else
                {
                    clienteDTO.TipoPessoa = Enums.TipoPessoa.PessoaJuridica;
                }

            }
            catch (Exception e)
            {

                return new Resposta<ClienteDTO>("Erro ao tentar-se buscar o cliente pelo id!", false, null);
            }

        }

        private int ObterUltimaPaginaListagem(int totalElementos, int elementosPorPagina)
        {

            if (totalElementos <= elementosPorPagina)
            {

                return 1;
            }

            return totalElementos / elementosPorPagina;
        }

        // buscar clientes
        public async Task<Resposta<RetornoListagem<List<ClienteDTO>>>> BuscarClientes(String token, int paginaAtual, int elementosPorPagina)
        {

            try
            {
                // TokenDTO tokenDTO = await _tokenServico.ValidarTokenUsuario(token);

                if (paginaAtual <= 0)
                {
                    paginaAtual = 1;
                }

                if (!_elementosPorPaginaConsulta.Contains(elementosPorPagina))
                {
                    elementosPorPagina = _elementosPorPaginaConsulta[ 0 ];
                }

                List<Cliente> clientes = await _clienteRepositorio.BuscarClientes(paginaAtual, elementosPorPagina);
                int totalClientes = await _clienteRepositorio.BuscarTotalClientesCadastrados();
                int ultimaPagina = ObterUltimaPaginaListagem(totalClientes, elementosPorPagina);

                if (clientes.Count == 0)
                {

                    return new Resposta<RetornoListagem<List<ClienteDTO>>>("Nenhum cliente na pagina atual!", true, new RetornoListagem<List<ClienteDTO>>()
                    {
                        Elementos = new List<ClienteDTO>(),
                        PaginaAtual = paginaAtual,
                        TotalElementos = totalClientes,
                        UltimaPagina = ultimaPagina
                    });
                }

                List<ClienteDTO> clientesDTO = new List<ClienteDTO>();

                clientes.ForEach((cliente) =>
                {
                    ClienteDTO clienteDTO = new ClienteDTO();

                    clienteDTO.ClienteId = cliente.ClienteId;

                    if (cliente.TipoPessoa == "pf")
                    {
                        clienteDTO.TipoPessoa = TipoPessoa.PessoaFisica;
                    }
                    else
                    {
                        clienteDTO.TipoPessoa = TipoPessoa.PessoaJuridica;
                    }

                    clienteDTO.TipoPessoaNome = cliente.TipoPessoa;

                    clienteDTO.TelefonePrincipal = cliente.TelefonePrincipal;
                    clienteDTO.TelefoneSecundario = cliente.TelefoneSecundario;
                    clienteDTO.EmailPrincipal = cliente.EmailPrincipal;
                    clienteDTO.EmailSecundario = cliente.EmailSecundario;
                    clienteDTO.Status = cliente.Status;
                    clienteDTO.NomeCompleto = cliente.NomeCompleto;
                    clienteDTO.Cpf = cliente.Cpf;
                    clienteDTO.Rg = cliente.Rg;
                    clienteDTO.DataNascimento = cliente.DataNascimento;
                    clienteDTO.RazaoSocial = cliente.RazaoSocial;
                    clienteDTO.Cnpj = cliente.Cnpj;
                    clienteDTO.DataFundacao = cliente.DataFundacao;
                    clienteDTO.ValorPatrimonio = cliente.ValorPatrimonio;

                    if (cliente.Genero == "Masculino")
                    {
                        clienteDTO.Genero = Genero.Masculino;
                    }
                    else if (cliente.Genero == "Feminino")
                    {
                        clienteDTO.Genero = Genero.Feminino;
                    }
                    else if (cliente.Genero == "Outro")
                    {
                        clienteDTO.Genero = Genero.Outro;
                    }

                    clienteDTO.GeneroNome = cliente.Genero;

                    clienteDTO.EnderecoDTO = new EnderecoDTO()
                    {
                        EnderecoId = cliente.Endereco.EnderecoId,
                        Cep = cliente.Endereco.Cep,
                        Complemento = cliente.Endereco.Complemento,
                        Logradouro = cliente.Endereco.Logradouro,
                        Bairro = cliente.Endereco.Bairro,
                        Cidade = cliente.Endereco.Cidade,
                        Uf = cliente.Endereco.Uf
                    };

                    clientesDTO.Add(clienteDTO);
                });

                return new Resposta<RetornoListagem<List<ClienteDTO>>>("Clientes encontrados com sucesso!", true, new RetornoListagem<List<ClienteDTO>>()
                {
                    Elementos = clientesDTO,
                    PaginaAtual = paginaAtual,
                    TotalElementos = totalClientes,
                    UltimaPagina = ultimaPagina
                });
            }
            catch (TokenInvalidoException e)
            {

                return new Resposta<RetornoListagem<List<ClienteDTO>>>(e.Message, false, null);
            }
            catch (Exception e)
            {

                return new Resposta<RetornoListagem<List<ClienteDTO>>>("Erro ao tentar-se consultar os clientes!", false, null);
            }

        }

        public async Task<Resposta<bool>> DeletarCliente(int idClienteDeletar)
        {
            throw new NotImplementedException();
        }

        public async Task<Resposta<List<ClienteDTO>>> FiltrarClientes(int paginaAtual, int elementosPorPagina, FiltroClientes filtroClientes)
        {
            throw new NotImplementedException();
        }

        // salvar cliente (cadastrar/editar)
        public async Task<Resposta<ClienteDTO>> SalvarCliente(ClienteDTO clienteDTOSalvar)
        {
            await _clienteRepositorio.IniciarTransacao();

            try
            {

                if (clienteDTOSalvar.ClienteId == 0)
                {

                    // cadastrar cliente
                    return await CadastrarCliente(clienteDTOSalvar);
                }

                // editar cliente
                return await EditarCliente(clienteDTOSalvar);
            }
            catch (Exception e)
            {
                await _clienteRepositorio.RollbackTransacao();

                return new Resposta<ClienteDTO>()
                {
                    Msg = "Erro ao tentar-se salvar o cliente!",
                    Ok = false,
                    Conteudo = null
                };
            }

        }

        // cadastrar cliente
        private async Task<Resposta<ClienteDTO>> CadastrarCliente(ClienteDTO clienteDTOCadastrar)
        {

            try
            {

                // validar o tipo de pessoa informado
                if (clienteDTOCadastrar.TipoPessoa != TipoPessoa.PessoaFisica && clienteDTOCadastrar.TipoPessoa != TipoPessoa.PessoaJuridica)
                {

                    return new Resposta<ClienteDTO>("Tipo de pessoa inválido!", false, null);
                }

                if (clienteDTOCadastrar.TipoPessoa.Equals(TipoPessoa.PessoaFisica))
                {
                    // cadastrar cliente pessoa fisica
                    RetornoValidarDadosCliente retornoValidarDadosClientePessoaFisica = ValidaDadosCadastroCliente.ValidarDadosCadastroCliente(clienteDTOCadastrar);

                    if (!retornoValidarDadosClientePessoaFisica.Ok)
                    {

                        if (retornoValidarDadosClientePessoaFisica.ValidacoesDados.ContainsKey("cpf"))
                        {

                            return new Resposta<ClienteDTO>(retornoValidarDadosClientePessoaFisica.ValidacoesDados[ "cpf" ], false, null);
                        }

                        if (retornoValidarDadosClientePessoaFisica.ValidacoesDados.ContainsKey("dataNascimento"))
                        {

                            return new Resposta<ClienteDTO>(retornoValidarDadosClientePessoaFisica.ValidacoesDados[ "dataNascimento" ], false, null);
                        }

                        if (retornoValidarDadosClientePessoaFisica.ValidacoesDados.ContainsKey("emailPrincipal"))
                        {

                            return new Resposta<ClienteDTO>(retornoValidarDadosClientePessoaFisica.ValidacoesDados[ "emailPrincipal" ], false, null);
                        }

                        if (retornoValidarDadosClientePessoaFisica.ValidacoesDados.ContainsKey("emailSecundario"))
                        {

                            return new Resposta<ClienteDTO>(retornoValidarDadosClientePessoaFisica.ValidacoesDados[ "emailSecundario" ], false, null);
                        }

                        if (retornoValidarDadosClientePessoaFisica.ValidacoesDados.ContainsKey("telefonePrincipal"))
                        {

                            return new Resposta<ClienteDTO>(retornoValidarDadosClientePessoaFisica.ValidacoesDados[ "telefoneSecundario" ], false, null);
                        }

                        return new Resposta<ClienteDTO>("Erro ao tentar-se validar os dados!", false, null);
                    }
                    else
                    {
                        // validar se já existe outro cliente cadastrado com o mesmo e-mail principal
                        Cliente clienteMesmoEmailPrincipal = await _clienteRepositorio.BuscarClientePeloEmailPrincipal(clienteDTOCadastrar.EmailPrincipal);

                        if (clienteMesmoEmailPrincipal is not null)
                        {

                            return new Resposta<ClienteDTO>("Já existe outro cliente cadastrado com o e-mail principal informado!", false, null);
                        }

                        // validar se já existe outro cliente cadastrado com o mesmo cpf
                        Cliente clienteMesmoCpf = await _clienteRepositorio.BuscarClientePeloCpf(clienteDTOCadastrar.Cpf);

                        if (clienteMesmoCpf != null)
                        {

                            return new Resposta<ClienteDTO>("Já existe outro cliente cadastrado com esse cpf!", false, null);
                        }

                        // cadastrar o cliente
                        Cliente clienteCadastrar = new Cliente()
                        {
                            TipoPessoa = clienteDTOCadastrar.TipoPessoa == TipoPessoa.PessoaFisica ? "pf" : "pj",
                            EmailPrincipal = clienteDTOCadastrar.EmailPrincipal,
                            EmailSecundario = clienteDTOCadastrar.EmailSecundario,
                            TelefonePrincipal = clienteDTOCadastrar.TelefonePrincipal,
                            TelefoneSecundario = clienteDTOCadastrar.TelefoneSecundario,
                            DataCadastro = new DateTime(),
                            Status = clienteDTOCadastrar.Status,
                            NomeCompleto = clienteDTOCadastrar.NomeCompleto,
                            Cpf = clienteDTOCadastrar.Cpf,
                            DataNascimento = clienteDTOCadastrar.DataNascimento,
                            Rg = clienteDTOCadastrar.Rg,
                            RazaoSocial = "",
                            DataFundacao = new DateTime(),
                            Cnpj = "",
                            ValorPatrimonio = 0
                        };

                        if (clienteDTOCadastrar.Genero == Genero.Masculino)
                        {
                            clienteCadastrar.Genero = "Masculino";
                            clienteDTOCadastrar.GeneroNome = "Masculino";
                        }
                        else if (clienteDTOCadastrar.Genero == Genero.Feminino)
                        {
                            clienteCadastrar.Genero = "Feminino";
                            clienteDTOCadastrar.GeneroNome = "Feminino";
                        }
                        else
                        {
                            clienteCadastrar.Genero = "Outro";
                            clienteDTOCadastrar.GeneroNome = "Outro";
                        }

                        await _clienteRepositorio.CadastrarCliente(clienteCadastrar);

                        // cadastrar o endereço do cliente
                        Endereco endereco = new Endereco();
                        endereco.Cep = clienteDTOCadastrar.EnderecoDTO.Cep;
                        endereco.Logradouro = clienteDTOCadastrar.EnderecoDTO.Logradouro;
                        endereco.Bairro = clienteDTOCadastrar.EnderecoDTO.Bairro;
                        endereco.Complemento = clienteDTOCadastrar.EnderecoDTO.Complemento;
                        endereco.Cidade = clienteDTOCadastrar.EnderecoDTO.Cidade;
                        endereco.Uf = clienteDTOCadastrar.EnderecoDTO.Uf;
                        endereco.ClienteId = clienteCadastrar.ClienteId;

                        await _clienteRepositorio.CadastrarEnderecoCliente(endereco);

                        await _clienteRepositorio.CommitarTransacao();

                        clienteDTOCadastrar.ClienteId = clienteCadastrar.ClienteId;
                        clienteDTOCadastrar.EnderecoDTO.EnderecoId = endereco.EnderecoId;
                        clienteDTOCadastrar.TipoPessoaNome = clienteDTOCadastrar.TipoPessoa == TipoPessoa.PessoaFisica ? "pf" : "pj";

                        return new Resposta<ClienteDTO>("Cliente cadastrado com sucesso!", true, clienteDTOCadastrar);
                    }

                }
                else
                {
                    // cadastrar cliente pessoa juridica

                    return new Resposta<ClienteDTO>("Cadastrar cliente pessoa juridica!", true, null);
                }

            }
            catch (TokenInvalidoException e)
            {

                throw e;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        // editar cliente
        private async Task<Resposta<ClienteDTO>> EditarCliente(ClienteDTO clienteDTOEditar)
        {
            throw new NotImplementedException();
        }

    }
}
