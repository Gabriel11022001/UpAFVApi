using Microsoft.IdentityModel.Tokens;
using UpOnlineAFVApi.Enums;

namespace UpOnlineAFVApi.Filtros
{
    public class FiltroClientes
    {

        public TipoPessoa TipoPessoa { get; set; }
        public String NomeCompleto { get; set; }
        public String Cpf { get; set; }
        public String EmailPrincipal { get; set; }
        public String EmailSecundario { get; set; }
        public String TelefonePrincipal { get; set; }
        public String TelefoneSecundario { get; set; }
        public Genero Genero { get; set; }
        public DateTime DataNascimentoInicio { get; set; }
        public DateTime DataNascimentoFinal { get; set; }
        public String Cnpj { get; set; }
        public String RazaoSocial { get; set; }
        public DateTime DataFundacaoInicio { get; set; }
        public DateTime DataFundacaoFinal { get; set; }

    }
}
