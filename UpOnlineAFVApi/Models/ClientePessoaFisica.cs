using System.ComponentModel.DataAnnotations;

namespace UpOnlineAFVApi.Models
{
    public class ClientePessoaFisica: Cliente
    {

        [ Required(ErrorMessage = "Informe o nome completo!") ]
        [ MaxLength(255, ErrorMessage = "O nome completo deve possuir no máximo 255 caracteres!") ]
        [ MinLength(3, ErrorMessage = "O nome completo deve possuir no mínimo 3 caracteres!") ]
        public String NomeCompleto { get; set; }
        [ Required(ErrorMessage = "Informe o cpf!") ]
        [ MaxLength(14, ErrorMessage = "O cpf deve possuir no máximo 14 caracteres!") ]
        [ MinLength(14, ErrorMessage = "O cpf deve possuir no mínimo 14 caracteres!") ]
        public String Cpf { get; set; }
        [ Required(ErrorMessage = "Informe a data de nascimento!") ]
        public DateTime DataNascimento { get; set; }
        [ Required(ErrorMessage = "Informe o gênero!") ]
        public String Genero { get; set; }
        [ Required(ErrorMessage = "Informe o rg!") ]
        public String Rg { get; set; }

    }
}
