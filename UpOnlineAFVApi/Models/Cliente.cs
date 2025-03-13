using System.ComponentModel.DataAnnotations;

namespace UpOnlineAFVApi.Models
{
    public class Cliente
    {

        public int ClienteId { get; set; }
        [ Required(ErrorMessage = "Informe o tipo de pessoa!") ]
        public String TipoPessoa { get; set; }
        [ Required(ErrorMessage = "Informe o e-mail principal") ]
        [ MaxLength(255, ErrorMessage = "O e-mail principal deve possuir no máximo 255 caracteres!") ]
        [ MinLength(6, ErrorMessage = "O e-mail principal deve possuir no mínimo 6 caracteres!") ]
        public String EmailPrincipal { get; set; }
        [MaxLength(255, ErrorMessage = "O e-mail secundário deve possuir no máximo 255 caracteres!")]
        [MinLength(6, ErrorMessage = "O e-mail secundário deve possuir no mínimo 6 caracteres!")]
        public String EmailSecundario { get; set; }
        [ Required(ErrorMessage = "Informe o telefone principal!") ]
        [ MaxLength(100, ErrorMessage = "O telefone principal deve possuir no máximo 100 caracteres!") ]
        [ MinLength(6, ErrorMessage = "O telefone principal deve possuir no mínimo 6 caracteres!") ]
        public String TelefonePrincipal { get; set; }
        public String TelefoneSecundario { get; set; }
        [ Required(ErrorMessage = "Informe o status!") ]
        public Boolean Status { get; set; }
        [ Required(ErrorMessage = "Informe a data de cadastro!") ]
        public DateTime DataCadastro { get; set; }
        [ Required(ErrorMessage = "Informe o endereço!") ]
        public Endereco Endereco { get; set; }

        // pessoa fisica
        public String NomeCompleto { get; set; }
        public String Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public String Genero { get; set; }
        public String Rg { get; set; }

        // pessoa juridica
        public String Cnpj { get; set; }
        public String RazaoSocial { get; set; }
        public DateTime DataFundacao { get; set; }
        public Double ValorPatrimonio { get; set; }

    }
}
