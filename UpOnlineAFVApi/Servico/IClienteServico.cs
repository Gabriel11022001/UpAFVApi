using UpOnlineAFVApi.DTOs;
using UpOnlineAFVApi.Filtros;

namespace UpOnlineAFVApi.Servico
{
    public interface IClienteServico
    {

        Task<Resposta<ClienteDTO>> SalvarCliente(ClienteDTO clienteDTOSalvar);

        Task<Resposta<List<ClienteDTO>>> BuscarClientes(int paginaAtual, int elementosPorPagina);

        Task<Resposta<ClienteDTO>> BuscarClientePeloId(int idCliente);

        Task<Resposta<List<ClienteDTO>>> FiltrarClientes(int paginaAtual, int elementosPorPagina, FiltroClientes filtroClientes);

        Task<Resposta<Boolean>> DeletarCliente(int idClienteDeletar);

        Task<Resposta<ClienteDTO>> AlterarStatusCliente(int idCliente, Boolean novoStatus);

    }
}
