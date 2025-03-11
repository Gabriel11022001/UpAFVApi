using System.ComponentModel.DataAnnotations;

namespace UpOnlineAFVApi.DTOs
{
    public class PermissaoNivelAcessoUsuarioDTO
    {

        public int PermissaoNivelAcessoUsuarioId { get; set; }
        [ Required(ErrorMessage = "Informe o nome da permissão!") ]
        public String Nome { get; set; }
        [ Required(ErrorMessage = "Informe o status da permissão!") ]
        public Boolean Ativo { get; set; }

    }
}
