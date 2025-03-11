using UpOnlineAFVApi.DTOs;

namespace UpOnlineAFVApi.Utils
{
    public static class ValidaDadosCadastroCliente
    {

        // validar dados do cliente durante o cadastro
        public static RetornoValidarDadosCliente ValidarDadosCadastroCliente(ClienteDTO clienteDTOValidar)
        {

            if (clienteDTOValidar.TipoPessoa == Enums.TipoPessoa.PessoaFisica)
            {
                // validar dados da pessoa fisica

                return ValidarDadosClientePessoaFisica(clienteDTOValidar);
            }
            else
            {
                // validar dados da pessoa juridica

                return ValidarDadosClientePessoaJuridica(clienteDTOValidar);
            }

        }

        // validar dados da pessoa fisica
        private static RetornoValidarDadosCliente ValidarDadosClientePessoaFisica(ClienteDTO clienteDTO)
        {
            RetornoValidarDadosCliente retornoValidarDadosClientePessoaFisica = new RetornoValidarDadosCliente();

            if (!ValidaCpf.ValidarCpf(clienteDTO.Cpf))
            {
                retornoValidarDadosClientePessoaFisica.Ok = false;
                retornoValidarDadosClientePessoaFisica.ValidacoesDados.Add("cpf", "Cpf inválido!");
            }

            if (!ValidaDataNascimento.ValidarDataNascimento(clienteDTO.DataNascimento))
            {
                retornoValidarDadosClientePessoaFisica.Ok = false;
                retornoValidarDadosClientePessoaFisica.ValidacoesDados.Add("dataNascimento", "Data nascimento inválida!");
            }

            if (!ValidaEmail.ValidarEmail(clienteDTO.EmailPrincipal))
            {
                retornoValidarDadosClientePessoaFisica.Ok = false;
                retornoValidarDadosClientePessoaFisica.ValidacoesDados.Add("emailPrincipal", "E-mail principal inválido!");
            }

            if (clienteDTO.EmailSecundario.Trim() != "" && !ValidaEmail.ValidarEmail(clienteDTO.EmailSecundario))
            {
                retornoValidarDadosClientePessoaFisica.Ok = false;
                retornoValidarDadosClientePessoaFisica.ValidacoesDados.Add("emailSecundario", "E-mail secundário inválido!");
            }

            if (!ValidaTelefone.ValidarTelefone(clienteDTO.TelefonePrincipal))
            {
                retornoValidarDadosClientePessoaFisica.Ok = false;
                retornoValidarDadosClientePessoaFisica.ValidacoesDados.Add("telefonePrincipal", "Telefone principal inválido!");
            }

            if (clienteDTO.TelefoneSecundario.Trim() != "" && !ValidaEmail.ValidarEmail(clienteDTO.EmailSecundario))
            {
                retornoValidarDadosClientePessoaFisica.Ok = false;
                retornoValidarDadosClientePessoaFisica.ValidacoesDados.Add("telefoneSecundario", "Telefone secundário inválido!");
            }

            return retornoValidarDadosClientePessoaFisica;
        }

        // validar dados da pessoa juridica
        private static RetornoValidarDadosCliente ValidarDadosClientePessoaJuridica(ClienteDTO clienteDTO)
        {
            RetornoValidarDadosCliente retornoValidarDadosClientePessoaJuridica = new RetornoValidarDadosCliente();

            return retornoValidarDadosClientePessoaJuridica;
        }

    }
}
