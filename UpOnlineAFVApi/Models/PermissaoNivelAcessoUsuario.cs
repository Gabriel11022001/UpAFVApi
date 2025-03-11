using System.ComponentModel.DataAnnotations;

namespace UpOnlineAFVApi.Models
{
    public class PermissaoNivelAcessoUsuario
    {

        public int PermissaoNivelAcessoUsuarioId { get; set; }
        [ Required(ErrorMessage = "Informe o nome da permissão!") ]
        public String Nome { get; set; }
        [ Required(ErrorMessage = "Informe o status da permissão!") ]
        public Boolean Ativo { get; set; }
        public List<NivelAcessoUsuario> NiveisAcesso { get; set; }

        public PermissaoNivelAcessoUsuario()
        {
            NiveisAcesso = new List<NivelAcessoUsuario>();
        }

    }
}
