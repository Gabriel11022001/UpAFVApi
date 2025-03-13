using System.ComponentModel.DataAnnotations;
using UpOnlineAFVApi.Enums;

namespace UpOnlineAFVApi.DTOs
{
    public class ClienteDTO
    {

        public int ClienteId { get; set; }
        [ Required(ErrorMessage = "Informe o tipo de pessoa!") ]
        public TipoPessoa TipoPessoa { get; set; }
        public String TipoPessoaNome { get; set; }
        [ Required(ErrorMessage = "Informe o telefone principal!") ]
        public String TelefonePrincipal { get; set; }
        public String TelefoneSecundario { get; set; }
        [ Required(ErrorMessage = "Informe o e-mail principal!") ]
        public String EmailPrincipal { get; set; }
        public String EmailSecundario { get; set; }
        public Boolean Status { get; set; }

        // pessoa fisica
        public Genero Genero { get; set; }
        public String GeneroNome { get; set; }
        public String NomeCompleto { get; set; }
        public String Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public String Rg { get; set; }

        // pessoa juridica
        public String Cnpj { get; set; }
        public String RazaoSocial { get; set; }
        public Double ValorPatrimonio { get; set; }
        public DateTime DataFundacao { get; set; }

        // endereço
        [ Required(ErrorMessage = "Informe o endereço!") ]
        public EnderecoDTO EnderecoDTO { get; set; }

    }
}
