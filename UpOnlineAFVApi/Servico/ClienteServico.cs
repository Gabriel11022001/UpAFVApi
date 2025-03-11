using UpOnlineAFVApi.DTOs;
using UpOnlineAFVApi.Filtros;
using UpOnlineAFVApi.Models;
using UpOnlineAFVApi.Repositorio;
using UpOnlineAFVApi.Utils;

namespace UpOnlineAFVApi.Servico
{
    public class ClienteServico : IClienteServico
    {

        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteServico(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
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

        public async Task<Resposta<List<ClienteDTO>>> BuscarClientes(int paginaAtual, int elementosPorPagina)
        {
            throw new NotImplementedException();
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

            if (clienteDTOCadastrar.TipoPessoa == Enums.TipoPessoa.PessoaFisica)
            {

                // cadastrar cliente pessoa fisica
                return await CadastrarClientePessoaFisica(clienteDTOCadastrar);
            }

            // cadastrar cliente pessoa juridica
            return await CadastrarClientePessoaJuridica(clienteDTOCadastrar);
        }

        private async Task<Resposta<ClienteDTO>> EditarCliente(ClienteDTO clienteDTOEditar)
        {
            throw new NotImplementedException();
        }

        private async Task<Resposta<ClienteDTO>> CadastrarClientePessoaFisica(ClienteDTO clienteDTO)
        {
            // validar dados do cliente pessoa fisica
            RetornoValidarDadosCliente retornoValidarDadosClientePessoaFisica = ValidaDadosCadastroCliente.ValidarDadosCadastroCliente(clienteDTO);

            if (!retornoValidarDadosClientePessoaFisica.Ok)
            {

                if (retornoValidarDadosClientePessoaFisica.ValidacoesDados.ContainsKey("cpf"))
                {

                    return new Resposta<ClienteDTO>(retornoValidarDadosClientePessoaFisica.ValidacoesDados["cpf"], false, null);
                }

                if (retornoValidarDadosClientePessoaFisica.ValidacoesDados.ContainsKey("dataNascimento"))
                {

                    return new Resposta<ClienteDTO>(retornoValidarDadosClientePessoaFisica.ValidacoesDados["dataNascimento"], false, null);
                }

                if (retornoValidarDadosClientePessoaFisica.ValidacoesDados.ContainsKey("telefonePrincipal"))
                {

                    return new Resposta<ClienteDTO>(retornoValidarDadosClientePessoaFisica.ValidacoesDados["telefonePrincipal"], false, null);
                }

                if (retornoValidarDadosClientePessoaFisica.ValidacoesDados.ContainsKey("telefoneSecundario"))
                {

                    return new Resposta<ClienteDTO>(retornoValidarDadosClientePessoaFisica.ValidacoesDados["telefoneSecundario"], false, null);
                }

                if (retornoValidarDadosClientePessoaFisica.ValidacoesDados.ContainsKey("emailPrincipal"))
                {

                    return new Resposta<ClienteDTO>(retornoValidarDadosClientePessoaFisica.ValidacoesDados["emailPrincipal"], false, null);
                }

                if (retornoValidarDadosClientePessoaFisica.ValidacoesDados.ContainsKey("emailSecundario"))
                {

                    return new Resposta<ClienteDTO>(retornoValidarDadosClientePessoaFisica.ValidacoesDados["emailSecundario"], false, null);
                }

            }

            // validar se já existe outro cliente cadastrado com o mesmo cpf
            Cliente clienteMesmoCpf = await _clienteRepositorio.BuscarClientePeloCpf(clienteDTO.Cpf);

            if (clienteMesmoCpf is not null)
            {

                return new Resposta<ClienteDTO>()
                {
                    Msg = "Já existe outro cliente cadastrado com o mesmo cpf!",
                    Ok = false,
                    Conteudo = null
                };
            }

            // validar se já existe outro cliente cadastrado com o mesmo telefone principal
            Cliente clienteMesmoTelefonePrincipal = await _clienteRepositorio.BuscarClientePeloTelefonePrincipal(clienteDTO.TelefonePrincipal);

            if (clienteMesmoTelefonePrincipal is not null)
            {

                return new Resposta<ClienteDTO>()
                {
                    Msg = "Já existe outro cliente com o mesmo telefone principal!",
                    Conteudo = null,
                    Ok = false
                };
            }

            // validar se já existe outro cliente cadastrado com o mesmo e-mail principal
            Cliente clienteMesmoEmailPrincipal = await _clienteRepositorio.BuscarClientePeloEmailPrincipal(clienteDTO.EmailPrincipal);

            if (clienteMesmoEmailPrincipal is not null)
            {

                return new Resposta<ClienteDTO>()
                {
                    Msg = "Já existe outro cliente cadastrado com o mesmo e-mail principal!",
                    Conteudo = null,
                    Ok = false
                };
            }

            ClientePessoaFisica clientePessoaFisica = new ClientePessoaFisica();
            clientePessoaFisica.NomeCompleto = clienteDTO.NomeCompleto;
            clientePessoaFisica.TelefonePrincipal = clienteDTO.TelefonePrincipal;
            clientePessoaFisica.TelefoneSecundario = clienteDTO.TelefoneSecundario;
            clientePessoaFisica.Cpf = clienteDTO.Cpf;
            clientePessoaFisica.EmailPrincipal = clienteDTO.EmailPrincipal;
            clientePessoaFisica.EmailSecundario = clienteDTO.EmailSecundario;
            clientePessoaFisica.DataCadastro = new DateTime();
            clientePessoaFisica.TipoPessoa = "pf";
            clientePessoaFisica.DataNascimento = clienteDTO.DataNascimento;

            if (clienteDTO.Genero == Enums.Genero.Masculino)
            {
                clientePessoaFisica.Genero = "Masculino";
            }
            else if (clienteDTO.Genero == Enums.Genero.Feminino)
            {
                clientePessoaFisica.Genero = "Feminino";
            }
            else
            {
                clientePessoaFisica.Genero = "Outro";
            }

            clientePessoaFisica.Status = clienteDTO.Status;

            await _clienteRepositorio.CadastrarClientePessoaFisica(clientePessoaFisica);

            Endereco endereco = new Endereco();
            endereco.ClienteId = clientePessoaFisica.ClienteId;
            endereco.Cep = clienteDTO.EnderecoDTO.Cep;
            endereco.Logradouro = clienteDTO.EnderecoDTO.Logradouro;
            endereco.Complemento = clienteDTO.EnderecoDTO.Complemento;
            endereco.Cidade = clienteDTO.EnderecoDTO.Cidade;
            endereco.Bairro = clienteDTO.EnderecoDTO.Bairro;
            endereco.Uf = clienteDTO.EnderecoDTO.Uf;

            await _clienteRepositorio.CadastrarEnderecoCliente(endereco);

            clienteDTO.ClienteId = clientePessoaFisica.ClienteId;
            clienteDTO.EnderecoDTO.EnderecoId = endereco.EnderecoId;

            if (clienteDTO.Genero == Enums.Genero.Masculino)
            {
                clienteDTO.GeneroNome = "Masculino";
            }
            else if (clienteDTO.Genero == Enums.Genero.Feminino)
            {
                clienteDTO.GeneroNome = "Feminino";
            }
            else
            {
                clienteDTO.GeneroNome = "Outro";
            }

            if (clienteDTO.TipoPessoa == Enums.TipoPessoa.PessoaFisica)
            {
                clienteDTO.TipoPessoaNome = "pf";
            }
            else
            {
                clienteDTO.TipoPessoaNome = "pj";
            }

            await _clienteRepositorio.CommitarTransacao();

            return new Resposta<ClienteDTO>("Cliente cadastrado com sucesso!", true, clienteDTO);
        }

        private async Task<Resposta<ClienteDTO>> CadastrarClientePessoaJuridica(ClienteDTO clienteDTO)
        {
            throw new NotImplementedException();
        }

    }
}
