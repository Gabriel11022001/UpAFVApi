using UpOnlineAFVApi.Filtros;
using UpOnlineAFVApi.Models;

namespace UpOnlineAFVApi.Repositorio
{
    public interface IClienteRepositorio: IControleTransacao
    {

        Task CadastrarClientePessoaFisica(ClientePessoaFisica clientePessoaFisicaCadastrar);

        Task CadastrarClientePessoaJuridica(ClientePessoaJuridica clientePessoaJuridicaCadastrar);

        Task<List<Cliente>> BuscarClientes(int paginaAtual, int totalElementosPorPagina);

        Task<Cliente> BuscarClientePeloId(int clienteId);

        Task DeletarCliente(Cliente clienteDeletar);

        Task EditarClientePessoaFisica(ClientePessoaFisica clientePessoaFisicaEditar);

        Task EditarClientePessoaJuridica(ClientePessoaJuridica clientePessoaJuridicaEditar);

        Task<List<Cliente>> FiltrarClientes(int paginaAtual, int totalElementosPorPagina, FiltroClientes filtroClientes);

        Task AlterarStatusCliente(int idCliente, Boolean novoStatus);

        Task<Cliente> BuscarClientePeloCpf(String cpf);

        Task<Cliente> BuscarClientePeloTelefonePrincipal(String telefonePrincipal);

        Task<Cliente> BuscarClientePeloEmailPrincipal(String emailPrincipal);

        Task CadastrarEnderecoCliente(Endereco endereco);

    }
}
