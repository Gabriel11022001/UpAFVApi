using System.ComponentModel.DataAnnotations;

namespace UpOnlineAFVApi.Models
{
    public class ClientePessoaJuridica: Cliente
    {

        [ Required(ErrorMessage = "Informe o cnpj!") ]
        public String Cnpj { get; set; }
        [ Required(ErrorMessage = "Informe a razão social!") ]
        public String RazaoSocial { get; set; }
        [ Required(ErrorMessage = "Informe a data de fundação!") ]
        public DateTime DataFundacao { get; set; }
        public Double ValorPatrimonio { get; set; }

    }
}
