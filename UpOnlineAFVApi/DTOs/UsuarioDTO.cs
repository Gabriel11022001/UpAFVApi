using System.ComponentModel.DataAnnotations;

namespace UpOnlineAFVApi.DTOs
{
    public class UsuarioDTO
    {

        public int UsuarioId { get; set; }
        [ Required(ErrorMessage = "Informe o nome do usuário!") ]
        [ StringLength(255, MinimumLength = 3, ErrorMessage = "O nome do usuário deve ter entre 3 e 255 caracteres!") ]
        public String Nome { get; set; }
        [ Required(ErrorMessage = "Informe o e-mail do usuário!") ]
        [ StringLength(255, ErrorMessage = "O e-mail deve possuir no máximo 255 caracteres!") ]
        public String Email { get; set; }
        [ Required(ErrorMessage = "Informe o telefone do usuário!") ]
        [ StringLength(100, ErrorMessage = "O telefone do usuário deve possuir no máximo 100 caracteres!") ]
        public String Telefone { get; set; }
        public DateTime DataCadastro { get; set; }
        [ Required(ErrorMessage = "Informe o status do usuário!") ]
        public Boolean Status { get; set; }
        [ Required(ErrorMessage = "Informe o id do nível de acesso do usuário!") ]
        public int NivelAcessoId { get; set; }
        public NivelAcessoUsuarioDTO NivelAcessoUsuarioDTO { get; set; }
        public String Token { get; set; }
        [ Required(ErrorMessage = "Informe a senha do usuário!") ]
        public String Senha { get; set; }

    }
}
