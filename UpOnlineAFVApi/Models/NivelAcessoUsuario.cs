using System.ComponentModel.DataAnnotations;

namespace UpOnlineAFVApi.Models
{
    public class NivelAcessoUsuario
    {

        public int NivelAcessoUsuarioId { get; set; }
        [ Required(ErrorMessage = "Informe o nome do nível de acesso!") ]
        public String Nome { get; set; }
        [ Required(ErrorMessage = "Informe o status do nível de acesso!") ]
        public Boolean Ativo { get; set; }
        public List<Usuario> Usuarios { get; set; }
        public List<PermissaoNivelAcessoUsuario> Permissoes { get; set; }

        public NivelAcessoUsuario()
        {
            Usuarios = new List<Usuario>();
            Permissoes = new List<PermissaoNivelAcessoUsuario>();
        }

    }
}
