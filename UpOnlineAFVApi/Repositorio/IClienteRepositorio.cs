using UpOnlineAFVApi.Filtros;
using UpOnlineAFVApi.Models;

namespace UpOnlineAFVApi.Repositorio
{
    public interface IClienteRepositorio: IControleTransacao
    {

        Task CadastrarCliente(Cliente clienteCadastrar);

        Task EditarCliente(Cliente clienteEditar);

        Task<List<Cliente>> BuscarClientes(int paginaAtual, int totalElementosPorPagina);

        Task<Cliente> BuscarClientePeloId(int clienteId);

        Task DeletarCliente(Cliente clienteDeletar);

        Task<List<Cliente>> FiltrarClientes(int paginaAtual, int totalElementosPorPagina, FiltroClientes filtroClientes);

        Task AlterarStatusCliente(int idCliente, Boolean novoStatus);

        Task<Cliente> BuscarClientePeloCpf(String cpf);

        Task<Cliente> BuscarClientePeloTelefonePrincipal(String telefonePrincipal);

        Task<Cliente> BuscarClientePeloEmailPrincipal(String emailPrincipal);

        Task CadastrarEnderecoCliente(Endereco endereco);

        Task<int> BuscarTotalClientesCadastrados();

    }
}
