using UpOnlineAFVApi.Contexto;
using UpOnlineAFVApi.Filtros;
using UpOnlineAFVApi.Models;
using Microsoft.EntityFrameworkCore;

namespace UpOnlineAFVApi.Repositorio
{
    public class ClienteRepositorio : RepositorioBase, IClienteRepositorio
    {

        public ClienteRepositorio(ApiDbContexto contexto): base(contexto) { }

        // alterar status do cliente
        public async Task AlterarStatusCliente(int idCliente, bool novoStatus)
        {
            Cliente clienteAlterarStatus = await Contexto.Clientes.FindAsync(idCliente);

            if (clienteAlterarStatus is not null)
            {
                clienteAlterarStatus.Status = novoStatus;

                Contexto.Clientes.Entry(clienteAlterarStatus).State = EntityState.Modified;
                await Contexto.SaveChangesAsync();
            }

        }

        // buscar cliente pelo id
        public async Task<Cliente> BuscarClientePeloId(int clienteId)
        {

            return await Contexto.Clientes.FindAsync(clienteId);
        }

        // buscar clientes paginado
        public async Task<List<Cliente>> BuscarClientes(int paginaAtual, int totalElementosPorPagina)
        {

            return await Contexto.Clientes
                .OrderBy(c => c.DataCadastro)
                .Include(c => c.Endereco)
                .Skip((paginaAtual - 1) * totalElementosPorPagina)
                .Take(totalElementosPorPagina)
                .ToListAsync();
        }

        // deletar cliente
        public async Task DeletarCliente(Cliente clienteDeletar)
        {
            Contexto.Clientes.Entry(clienteDeletar).State = EntityState.Deleted;
            await Contexto.SaveChangesAsync();
        }

        public async Task<List<Cliente>> FiltrarClientes(int paginaAtual, int totalElementosPorPagina, FiltroClientes filtroClientes)
        {
            throw new NotImplementedException();
        }

        // buscar cliente pelo cpf
        public async Task<Cliente> BuscarClientePeloCpf(String cpf)
        {

            return await Contexto.Clientes.FirstOrDefaultAsync(c => c.Cpf.Equals(cpf.Trim()));
        }

        // buscar cliente pelo telefone principal
        public async Task<Cliente> BuscarClientePeloTelefonePrincipal(string telefonePrincipal)
        {

            return await Contexto.Clientes.FirstOrDefaultAsync(c => c.TelefonePrincipal.Equals(telefonePrincipal.Trim()));
        }

        // buscar cliente pelo e-mail principal
        public async Task<Cliente> BuscarClientePeloEmailPrincipal(string emailPrincipal)
        {

            return await Contexto.Clientes.FirstOrDefaultAsync(c => c.EmailPrincipal.Equals(emailPrincipal));
        }

        // cadastrar endereço do cliente
        public async Task CadastrarEnderecoCliente(Endereco endereco)
        {
            await Contexto.Enderecos.AddAsync(endereco);
            await Contexto.SaveChangesAsync();
        }

        public async Task IniciarTransacao()
        {
            await Contexto.Database.BeginTransactionAsync();
        }

        public async Task CommitarTransacao()
        {
            await Contexto.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransacao()
        {
            await Contexto.Database.RollbackTransactionAsync();
        }

        // cadastrar cliente
        public async Task CadastrarCliente(Cliente clienteCadastrar)
        {
            await Contexto.Clientes.AddAsync(clienteCadastrar);
            await Contexto.SaveChangesAsync();
        }

        // editar cliente
        public async Task EditarCliente(Cliente clienteEditar)
        {
            Contexto.Clientes.Entry(clienteEditar).State = EntityState.Modified;
            await Contexto.SaveChangesAsync();
        }

        // buscar total de clientes cadastrados no banco de dados
        public async Task<int> BuscarTotalClientesCadastrados()
        {
            List<Cliente> clientes = await Contexto.Clientes.ToListAsync();

            return clientes.Count;
        }

    }
}
