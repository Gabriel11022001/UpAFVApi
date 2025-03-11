using System.ComponentModel.DataAnnotations;

namespace UpOnlineAFVApi.Models
{
    public class Usuario
    {

        public int UsuarioId { get; set; }
        [ Required(ErrorMessage = "Informe o nome do usuário!") ]
        [ MaxLength(255, ErrorMessage = "O nome do usuário deve possuir no máximo 255 caracateres!") ]
        [ MinLength(3, ErrorMessage = "O nome do usuário deve possuir no mínimo 3 caracteres!") ]
        public string Nome { get; set; }
        [ Required(ErrorMessage = "Informe o e-mail do usuário!") ]
        [ MaxLength(255, ErrorMessage = "O e-mail do usuário deve possuir no máximo 255 caracteres!") ]
        public string Email { get; set; }
        [ Required(ErrorMessage = "Informe a senha do usuário!") ]
        [ MaxLength(6, ErrorMessage = "A senha do usuário deve possuir no máximo 6 caracteres!") ]
        [ MinLength(6, ErrorMessage = "A senha do usuário deve possuir no mínimo 6 caracteres!") ]
        public string Senha { get; set; }
        [ Required(ErrorMessage = "Informe o telefone do usuário!") ]
        [ MaxLength(100, ErrorMessage = "O telefone deve possuir no máximo 100 caracteres!") ]
        public string Telefone { get; set; }
        [ Required(ErrorMessage = "Informe a data de cadastro do usuário!") ]
        public DateTime DataCadastro { get; set; }
        [ Required(ErrorMessage = "Informe o status do usuário!") ]
        public Boolean Ativo { get; set; }
        public int NivelAcessoUsuarioId { get; set; }
        public NivelAcessoUsuario NivelAcessoUsuario { get; set; }

    }
}
